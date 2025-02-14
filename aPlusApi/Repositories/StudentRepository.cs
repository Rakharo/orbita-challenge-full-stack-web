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
    public string CreateStudent(Student studentData)
    {
        _context.Student.Add(studentData);
        _context.SaveChanges();
        return studentData.Ra;
    }

    //Method to return all students listed with pagination (default page size of 10 items)public class StudentListResponse


    public StudentListResponse GetAll(int pageNumber, int pageSize)
    {
        int totalStudents = _context.Student.Count();

        var students = _context.Student
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(student => new StudentDTO
            {
                Ra = student.Ra,
                Name = student.Name,
                Email = student.Email,
                Cpf = student.Cpf
            })
            .ToList();

        return new StudentListResponse
        {
            Students = students,
            TotalCount = totalStudents
        };
    }


    //Method to search student by Ra
    public StudentDTO GetStudentByRa(string ra)
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
    public bool DeleteStudent(string ra)
    {
        var student = _context.Student.FirstOrDefault(s => s.Ra == ra); // Agora busca pelo RA corretamente
        if (student != null)
        {
            _context.Student.Remove(student);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    //Method to updated student
    public bool UpdateStudent(string ra, Student updatedStudent)
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