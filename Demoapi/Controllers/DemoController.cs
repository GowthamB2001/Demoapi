using Demoapi.Context;
using Demoapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly DemoContext _dbcontext;
        public DemoController (DemoContext demoContext)
        {
            _dbcontext = demoContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemoModel>>> GetDemo()
        { 
            if(_dbcontext.DemoModels == null)
            {
                return NotFound();
            }

            return await _dbcontext.DemoModels.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DemoModel>> GetDemo(int id)

        {
            if (_dbcontext.DemoModels == null)
            {
                return NotFound();
            }
            var demo=await _dbcontext.DemoModels.FindAsync(id);
            if(demo == null)
            {
                return NotFound();
            }
            return demo;
        }
        [HttpPost]
        public async Task<ActionResult<DemoModel>> PostDemo(DemoModel demo )
        {
            _dbcontext.DemoModels.Add(demo);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDemo), new { id = demo.Id }, demo);

        }

        [HttpPut]
        public async Task<IActionResult> PutDemo( int id, DemoModel demo)
        {
            if(id != demo.Id)
            {
                return BadRequest();
            }
            _dbcontext.Entry(demo).State = EntityState.Modified;
            try
            {
                await _dbcontext.SaveChangesAsync();

            }
            catch(DbUpdateConcurrencyException) 
            {
                if (!DemoAvailbale(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;

                }
            
            }
            return Ok();
        }
        private bool DemoAvailbale(int id)
        {
            return (_dbcontext.DemoModels?.Any(x => x.Id == id)).GetValueOrDefault();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemo(int id)
        {
            if (_dbcontext.DemoModels == null)
            {
                return NotFound();

            }
            var demo=await _dbcontext.DemoModels.FindAsync(id);
            if (demo == null)
            {
                return NotFound();
            }
            _dbcontext.DemoModels.Remove(demo);
            await _dbcontext.SaveChangesAsync();
            return Ok();

        }
    }
}
