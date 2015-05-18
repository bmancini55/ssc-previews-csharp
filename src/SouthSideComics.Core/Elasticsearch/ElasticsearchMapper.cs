using Microsoft.Framework.OptionsModel;
using Nest;
using SouthSideComics.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Elasticsearch
{
    public class ElasticsearchMapper
    {
        public ElasticsearchMapper(IOptions<Config> config)
        {
            ElasticsearchUri = config.Options.ElasticsearchUri;
        }

        public string ElasticsearchUri { get; set; }

        public ElasticClient GetClient()
        {
            var node = new Uri(ElasticsearchUri);
            var settings = new ConnectionSettings(
                node,
                defaultIndex: "ssc-items"
            );
            var client = new ElasticClient(settings);
            return client;
        }
    }
}
