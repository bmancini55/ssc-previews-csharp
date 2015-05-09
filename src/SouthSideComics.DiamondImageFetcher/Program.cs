using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using SouthSideComics.Core.Mappers;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Framework.OptionsModel;

namespace SouthSideComics.DiamondImageFetcher
{
    public class Program
    {
        CookieContainer Cookies { get; set; }
        IConfiguration Configuration { get; set; }
        List<string> StockNumbers { get; set; }

        public async void Main(string[] args)
        {                        
            IServiceCollection services = new ServiceCollection();

            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables()
                .AddUserSecrets();
            
            services.Configure<ConnectionConfig>(p =>
            {                
                p.ConnectionString = Configuration.Get("Data:DefaultConnection:ConnectionString");
            });
            services.AddOptions();          
            services.AddTransient<ItemMapper>();

            // cookie container is used to manage the cookies that happen
            Cookies = new CookieContainer();

            // authenticate with system
            Authenticate();

            // load stock numbers
            var serviceProvider = services.BuildServiceProvider();            
            var itemMapper = serviceProvider.GetRequiredService<ItemMapper>();

            if (args.Length > 0)
            {
                StockNumbers = args[0].Split(',').ToList();
                StockNumbers.ForEach(p => p.Trim());
            }
            else
            {
                var items = await itemMapper.FindAllAsync();
                StockNumbers = items.Select(p => p.StockNumber).ToList();
            }
            

            foreach(var stockNumber in StockNumbers)
            {                
                Fetch(stockNumber);
                Thread.Sleep(1000);
            }
                                   
            Console.ReadKey();
        }

        public void Authenticate()
        {
            // make request to login page with credentials            
            HttpWebRequest request = HttpWebRequest.Create("https://retailerservices.diamondcomics.com/Login/Login?ReturnUrl=%2f") as HttpWebRequest;
            request.CookieContainer = Cookies;
            request.AllowAutoRedirect = false;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla / 5.0(Windows NT 6.1; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 42.0.2311.135 Safari / 537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Referer = "https://retailerservices.diamondcomics.com/Login/Login?ReturnUrl=%2f";
            request.Headers.Add("Origin", "https://retailerservices.diamondcomics.com");

            string postData = string.Format("UserName={0}&EnteredCustNo={1}&Password={2}&RememberMe=false&Submit=Login",
                Configuration.Get("Diamond:UserName"),
                Configuration.Get("Diamond:CustomerId"),
                Configuration.Get("Diamond:Passowrd"));

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var response = request.GetResponse() as HttpWebResponse;
        }

        public void Fetch(string stocknumber)
        {
            Console.WriteLine("Processing " + stocknumber);
            HttpWebRequest request = HttpWebRequest.Create("https://retailerservices.diamondcomics.com/Image/Resource/1/" + stocknumber) as HttpWebRequest;
            request.CookieContainer = Cookies;
            request.AllowAutoRedirect = false;
            request.Method = "GET";
            request.UserAgent = "Mozilla / 5.0(Windows NT 6.1; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 42.0.2311.135 Safari / 537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            var responseStream = response.GetResponseStream();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (var fs = new FileStream(string.Format(@"d:\covers\{0}.jpg", stocknumber), FileMode.Create))
                {
                    responseStream.CopyTo(fs);
                }                            
            }
            else
            {
                Console.WriteLine("Failed with response code " + response.StatusDescription);
            }
        }
    }
}
