using System.Threading.Tasks;
using Microsoft.Framework.OptionsModel;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System;

namespace SouthSideComics.Core.Mongo
{
    public class PersonMapper : MongoMapper<Person>
    {
        public PersonMapper(IOptions<Config> config) 
            : base(config, "person")
        { }

        public Task<Person> FindByFullNameAsync(string writer)
        {
            return GetCollection()
                .Find(p => p.FullName == writer)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<Person>> FindWritersAsync()
        {
            return new PagedList<Person>(await GetCollection()
                .Find(p => p.Writer)
                .SortBy(p => p.FullName)
                .ToListAsync());            
        }

        public async Task<PagedList<Person>> FindArtistsAsync()
        {
            return new PagedList<Person>(await GetCollection()
                .Find(p => p.Artist)
                .SortBy(p => p.FullName)
                .ToListAsync());
        }

        public async Task<PagedList<Person>> FindCoverArtistAsync()
        {
            return new PagedList<Person>(await GetCollection()
                .Find(p => p.CoverArtist)
                .SortBy(p => p.FullName)
                .ToListAsync());
        }

        
    }
}
