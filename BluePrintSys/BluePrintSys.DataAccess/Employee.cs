using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace BluePrintSys.DataAccess
{

    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(150)]
        public string EmployeeGuid { get; set; }
        [Required]
        [StringLength(150)]
        public string FullName { get; set; }
        [Required]
        [StringLength(150)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        public int PositionId { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }

        [ForeignKey(nameof(PositionId))]
        [InverseProperty("Employees")]
        public virtual Position Position { get; set; }
    }

}
