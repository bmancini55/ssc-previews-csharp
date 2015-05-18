using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using SouthSideComics.Core.Mongo;
using SouthSideComics.Core.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SouthSideComics.ItemImporter
{
    public class Program
    {
        public void Main(string[] args)
        {
            IConfiguration configuration = new Configuration()                
                .AddUserSecrets();

            IServiceCollection services = new ServiceCollection();
            services.Configure<Config>(configuration.GetSubKey("Data:Config"));
            services.AddOptions();
            services.AddTransient<CategoryMapper>();
            services.AddTransient<GenreMapper>();
            services.AddTransient<Core.Mongo.ItemMapper>();
            services.AddTransient<PersonMapper>();            
            services.AddTransient<PreviewsItemMapper>();
            services.AddTransient<PublisherMapper>();
            services.AddTransient<ItemImportFactory>();
            services.AddTransient<SeriesMapper>();
            services.AddTransient<Core.Elasticsearch.ItemMapper>();
            
            IServiceProvider provider = services.BuildServiceProvider();

            var importService = ActivatorUtilities.CreateInstance<ItemImportService>(provider);
            Task.WaitAll(importService.Process("MAY15"));            

            Console.ReadKey();
        }
    }

    public class ItemImportService
    {
        public ItemImportService(ItemImportFactory itemImportFactory, PreviewsItemMapper previewsItemMapper, CategoryMapper categoryMapper, 
            PublisherMapper publisherMapper, PersonMapper personMapper, SeriesMapper seriesMapper, GenreMapper genreMapper, Core.Mongo.ItemMapper itemMapper, Core.Elasticsearch.ItemMapper esItemMapper)
        {
            this.factory = itemImportFactory;
            this.previewsItemMapper = previewsItemMapper;
            this.categoryMapper = categoryMapper;
            this.publisherMapper = publisherMapper;
            this.personMapper = personMapper;
            this.seriesMapper = seriesMapper;
            this.genreMapper = genreMapper;
            this.itemMapper = itemMapper;
            this.esItemMapper = esItemMapper;
        }

        ItemImportFactory factory;
        PreviewsItemMapper previewsItemMapper;
        CategoryMapper categoryMapper;
        PublisherMapper publisherMapper;
        PersonMapper personMapper;
        SeriesMapper seriesMapper;
        GenreMapper genreMapper;
        Core.Mongo.ItemMapper itemMapper;
        Core.Elasticsearch.ItemMapper esItemMapper;
        
        public async Task Process(string preview)
        {
            var previewsitems = await previewsItemMapper.FindAsync(1, int.MaxValue, preview);
            foreach(var previewsitem in previewsitems)
            {
                Category category = null;
                Genre genre = null;
                Series series = null;
                Publisher publisher = null;
                Person writer = null;
                Person artist = null;
                Person coverartist = null;


                // category
                category = await categoryMapper.FindByIdAsync(p => p.Id == previewsitem.Category);

                // genre
                genre = await genreMapper.FindByIdAsync(p => p.Id == previewsitem.Genre);

                // series
                series = 
                    await seriesMapper.FindByIdAsync(p => p.Id == previewsitem.SeriesCode) ??
                    await seriesMapper.InsertAsync(factory.CreateSeries(previewsitem));

                // publisher
                if (!string.IsNullOrEmpty(previewsitem.Publisher))
                {
                    publisher =
                        await publisherMapper.FindByNameAsync(previewsitem.Publisher) ??
                        await publisherMapper.InsertAsync(factory.CreatePublisher(previewsitem));
                }

                // writer
                if (!string.IsNullOrEmpty(previewsitem.Writer))
                {
                    writer =
                        await personMapper.FindByFullNameAsync(previewsitem.Writer) ??
                        await personMapper.InsertAsync(factory.CreateWriter(previewsitem));
                }

                // artist
                if (!string.IsNullOrEmpty(previewsitem.Artist))
                {
                    artist =
                        await personMapper.FindByFullNameAsync(previewsitem.Artist) ??
                        await personMapper.InsertAsync(factory.CreateArtist(previewsitem));
                }

                // cover artist
                if (!string.IsNullOrEmpty(previewsitem.Artist))
                {
                    coverartist =
                        await personMapper.FindByFullNameAsync(previewsitem.CoverArtist) ??
                        await personMapper.InsertAsync(factory.CreateCoverArtist(previewsitem));
                }

                // item
                var item = factory.CreateItem(previewsitem, category, genre, series, publisher, writer, artist, coverartist);                
                item = await itemMapper.InsertAsync(item);
                await esItemMapper.SaveAsync(item);
                Console.WriteLine("Wrote: " + item.Title);
            }
        }                       
    }

    public class ItemImportFactory
    {
        public Item CreateItem(PreviewsItem previewsItem, Category category, Genre genre, Series series, Publisher publisher, Person writer, Person artist, Person coverartist)
        {
            var title = previewsItem.Copy.Title;
            var description = previewsItem.Copy.Preview + "\n" + previewsItem.Copy.Description;
            var previews = new List<Item.PreviewsItemLink>()
            {
                new Item.PreviewsItemLink(previewsItem)               
            };

            Item.PersonLink writerLink = writer == null ? null : new Item.PersonLink(writer);
            Item.PersonLink artistLink = artist == null ? null : new Item.PersonLink(artist);
            Item.PersonLink coverartistLink = coverartist == null ? null : new Item.PersonLink(coverartist);

            return new Item()
            {
                Adult = previewsItem.Adult,                 // TODO TYPE
                AllianceSKU = previewsItem.AllianceSKU,
                Artist = artistLink,
                BoxPerCase = previewsItem.BoxPerCase,
                BrandCode = previewsItem.BrandCode,         // TODO ??
                CardsPerPack = previewsItem.CardsPerPack,
                Category = category,
                Caution1 = previewsItem.Caution1,
                Caution2 = previewsItem.Caution2,
                Caution3 = previewsItem.Caution3,
                CoverArtist = coverartistLink,
                Description = description,
                DiscountCode = previewsItem.DiscountCode,   // TODO ??
                EANNumber = previewsItem.EANNumber,
                FOCDate = previewsItem.FOCDate,             // TODO TYPE
                FOCVendor = previewsItem.FOCVendor,
                Genre = genre,
                Increment = previewsItem.Increment,
                IssueNumber = previewsItem.IssueNumber,
                IssueSequenceNumber = previewsItem.IssueSequenceNumber,
                Mature = previewsItem.Mature,               // TODO TYPE
                MaxIssue = previewsItem.MaxIssue,
                PackPerBox = previewsItem.PackPerBox,
                ParentItem = previewsItem.ParentItem,
                Previews = previews,
                Price = previewsItem.Price,                 // TODO TYPE
                PrintDate = previewsItem.PrintDate,         // TODO TYPE
                Publisher = publisher,
                Series = series,
                ShipDate = previewsItem.ShipDate,           // TODO TYPE
                ShortISBNNumber = previewsItem.ShortISBNNumber,
                StandardRetailPrice = previewsItem.StandardRetailPrice,
                StockNumber = previewsItem.StockNumber,
                Title = title,
                UPCNumber = previewsItem.UPCNumber,
                VariantDescription = previewsItem.VariantDescription,
                VolumeTag = previewsItem.VolumeTag,
                Writer = writerLink
            };
        }

        public Series CreateSeries(PreviewsItem previewitem)
        {
            return new Series()
            {
                Id = previewitem.SeriesCode,
                Name = previewitem.MainDescription
            };
        }

        public Publisher CreatePublisher(PreviewsItem previewItem)
        {
            return new Publisher()
            {
                Name = previewItem.Publisher
            };
        }

        public Person CreateWriter(PreviewsItem previewsItem)
        {
            return new Person()
            {
                FullName = previewsItem.Writer,
                Writer = true
            };
        }

        public Person CreateArtist(PreviewsItem previewItem)
        {
            return new Person()
            {
                FullName = previewItem.Artist,
                Artist = true
            };
        }

        public Person CreateCoverArtist(PreviewsItem previewsItem)
        {
            return new Person()
            {
                FullName = previewsItem.CoverArtist,
                CoverArtist = true
            };
        }
    }

}
