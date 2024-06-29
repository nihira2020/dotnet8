using learndotnet.Repos;
using learndotnet.Repos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace learndotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociateController : ControllerBase
    {
        private readonly LearndataContext _context;
        public AssociateController(LearndataContext context)
        {
            this._context = context;
        }

        [HttpGet("Getall")]
        public async Task<IActionResult> Getall()
        {
            var _data = await this._context.TblCustomers.ToListAsync();
            if (_data.Any())
            {
                return Ok(_data);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpGet("Getbycode")]
        public async Task<IActionResult> Getbycode(string code)
        {
            var _data = await this._context.TblCustomers.FindAsync(code);
            if (_data != null)
            {
                return Ok(_data);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TblCustomer tblCustomer)
        {
            await this._context.TblCustomers.AddAsync(tblCustomer);
            await this._context.SaveChangesAsync();
            return Ok(await Getall());

        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(TblCustomer tblCustomer,string code)
        {
            var _data= await this._context.TblCustomers.FindAsync(code);
            if(_data!=null)
            {
                _data.Name = tblCustomer.Name;
                _data.Email=tblCustomer.Email;
                _data.Creditlimit = tblCustomer.Creditlimit;
                _data.Taxcode = tblCustomer.Taxcode;
                _data.Phone = tblCustomer.Phone;
            }
            await this._context.SaveChangesAsync();
            return Ok(await Getall());

        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(string code)
        {
            var _data = await this._context.TblCustomers.FindAsync(code);
            if (_data != null)
            {
                this._context.TblCustomers.Remove(_data);
            }
            await this._context.SaveChangesAsync();
            return Ok(await Getall());

        }

    }
}
 