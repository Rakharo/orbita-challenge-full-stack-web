using Microsoft.AspNetCore.Mvc;
using aPlusApi.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace aPlusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // Método para inserir um estudante
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { Id = student.Id }, student);
        }


        // Método para buscar um estudante por ID
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            var student = await _context.Student.FindAsync(Id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
    }
}