using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.MySql;

namespace SouthSideComics.PreviewsItemProcessor
{
    public class Program
    {
        public async void Main(string[] args)
        {            
            // validate file path exists
            if (args.Length != 1)
            {
                Console.WriteLine("File path must be supplied.");
                return;
            }

            IConfiguration configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddUserSecrets();

            IServiceCollection services = new ServiceCollection();
            services.Configure<Config>(p =>
            {
                p.MySqlConnectionString = configuration.Get("Data:DefaultConnection:ConnectionString");
            });
            services.AddOptions();
            services.AddTransient<PreviewsItemMapper>();

            var filePath = args[0];            
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File path must exist");
                return;
            }

            var csvConfig = new CsvConfiguration();
            csvConfig.Delimiter = "\t";            
            
            using (var stream = new StreamReader(filePath))
            using (var csv = new CsvReader(stream, csvConfig)) 
            {
                // csv parser for the previews file            
                //    - check for nuget package
                //    - lets read in a a 3 line file or something and figure out how to get it to work
                //    - lets exclude the top several lines automatically            
                var csvItems = csv.GetRecords<PreviewsItem>();

                // write each record to the database
                var serviceProvider = services.BuildServiceProvider();                                                
                var itemMapper = serviceProvider.GetService<PreviewsItemMapper>();

                foreach (var csvItem in csvItems)
                {    
                    Console.WriteLine(string.Format("{0} - {1}", csvItem.DIAMD_NO, csvItem.FULL_TITLE));                
                    var item = new Core.Models.PreviewsItem()
                    {
                        DiamondNumber = csvItem.DIAMD_NO,
                        StockNumber = csvItem.STOCK_NO,
                        ParentItem = csvItem.PARENT_ITEM_NO_ALT,
                        BounceUseItem = csvItem.BOUNCE_USE_ITEM,
                        FullTitle = csvItem.FULL_TITLE,
                        MainDescription = csvItem.MAIN_DESC,
                        VariantDescription = csvItem.VARIANT_DESC,
                        SeriesCode = csvItem.SERIES_CODE,
                        IssueNumber = csvItem.ISSUE_NO,
                        IssueSequenceNumber = csvItem.ISSUE_SEQ_NO,
                        VolumeTag = csvItem.VOLUME_TAG,
                        MaxIssue = csvItem.MAX_ISSUE,
                        Price = csvItem.PRICE,
                        Publisher = csvItem.PUBLISHER,
                        UPCNumber = csvItem.UPC_NO,
                        ShortISBNNumber = csvItem.SHORT_ISBN_NO,
                        EANNumber = csvItem.EAN_NO,
                        CardsPerPack = csvItem.CARDS_PER_PACK,
                        PackPerBox = csvItem.PACK_PER_BOX,
                        BoxPerCase = csvItem.BOX_PER_CASE,
                        DiscountCode = csvItem.DISCOUNT_CODE,
                        Increment = csvItem.INCREMENT,
                        PrintDate = csvItem.PRNT_DATE,
                        FOCVendor = csvItem.FOC_VENDOR,
                        ShipDate = csvItem.SHIP_DATE,
                        StandardRetailPrice = csvItem.SRP,
                        Category = csvItem.CATEGORY,
                        Genre = csvItem.GENRE,
                        BrandCode = csvItem.BRAND_CODE,
                        Mature = csvItem.MATURE,
                        Adult = csvItem.ADULT,
                        OfferedAgain = csvItem.OA,
                        Caution1 = csvItem.CAUT1,
                        Caution2 = csvItem.CAUT2,
                        Caution3 = csvItem.CAUT3,
                        Resolicited = csvItem.RESOL,
                        NotePrice = csvItem.NOTE_PRICE,
                        OrderFormNotes = csvItem.ORDER_FORM_NOTES,
                        Page = csvItem.PAGE,
                        Writer = csvItem.WRITER,
                        Artist = csvItem.ARTIST,
                        CoverArtist = csvItem.COVER_ARTIST,
                        AllianceSKU = csvItem.ALLIANCE_SKU,
                        FOCDate = csvItem.FOC_DATE
                    };

                    await itemMapper.SaveAsync(item);                    
                }
            }

            Console.ReadKey();
        }               
    }
}
