using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using PharmacyBusiness.DTOs;
using PharmacyBusiness.Services;

namespace PharmacyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> GetAll()
        {
            var medicines = await _medicineService.GetAllAsync();
            return Ok(medicines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineDto>> GetById(int id)
        {
            var medicine = await _medicineService.GetByIdAsync(id);
            if (medicine == null) return NotFound($"Medicine with Id {id} not found.");
            return Ok(medicine);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MedicineDto>> Create(CreateMedicineDto dto)
        {
            var created = await _medicineService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, CreateMedicineDto dto)
        {
            var success = await _medicineService.UpdateAsync(id, dto);
            if (!success) return NotFound($"Medicine with Id {id} not found.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _medicineService.DeleteAsync(id);
            if (!success) return NotFound($"Medicine with Id {id} not found.");
            return NoContent();
        }
    }
}
