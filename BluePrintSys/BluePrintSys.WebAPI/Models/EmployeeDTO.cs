using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BluePrintSys.WebAPI.Models
{
    public class EmployeeDTO
    {

        public int EmployeeId { get; set; }
    
        public string EmployeeGuid { get; set; }
      
        public string FullName { get; set; }
     
        public string Address { get; set; }
      
        public string PhoneNumber { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public string Comment { get; set; }

      
    }




}
