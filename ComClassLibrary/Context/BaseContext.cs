using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ComClassLibrary.Models;

namespace ComClassLibrary.Models
{
    public  class BaseContext<TdbContext> : DbContext where TdbContext:DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<BaseContext<TdbContext>>(null);
        }

        public BaseContext()
            : base("Name=Test001Context")
        {
        }

    }
}
