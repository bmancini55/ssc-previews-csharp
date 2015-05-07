using Microsoft.Framework.OptionsModel;
using MySql.Data.MySqlClient;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Mappers;
using SouthSideComics.Core.Models;
using System.Data;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mappers
{
    public class ItemMapper : MySqlMapper
    {
        public ItemMapper(IOptions<ConnectionConfig> config)
            : base(config)
        { }

        public Item Parse(MySqlDataReader reader)
        {            
            Item result = null;
            if(reader.HasRows)
            {
                result = new Item()
                {
                    ItemId = reader.GetInt32("itemid"),
                    Adult = reader.GetString("adult"),
                    Artist = reader.GetString("artist"),
                    AllianceSKU = reader.GetString("alliancesku"),
                    BounceUseItem = reader.GetString("bounceuseitem"),
                    BoxPerCase = reader.GetString("boxpercase"),
                    BrandCode = reader.GetString("brandcode"),
                    CardsPerPack = reader.GetString("cardperpack"),
                    Category = reader.GetString("category"),
                    Caution1 = reader.GetString("caution1"),
                    Caution2 = reader.GetString("caution2"),
                    Caution3 = reader.GetString("caution3"),
                    CoverArtist = reader.GetString("coverartist"),
                    DiamondNumber = reader.GetString("diamondnumber"),
                    DiscountCode = reader.GetString("discountcode"),
                    EANNumber = reader.GetString("eannumber"),
                    FOCDate = reader.GetString("focdate"),
                    FOCVendor = reader.GetString("focvendor"),
                    FullTitle = reader.GetString("fulltitle"),
                    Genre = reader.GetString("genre"),
                    Increment = reader.GetString("increment"),
                    IssueNumber = reader.GetString("issuenumber"),
                    IssueSequenceNumber = reader.GetString("issuesequencenumber"),
                    MainDescription = reader.GetString("maindescription"),
                    Mature = reader.GetString("mature"),
                    MaxIssue = reader.GetString("maxissue"),
                    NotePrice = reader.GetString("noteprice"),
                    OfferedAgain = reader.GetString("offeredagain"),
                    OrderFormNotes = reader.GetString("orderformnotes"),
                    PackPerBox = reader.GetString("packperbox"),
                    Page = reader.GetString("page"),
                    ParentItem = reader.GetString("parentitem"),
                    Price = reader.GetString("price"),
                    PrintDate = reader.GetString("printdate"),
                    Publisher = reader.GetString("publisher"),
                    Resolicited = reader.GetString("resolicited"),
                    SeriesCode = reader.GetString("seriescode"),
                    ShipDate = reader.GetString("shipdate"),
                    ShortISBNNumber = reader.GetString("shortisbnnumber"),
                    StandardRetailPrice = reader.GetString("standardretailprice"),
                    StockNumber = reader.GetString("stocknumber"),
                    UPCNumber = reader.GetString("upcnumber"),
                    VariantDescription = reader.GetString("variantdescription"),
                    VolumeTag = reader.GetString("volumetag"),
                    Writer = reader.GetString("writer")
                };
            }
            return result;
        }

        public async Task<PagedList<Item>> FindAllAsync()
        {
            string cmdText = @"
                select *
                from item
                order by itemid;";

            return await ExecuteReaderListAsync(cmdText, CommandType.Text, null, Parse);
        }

        public async Task<Item> FindByStockNumberAsync(string stockNumber)
        {
            string cmdText = @"
                select *
                from item
                where stocknumber = @stocknumber;";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@stocknumber", stockNumber)
            };

            return await ExecuteReaderAsync(cmdText, CommandType.Text, parameters, Parse);
        }

        public async Task<Item> FindByDiamondNumberAsync(string diamondNumber)
        {
            string cmdText = @"
                select *
                from item
                where diamondnumber = @diamondnumber;";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@diamondnumber", diamondNumber)
            };

            return await ExecuteReaderAsync(cmdText, CommandType.Text, parameters, Parse);
        }

        public async Task<Item> FindByUPCAsync(string upc)
        {
            string cmdText = @"
                select *
                from item
                where upcnumber = @upcnumber;";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@upcnumber", upc)
            };

            return await ExecuteReaderAsync(cmdText, CommandType.Text, parameters, Parse);
        }

        public async Task<int> SaveAsync(Item instance)
        {
            string cmdText = @"
                insert into item (
                    itemid, adult, artist, alliancesku, bounceuseitem, boxpercase, brandcode, cardperpack, category, 
                    caution1, caution2, caution3, coverartist, diamondnumber, discountcode, eannumber, focdate, focvendor, 
                    fulltitle, genre, increment, issuenumber, issuesequencenumber, maindescription, mature, maxissue, noteprice, 
                    offeredagain, orderformnotes, packperbox, page, parentitem, price, printdate, publisher, resolicited, 
                    seriescode, shipdate, shortisbnnumber, standardretailprice, stocknumber, 
                    upcnumber, variantdescription, volumetag, writer    
                )
                values (
                    @itemid, @adult, @artist, @alliancesku, @bounceuseitem, @boxpercase, @brandcode, @cardperpack, @category, 
                    @caution1, @caution2, @caution3, @coverartist, @diamondnumber, @discountcode, @eannumber, @focdate, @focvendor, 
                    @fulltitle, @genre, @increment, @issuenumber, @issuesequencenumber, @maindescription, @mature, @maxissue, @noteprice, 
                    @offeredagain, @orderformnotes, @packperbox, @page, @parentitem, @price, @printdate, @publisher, @resolicited, 
                    @seriescode, @shipdate, @shortisbnnumber, @standardretailprice, @stocknumber, 
                    @upcnumber, @variantdescription, @volumetag, @writer
                )
                on duplicate key update
                    adult=@adult, artist=@artist, alliancesku=@alliancesku, bounceuseitem=@bounceuseitem, boxpercase=@boxpercase, brandcode=@brandcode, cardperpack=@cardperpack, category=@category, 
                    caution1=@caution1, caution2=@caution2, caution3=@caution3, coverartist=@coverartist, diamondnumber=@diamondnumber, discountcode=@discountcode, eannumber=@eannumber, focdate=@focdate, focvendor=@focvendor, 
                    fulltitle=@fulltitle, genre=@fulltitle, increment=@increment, issuenumber=@issuenumber, issuesequencenumber=@issuesequencenumber, maindescription=@maindescription, mature=@mature, maxissue=@maxissue, noteprice=@noteprice, 
                    offeredagain=@offeredagain, orderformnotes=@orderformnotes, packperbox=@packperbox, page=@page, parentitem=@parentitem, price=@price, printdate=@printdate, publisher=@publisher, resolicited=@resolicited, 
                    seriescode=@seriescode, shipdate=@shipdate, shortisbnnumber=@shortisbnnumber, standardretailprice=@standardretailprice, stocknumber=@stocknumber, 
                    upcnumber=@upcnumber, variantdescription=@variantdescription, volumetag=@volumetag, writer=@writer;";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@itemid", instance.ItemId),
                new MySqlParameter("@adult", instance.Adult),
                new MySqlParameter("@artist", instance.Artist),
                new MySqlParameter("@alliancesku", instance.AllianceSKU),
                new MySqlParameter("@bounceuseitem", instance.BounceUseItem),
                new MySqlParameter("@boxpercase", instance.BoxPerCase),
                new MySqlParameter("@brandcode", instance.BrandCode),
                new MySqlParameter("@cardperpack", instance.CardsPerPack),
                new MySqlParameter("@category", instance.Category),
                new MySqlParameter("@caution1", instance.Caution1),
                new MySqlParameter("@caution2", instance.Caution2),
                new MySqlParameter("@caution3", instance.Caution3),
                new MySqlParameter("@coverartist", instance.CoverArtist),
                new MySqlParameter("@diamondnumber", instance.DiamondNumber),
                new MySqlParameter("@discountcode", instance.DiscountCode),
                new MySqlParameter("@eannumber", instance.EANNumber),
                new MySqlParameter("@focdate", instance.FOCDate),
                new MySqlParameter("@focvendor", instance.FOCVendor),
                new MySqlParameter("@fulltitle", instance.FullTitle),
                new MySqlParameter("@genre", instance.Genre),
                new MySqlParameter("@increment", instance.Increment),
                new MySqlParameter("@issuenumber", instance.IssueNumber),
                new MySqlParameter("@issuesequencenumber", instance.IssueSequenceNumber),
                new MySqlParameter("@maindescription", instance.MainDescription),
                new MySqlParameter("@mature", instance.Mature),
                new MySqlParameter("@maxissue", instance.MaxIssue),
                new MySqlParameter("@noteprice", instance.NotePrice),
                new MySqlParameter("@offeredagain", instance.OfferedAgain),
                new MySqlParameter("@orderformnotes", instance.OrderFormNotes),
                new MySqlParameter("@packperbox", instance.PackPerBox),
                new MySqlParameter("@page", instance.Page),
                new MySqlParameter("@parentitem", instance.ParentItem),
                new MySqlParameter("@price", instance.Page),
                new MySqlParameter("@printdate", instance.PrintDate),
                new MySqlParameter("@publisher", instance.Publisher),
                new MySqlParameter("@resolicited", instance.Resolicited),
                new MySqlParameter("@seriescode", instance.SeriesCode),
                new MySqlParameter("@shipdate", instance.ShipDate),
                new MySqlParameter("@shortisbnnumber", instance.ShortISBNNumber),
                new MySqlParameter("@standardretailprice", instance.StandardRetailPrice),
                new MySqlParameter("@stocknumber", instance.StockNumber),
                new MySqlParameter("@upcnumber", instance.UPCNumber),
                new MySqlParameter("@variantdescription", instance.VariantDescription),
                new MySqlParameter("@volumetag", instance.VolumeTag),
                new MySqlParameter("@writer", instance.Writer)
            };
            
            var id = await ExecuteNonQueryAsync(cmdText, CommandType.Text, parameters);
            return (int)id;
        }

        public async void DeleteAsync(int itemid)
        {
            string cmdText = @"
                delete from item
                where itemid = @itemid;";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@itemid", itemid)
            };

            await ExecuteNonQueryAsync(cmdText, CommandType.Text, parameters);
        }

    }
}
