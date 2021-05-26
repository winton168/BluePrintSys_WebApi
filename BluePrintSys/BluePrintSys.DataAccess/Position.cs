using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace BluePrintSys.DataAccess
{

    [Table("Position")]
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int PositionId { get; set; }
        [Required]
        [StringLength(50)]
        public string PositionName { get; set; }
        public bool IsActive { get; set; }

        [InverseProperty(nameof(Employee.Position))]
        public virtual ICollection<Employee> Employees { get; set; }
    }


}


