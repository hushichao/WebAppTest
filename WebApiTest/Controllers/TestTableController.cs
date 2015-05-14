using System.Transactions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Description;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.SqlClient;
using System.Text;
using ComClassLibrary.Context;
using ComClassLibrary.Models;
using ComClassLibrary.Repo;
///C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.1\System.Transactions.dll
using ThinkNet.Database;

namespace WebAppTest.Controllers
{
     [RoutePrefix("api/Test")]
    public class TestTableController : ApiController
     {

         [Dependency]
         protected IRepository<TestTable, TableContext> DataRepo { get; set; }

         [HttpGet,Route("All"), ResponseType(typeof(List<TestTable>))]
         public async Task<IHttpActionResult> AllList()
         {
             var ssss = await DataRepo.SearchFor(i => true).ToListAsync();
             return Ok(ssss);
         }

    }
}
