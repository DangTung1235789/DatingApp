using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Group
    {
        public Group() { }
        public Group(string name)
        {
           Name = name;
        }
        // 17.12. Tracking the message groups
        [Key]
        
        public string Name { get; set; }
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }
}