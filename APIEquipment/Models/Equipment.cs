using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace APIEquipment.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public virtual EquipmentType Type { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
