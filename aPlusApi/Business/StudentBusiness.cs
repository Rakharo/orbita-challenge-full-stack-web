using aPlusApi.Interfaces.Business;
using aPlusApi.Interfaces.Repositories;
using aPlusApi.Models;
using System.Collections.Generic;

public class StudentBusiness : IStudentBusiness
{
    private readonly IStudentRepository _studentRepository;

    public StudentBusiness(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public long CreateStudent(StudentDTO studentDto)
    {
        // Converte StudentDTO para Student antes de chamar o repositório
        var student = new Student
        {
            Ra = studentDto.Ra,
            Name = studentDto.Name,
            Email = studentDto.Email,
            Cpf = studentDto.Cpf
        };
        // validates if a student already have same RA
        var existingStudentRA = _studentRepository.GetStudentByRa(student.Ra);
        if (existingStudentRA != null)
        {
            throw new Exception("Já existe um estudante com este RA.");
        }

        // validates if a student already have the same cpf
        var existingStudentCPF = _studentRepository.GetStudentByCpf(student.Cpf);
        if (existingStudentCPF != null)
        {
            throw new Exception("Já existe um estudante com este CPF.");
        }

        return _studentRepository.CreateStudent(student);
    }

    public List<StudentDTO> GetAllStudents(int pageNumber, int pageSize)
    {
        return _studentRepository.GetAll(pageNumber, pageSize);
    }

    public StudentDTO GetStudentByRa(int ra)
    {
        return _studentRepository.GetStudentByRa(ra);
    }

    public List<StudentDTO> GetStudentByName(string name)
    {
        return _studentRepository.GetStudentByName(name);
    }

    public bool DeleteStudent(int ra)
    {
        return _studentRepository.DeleteStudent(ra);
    }

    public bool UpdateStudent(int ra, StudentDTO updatedStudentDto)
    {
        // validate if student exists on db
        var existingStudent = _studentRepository.GetStudentByRa(ra);
        if (existingStudent == null)
        {
            throw new Exception("Estudante não encontrado.");
        }
        var updatedStudent = new Student
        {
            Ra = ra, // Mantemos o mesmo RA
            Name = updatedStudentDto.Name,
            Email = updatedStudentDto.Email,
            Cpf = updatedStudentDto.Cpf
        };

        return _studentRepository.UpdateStudent(ra, updatedStudent);
    }
}
