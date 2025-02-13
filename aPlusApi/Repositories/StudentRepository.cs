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

    //Method to create a new student into the db
    public long CreateStudent(Student studentData)
    {
        _context.Student.Add(studentData);
        _context.SaveChanges();
        return studentData.Ra;
    }

    //Method to return all students listed with pagination (default page size of 10 items)
    public List<StudentDTO> GetAll(int pageNumber, int pageSize)
    {
        return _context.Student
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(student => new StudentDTO { Ra = student.Ra, Name = student.Name, Email = student.Email, Cpf = student.Cpf })
            .ToList();
    }

    //Method to search student by Ra
    public StudentDTO GetStudentById(int ra)
    {
        return _context.Student
            .Where(student => student.Ra == ra)
            .Select(student => new StudentDTO { Ra = student.Ra, Name = student.Name, Email = student.Email, Cpf = student.Cpf })
            .FirstOrDefault();
    }

    //Method to search student by name
    public List<StudentDTO> GetStudentByName(string name)
    {
        return _context.Student
            .Where(student => student.Name.Contains(name))
            .Select(student => new StudentDTO { Ra = student.Ra, Name = student.Name, Email = student.Email, Cpf = student.Cpf })
            .ToList();
    }


       //Method to search student by cpf
    public StudentDTO GetStudentByCpf(string cpf)
    {
        return _context.Student
            .Where(student => student.Cpf == cpf)
            .Select(student => new StudentDTO { Ra = student.Ra, Name = student.Name, Email = student.Email, Cpf = student.Cpf })
            .FirstOrDefault();
    }

    //Method to delete student
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

    //Method to updated student
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