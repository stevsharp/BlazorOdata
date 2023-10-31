using BlazorOdata.Server.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BlazorOdata.Shared;

namespace BlazorOdata.Server.Controllers
{
    /// <summary>
    /// https://localhost:7001/odata/template
    /// </summary>
    public class TemplateController : ODataController
    {
        protected readonly AppDbContext _appDbContext;
        public TemplateController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public virtual IQueryable<Template> Get()
        {
            return _appDbContext.Set<Template>().AsQueryable();
        }

        [HttpGet]
        [EnableQuery]
        public SingleResult<Template> GetById([FromODataUri] int key)
        {
            return SingleResult.Create(_appDbContext.Templates.Where(employee => employee.Id == key));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                _appDbContext.Templates.Add(template);

                await _appDbContext.SaveChangesAsync();

                return Created(template);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpDelete]
        public IActionResult Delete([FromODataUri] int key)
        {
            var employee = _appDbContext.Templates.Find(key);

            if (employee == null)
            {
                return NotFound();
            }

            _appDbContext.Templates.Remove(employee);

            _appDbContext.SaveChanges();

            return NoContent();
        }

        [AcceptVerbs("PATCH", "MERGE")]
        public IActionResult Patch([FromODataUri] int key, Delta<Template> patch)
        {
            //Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = _appDbContext.Templates.Find(key);
            if (employee == null)
            {
                return NotFound();
            }

            patch.Patch(employee);

            try
            {
                _appDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(key))
                {
                    return NotFound();
                }

                throw;
            }

            return Updated(employee);
        }

        private bool EmployeeExists(int key)
        {
            return _appDbContext.Templates.Any(e => e.Id == key);
        }


    }
}
