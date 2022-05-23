using CarDealershipLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipLab.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        CarContext _context = new CarContext();

       

        [HttpGet("FullCarList")]
        public List<Car> GetCars()
        {
            return _context.Cars.ToList();
        }
       

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(car);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id,Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }
            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new {id=car.Id}, car);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if(car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(c => c.Id == id);
        }
    }
}
