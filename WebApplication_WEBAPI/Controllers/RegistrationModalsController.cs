using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_WEBAPI.Data;
using WebApplication_WEBAPI.Modals;

namespace WebApplication_WEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationModalsController : ControllerBase
    {

        private readonly ApplicationDbAPI _context;


        public RegistrationModalsController(ApplicationDbAPI context)
        {
            _context = context;
        }


        // GET: api/RegistrationModals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrationModal>>> GetregistrationModals()
        {
            var modals = await _context.registrationModals.ToListAsync();
            if (modals == null || modals.Count == 0)
            {
                return NoContent(); // Return 204 if the list is empty
            }

            return Ok(modals); // Return 200 with the data
        }

        // GET: api/RegistrationModals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrationModal>> GetRegistrationModal(int id)
        {
            var registrationModal = await _context.registrationModals.FindAsync(id);

            if (registrationModal == null)
            {
                return NotFound(); // Return 404 if the modal is not found
            }

            return Ok(registrationModal); // Return 200 with the data
        }

        // PUT: api/RegistrationModals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistrationModal(int id, RegistrationModal registrationModal)
        {
            if (id != registrationModal.Id)
            {
                return BadRequest("ID mismatch."); // Return 400 for bad request
            }

            _context.Entry(registrationModal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationModalExists(id))
                {
                    return NotFound(); // Return 404 if the record doesn't exist
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Return 204 if successful with no content
        }

        // POST: api/RegistrationModals
        [HttpPost]
        public async Task<ActionResult<RegistrationModal>> PostRegistrationModal(RegistrationModal registrationModal)
        {
            _context.registrationModals.Add(registrationModal);
            await _context.SaveChangesAsync();

            // Return 201 Created with the location of the new resource
            return CreatedAtAction(nameof(GetRegistrationModal), new { id = registrationModal.Id }, registrationModal);
        }

        // DELETE: api/RegistrationModals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistrationModal(int id)
        {
            var registrationModal = await _context.registrationModals.FindAsync(id);
            if (registrationModal == null)
            {
                return NotFound(); // Return 404 if the record is not found
            }

            _context.registrationModals.Remove(registrationModal);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 if the deletion was successful
        }

        private bool RegistrationModalExists(int id)
        {
            return _context.registrationModals.Any(e => e.Id == id);
        }
    }
}
