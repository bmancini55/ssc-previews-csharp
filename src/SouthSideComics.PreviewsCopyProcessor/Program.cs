using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using SouthSideComics.Core.Mappers;
using SouthSideComics.Core.Models;

namespace SouthSideComics.PreviewsCopyProcessor
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
            services.Configure<ConnectionConfig>(p =>
            {
                p.ConnectionString = configuration.Get("Data:DefaultConnection:ConnectionString");
            });
            services.AddOptions();
            services.AddTransient<PreviewsCopyMapper>();

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
                var copyMapper = serviceProvider.GetService<PreviewsCopyMapper>();

                foreach (var copy in copyData)
                {    
                    Console.WriteLine(string.Format("{0} - {1}", copy.StockNumber, copy.Title));                                    
                    await copyMapper.SaveAsync(copy);                    
                }
            }

            Console.ReadKey();
        }                              
    }

    public class PreviewsCopyMap : CsvClassMap<PreviewsCopy>
    {
        public PreviewsCopyMap()
        {
            Map(p => p.StockNumber).Index(0);
            Map(p => p.Title).Index(1);
            Map(p => p.Price).Index(2);
            Map(p => p.Preview).Index(3);
            Map(p => p.Description).Index(4);
        }
    }
}
