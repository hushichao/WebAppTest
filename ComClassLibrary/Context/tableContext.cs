using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComClassLibrary.Models;

namespace ComClassLibrary.Context
{
    public class TableContext : BaseContext<TableContext> 
    {
        public DbSet<TestTable> TestTableInfo { get; set; } 
    }
}
