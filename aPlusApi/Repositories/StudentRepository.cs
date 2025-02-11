using aPlusApi.Interfaces.Repositories;
using aPlusApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public long CreateStudent(Student studentData)
    {
        _context.Student.Add(studentData);
        _context.SaveChanges();
        return studentData.Ra;
    }

    public List<StudentDTO> GetAll(int pageNumber, int pageSize)
    {
        return _context.Student
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(student => new StudentDTO { Ra = student.Ra, Name = student.Name, Email = student.Email })
            .ToList();
    }


    public StudentDTO GetStudentById(int ra)
    {
        return _context.Student
            .Where(student => student.Ra == ra)
            .Select(student => new StudentDTO { Ra = student.Ra, Name = student.Name, Email = student.Email })
            .FirstOrDefault();
    }

    public List<StudentDTO> GetStudentByName(string name)
    {
        return _context.Student
            .Where(student => student.Name.Contains(name))
            .Select(student => new StudentDTO { Ra = student.Ra, Name = student.Name, Email = student.Email })
            .ToList();
    }

    public bool DeleteStudent(int ra)
    {
        var student = _context.Student.Find(ra);
        if (student != null)
        {
            _context.Student.Remove(student);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool UpdateStudent(int ra, Student updatedStudent)
    {
        var existingStudent = _context.Student.FirstOrDefault(s => s.Ra == ra);

        if (existingStudent == null)
        {
            return false;
        }

        existingStudent.Name = updatedStudent.Name;
        existingStudent.Email = updatedStudent.Email;
        existingStudent.Cpf = updatedStudent.Cpf;

        _context.SaveChanges();
        return true;
    }
}