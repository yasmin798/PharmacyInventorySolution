using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using PharmacyBusiness.DTOs;
using PharmacyBusiness.Services;

namespace PharmacyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll()
        {
            return Ok(await _supplierService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetById(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier == null) return NotFound($"Supplier with Id {id} not found.");
            return Ok(supplier);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SupplierDto>> Create(CreateSupplierDto dto)
        {
            var created = await _supplierService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _supplierService.DeleteAsync(id);
            if (!success) return NotFound($"Supplier with Id {id} not found.");
            return NoContent();
        }
    }
}
