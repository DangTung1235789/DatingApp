using System;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        //these properties define relationship between the appuser and message 
        public string SenderPhotoUrl { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public string RecipientPhotoUrl { get; set; }

        //
        public string Content { get; set; }
        // this is to be null if message has not beed read
        public DateTime? DateRead { get; set; }

        //time message is send
        public DateTime MessageSent { get; set; }
        //we dont want to sent these prop back without dto, lúc map sẽ bỏ qua 2 thuộc tính dưới 
        [JsonIgnore]
        public bool SenderDeleted { get; set; }
        [JsonIgnore]
        public bool RecipientDeleted { get; set; }

    
    }
}