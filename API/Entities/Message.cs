using System;

namespace API.Entities
{
    public class Message
    {
        //2. Setting up the entities for messaging
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        //these properties define relationship between the appuser and message 
        public AppUser Sender { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public AppUser Recipient { get; set; }

        //
        public string Content { get; set; }
        // this is to be null if message has not beed read
        public DateTime? DateRead { get; set; }

        //time message is send
        public DateTime MessageSent { get; set; } = DateTime.Now;

        //- the only time we delete a message from the serve, if both the sender and recipient have both 
        //deleted the massage 
        public bool SenderDelete { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}