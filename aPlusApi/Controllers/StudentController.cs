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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents(int pageNumber = 1, int pageSize = 10)
        {
            // Calcula o número de registros a ser "pulados" com base na página atual
            var skip = (pageNumber - 1) * pageSize;

            // Consulta os estudantes aplicando paginação
            var students = await _context.Student
                                         .Skip(skip)
                                         .Take(pageSize)
                                         .ToListAsync();

            if (students == null || !students.Any())
            {
                return NotFound();  // Retorna 404 caso não haja estudantes
            }

            // Retorna os estudantes com status 200 e informações de paginação
            return Ok(students);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            // Encontra o estudante pelo ID
            var student = await _context.Student.FindAsync(id);

            // Verifica se o estudante foi encontrado
            if (student == null)
            {
                return NotFound();  // Retorna 404 caso o estudante não seja encontrado
            }

            // Remove o estudante do contexto
            _context.Student.Remove(student);

            // Salva as alterações no banco
            await _context.SaveChangesAsync();

            // Retorna um status 204 (No Content) indicando sucesso na remoção
            return Ok($"Aluno {student.Name} deletado com sucesso");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            // Verifica se o ID fornecido no URL corresponde ao ID do corpo da requisição
            if (id != student.Id)
            {
                return BadRequest("O ID fornecido nao foi encontrado.");
            }

            // Busca o estudante no banco de dados pelo ID
            var existingStudent = await _context.Student.FindAsync(id);
            
            // Retorna 404 se o estudante não for encontrado
            if (existingStudent == null)
            {
                return NotFound();
            }


            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Cpf = student.Cpf;
            existingStudent.Ra = student.Ra;

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna a resposta com o status 200 e o estudante atualizado
            return Ok(existingStudent);
        }
    }
}