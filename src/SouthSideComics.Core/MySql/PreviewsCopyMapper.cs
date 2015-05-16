using Microsoft.Framework.OptionsModel;
using MySql.Data.MySqlClient;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System.Data;
using System.Threading.Tasks;

namespace SouthSideComics.Core.MySql
{
    public class PreviewsCopyMapper : MySqlMapper
    {
        public PreviewsCopyMapper(IOptions<Config> config)
            : base(config)
        { }

        public PreviewsCopy Parse(MySqlDataReader reader)
        {
            PreviewsCopy result = null;
            if (reader.HasRows)
            {
                result = new PreviewsCopy()
                {
                    //CopyId = reader.GetInt32("copyid"),
                    Description = reader.GetString("description"),
                    Preview = reader.GetString("preview"),
                    Price = reader.GetString("price"),
                    //StockNumber = reader.GetString("stocknumber"),
                    Title = reader.GetString("title")
                };
            }
            return result;
        }

        public async Task<PreviewsCopy> FindByStockNumberAsync(string stockNumber)
        {
            string cmdText = @"
                select *
                from previewscopy
                where stocknumber = @stocknumber;";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@stocknumber", stockNumber)
            };

            return await ExecuteReaderAsync(cmdText, CommandType.Text, parameters, Parse);
        }

        public async Task<int> SaveAsync(PreviewsCopy instance)
        {
            string cmdText = @"
                insert into previewscopy (copyid, stocknumber, title, price, preview, description) 
                values (@copyid, @stocknumber, @title, @price, @preview, @description)
                on duplicate key update
                    stocknumber=@stocknumber, title=@title, price=@price, preview=@preview, description=@description;";

            var parameters = new MySqlParameter[]
            {
                //new MySqlParameter("@copyid", instance.CopyId),
                //new MySqlParameter("@stocknumber", instance.StockNumber),
                new MySqlParameter("@title", instance.Title),
                new MySqlParameter("@price", instance.Price),
                new MySqlParameter("@preview", instance.Preview),
                new MySqlParameter("@description", instance.Description)
            };

            long id = await ExecuteNonQueryAsync(cmdText, CommandType.Text, parameters);
            return (int)id;
        }

        public async void DeleteAsync(int copyid)
        {
            string cmdText = @"
                delete from previewscopy
                where copyid = @copyid;";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@copyid", copyid)
            };

            await ExecuteNonQueryAsync(cmdText, CommandType.Text, parameters);
        }
    }
}