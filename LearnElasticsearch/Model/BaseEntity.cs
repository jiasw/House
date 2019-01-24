using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LearnElasticsearch.Model
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public string State { get; set; }
        public string CreateTime { get; set; }

        public string UpdateTime { get; set; }
    }
}
