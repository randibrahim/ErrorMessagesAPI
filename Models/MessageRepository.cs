using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorMessagesAPI.Models
{
    public class MessageRepository :IMessageRepository
    {
        private readonly IMessageContext _context;
        public MessageRepository(IMessageContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await _context
                            .Messages
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<Message> GetMessage(long id)
        {
            FilterDefinition<Message> filter = Builders<Message>.Filter.Eq(m => m.Id, id);
            return _context
                    .Messages
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(Message message)
        {
            await _context.Messages.InsertOneAsync(message);
        }
        public async Task<bool> Update(Message message)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Messages
                        .ReplaceOneAsync(
                            filter: g => g.Id == message.Id,
                            replacement: message);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(long id)
        {
            FilterDefinition<Message> filter = Builders<Message>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Messages
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<long> GetNextId()
        {
            return await _context.Messages.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}