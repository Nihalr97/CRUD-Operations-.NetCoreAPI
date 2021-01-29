using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Audree.Demo.Core.Models
{
    [Table("PLClub", Schema = "Admin")]
    public class PLClub
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string plname { get; set; }
        public DateTime Date { get; set; }
        public int? seasonrank { get; set; }
    }
}
