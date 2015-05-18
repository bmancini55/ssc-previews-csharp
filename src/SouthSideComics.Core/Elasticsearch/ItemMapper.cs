using Microsoft.Framework.OptionsModel;
using Nest;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Elasticsearch
{
    public class ItemMapper : ElasticsearchMapper
    {
        public ItemMapper(IOptions<Config> config)
            : base(config)
        { }

        public async Task SaveAsync(Item instance)
        {
            var result = await GetClient()
                .IndexAsync(instance, i => i
                    .Index("ssc-item")
                    .Type("item")
                );
        }

        public async Task<IEnumerable<Item>> FindAsync(int page, int pagesize, string preview, string publisher, string writer, string artist, string text)
        {
            var results = await GetClient()
                .SearchAsync<Item>(s => s
                    .Index("ssc-item")
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
                                        sq => sq.Match(m => m.Query(text).OnField(item => item.Title)),
                                        sq => sq.Match(m => m.Query(text).OnField(item => item.Publisher.Name)),
                                        sq => sq.Match(m => m.Query(text).OnField(item => item.Writer.FullName)),
                                        sq => sq.Match(m => m.Query(text).OnField(item => item.Artist.FullName))
                                    )
                                )
                            )
                        )
                    )
                    .SortAscending(sort => sort.Previews[0].PreviewNumber)
                );            
            return results.Documents;
        }
    }
}
