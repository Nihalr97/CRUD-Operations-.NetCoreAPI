using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Audree.Demo.Core.Models.Admin
{
    [Table("Employ", Schema = "Admin")]
    public class Employ
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string empname { get; set; }
        public DateTime Date { get; set; }
        public int? empid { get; set; }
    }
}
