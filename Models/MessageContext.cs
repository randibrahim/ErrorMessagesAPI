using ErrorMessagesAPI.Config;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorMessagesAPI.Models
{
    public class MessageContext : IMessageContext
    {
        private readonly IMongoDatabase _db;
        public MessageContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<Message> Messages => _db.GetCollection<Message>("Messages");
    }
}

