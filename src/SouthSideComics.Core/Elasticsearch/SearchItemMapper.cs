using Microsoft.Framework.OptionsModel;
using Nest;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Elasticsearch
{
    public class SearchItemMapper : ElasticsearchMapper
    {
        public SearchItemMapper(IOptions<Config> config)
            : base(config)
        { }

        public async Task SaveAsync(SearchItem instance)
        {
            var result = await GetClient()
                .IndexAsync(instance, i => i
                    .Index("items")
                    .Type("item")
                );
        }

        public async Task<PagedList<SearchItem>> FindAsync(int page, int pagesize, string preview, string publisher, string writer, string artist, string text)
        {
            var results = await GetClient()
                .SearchAsync<SearchItem>(s => s
                    .Index("items")
                    .Type("item")
                    .From((page - 1) * pagesize)
                    .Size(pagesize)
                    .Query(mainQuery => mainQuery
                        .Filtered(filtered => filtered
                            .Filter(filter => filter
                                .Bool(boolFilter => boolFilter
                                    .Must(
                                        mq => mq.Term(t => t.Publisher.Id, publisher),
                                        mq => mq.Term(t => t.Writer.Id, writer),
                                        mq => mq.Term(t => t.Artist.Id, artist)
                                    )
                                )
                            )
                            .Query(query => query
                                .Bool(boolFilter => boolFilter
                                    .Should(
                                        sq => sq.MatchPhrase(m => m.Query(text).OnField(item => item.Title)),
                                        sq => sq.MatchPhrase(m => m.Query(text).OnField(item => item.Publisher.Name)),
                                        sq => sq.MatchPhrase(m => m.Query(text).OnField(item => item.Writer.FullName)),
                                        sq => sq.MatchPhrase(m => m.Query(text).OnField(item => item.Artist.FullName))
                                    )
                                )
                            )
                        )
                    )
                    .SortAscending(sort => sort.Previews[0].PreviewNumber)
                );
            return new PagedList<SearchItem>(results.Documents, page, pagesize, (int)results.HitsMetaData.Total);            
        }
    }
}
