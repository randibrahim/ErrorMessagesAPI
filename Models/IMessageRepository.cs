using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorMessagesAPI.Models
{

   public interface IMessageRepository
    {
        // api/[GET]
        Task<IEnumerable<Message>> GetAllMessages();
        // api/1/[GET]
        Task<Message> GetMessage(long id);
        // api/[POST]
        Task Create(Message message);
        // api/[PUT]
        Task<bool> Update(Message message);
        // api/1/[DELETE]
        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }
}
