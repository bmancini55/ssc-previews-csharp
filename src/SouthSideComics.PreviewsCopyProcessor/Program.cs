using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using SouthSideComics.Core.Models;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Mongo;
using System.Threading.Tasks;

namespace SouthSideComics.PreviewsCopyProcessor
{
    public class Program
    {
        public async Task Main(string[] args)
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
            services.Configure<Config>(configuration.GetSubKey("Data:Config"));            
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
            csvConfig.Quote = '\"';
            csvConfig.RegisterClassMap<PreviewsCopyMap>();
            csvConfig.HasHeaderRecord = false;            
            
            using (var stream = new StreamReader(filePath))
            using (var csv = new CsvReader(stream, csvConfig)) 
            {
                // csv parser for the previews copy data                
                var copyData = csv.GetRecords<PreviewsCopy>();

                // write each record to the database
                var serviceProvider = services.BuildServiceProvider();                                                
                var copyMapper = serviceProvider.GetService<PreviewsItemMapper>();

                foreach (var copy in copyData)
                {    
                    Console.WriteLine(string.Format("{0} - {1}", copy.DiamondNumber, copy.Title));                                    
                    await copyMapper.SaveCopyAsync(copy);                    
                }
            }

            Console.ReadKey();
        }                              
    }

    public class PreviewsCopyMap : CsvClassMap<PreviewsCopy>
    {
        public PreviewsCopyMap()
        {
            Map(p => p.DiamondNumber).Index(0);
            Map(p => p.Title).Index(1);
            Map(p => p.Price).Index(2);
            Map(p => p.Preview).Index(3);
            Map(p => p.Description).Index(4);
        }
    }
}
