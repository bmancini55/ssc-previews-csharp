﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Models
{
    public class Category
    { 
        public string Id { get; set; }
        public string Name { get; set; }        
        public bool ForConsumers { get; set; }
    }
}
