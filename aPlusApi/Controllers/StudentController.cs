using aPlusApi.Interfaces.Business;
using aPlusApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentBusiness _studentBusiness;

    public StudentsController(IStudentBusiness studentBusiness)
    {
        _studentBusiness = studentBusiness;
    }

    // POST
    [HttpPost]
    public IActionResult CreateStudent([FromBody] Student student)
    {
        try
        {
            var ra = _studentBusiness.CreateStudent(student);
            return Ok(new { message = "Aluno criado com sucesso", ra });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET All
    [HttpGet]
    public IActionResult GetAllStudents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        return Ok(_studentBusiness.GetAllStudents(pageNumber, pageSize));
    }

    // GET ByID
    [HttpGet("{ra}")]
    public IActionResult GetStudentById(int ra)
    {
        var student = _studentBusiness.GetStudentById(ra);
        if (student == null)
        {
            return NotFound(new { message = "Aluno não encontrado" });
        }
        return Ok(student);
    }

    // DELETE
    [HttpDelete("{ra}")]
    public IActionResult DeleteStudent(int ra)
    {
        if (_studentBusiness.DeleteStudent(ra))
        {
            return Ok(new { message = "Aluno deletado com sucesso" });
        }
        return NotFound(new { message = "Aluno não encontrado" });
    }

    //PUT
    [HttpPut("{ra}")]
    public IActionResult UpdateStudent(int ra, [FromBody] Student updatedStudent)
    {
        try
        {
            bool updated = _studentBusiness.UpdateStudent(ra, updatedStudent);
            if (!updated)
            {
                return NotFound("Estudante não encontrado.");
            }
            return Ok("Estudante atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
