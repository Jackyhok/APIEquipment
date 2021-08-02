using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIEquipment.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIEquipment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly TMAEquipmentContext _context;

        public EquipmentsController(TMAEquipmentContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipment()
        {
            return await _context.Equipment.ToListAsync();
        }

        [HttpGet("GetEquipmentDetails/{id}")]
        public async Task<ActionResult<Equipment>> GetEquipmentDetails(int id)
        {
            var equipmentid = await _context.Equipment.FindAsync(id);
            var equipment = await _context.Equipment
                                            .Include(pub => pub.Employee)
                                            .Where(pub => pub.Id == id)
                                            .Include(pub => pub.Type)
                                            .Where(pub => pub.TypeId == equipmentid.TypeId)
                                            .FirstOrDefaultAsync();

            if (equipment == null)
            {
                return NotFound();
            }

            return equipment;
        }

        [HttpGet("PostEquipmentDetails/")]
        public async Task<ActionResult<Equipment>> PostEquipmentDetails(int id)
        {


            var equipment = new Equipment();
            equipment.Name = "Máy in C";
            equipment.Status = "Chưa xài";
            equipment.Description = "Máy in hãng C";
            equipment.TypeId = 3;

            Employee employee = new Employee();
            employee.Name = "Trần Thị D";



            equipment.Employee.Add(employee);


            _context.Equipment.Add(equipment);
            _context.SaveChanges();


            var equipmentid = await _context.Equipment.FindAsync(id);
            var equipments = await _context.Equipment
                                            .Include(pub => pub.Employee)
                                            .Include(pub => pub.Type)
                                            .Where(pub => pub.Id == equipment.Id)
                                            .FirstOrDefaultAsync();

            if (equipments == null)
            {
                return NotFound();
            }

            return equipments;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetEquipment(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            return equipment;
        }

        
        [HttpPut("UpdateEquipment/{id}")]
        public async Task<IActionResult> PutEquipment(int id, Equipment equipment)
        {
            if (id != equipment.Id)
            {
                return BadRequest();
            }

            _context.Entry(equipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpPost("CreateEquipment")]
        public async Task<ActionResult<Equipment>> PostEquipment(Equipment equipment)
        {
            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipment", new { id = equipment.Id }, equipment);
        }

        
        [HttpDelete("DeleteEquipment/{id}")]
        public async Task<ActionResult<Equipment>> DeleteEquipment(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();

            return equipment;
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.Id == id);
        }
    }
}
