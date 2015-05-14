using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComClassLibrary.Models
{
    [Table("TestTable")]
    public partial class TestTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public DateTime? Tag { get; set; }
    }
}
