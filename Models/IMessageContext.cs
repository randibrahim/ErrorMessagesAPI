
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorMessagesAPI.Models
{
  public  interface IMessageContext
    {
        IMongoCollection<Message> Messages { get; }

    }
}
