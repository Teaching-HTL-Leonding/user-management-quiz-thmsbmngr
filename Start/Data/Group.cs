using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Data
{
    public class Group
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public List<User>? Users { get; set; } = new List<User>();

        public Group? ParentGroup { get; set; }
        
        public List<Group>? ChildGroups { get; set; }
    }
}