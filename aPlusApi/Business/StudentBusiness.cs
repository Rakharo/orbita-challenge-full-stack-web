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

    public long CreateStudent(Student student)
    {
        // Regra de negócio: validar se já existe um estudante com o mesmo RA antes de criar
        var existingStudentRA = _studentRepository.GetStudentById(student.Ra);
        if (existingStudentRA != null)
        {
            throw new Exception("Já existe um estudante com este RA.");
        }
        
        // Regra de negócio: validar se já existe um estudante com o mesmo CPF
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

    public StudentDTO GetStudentById(int ra)
    {
        return _studentRepository.GetStudentById(ra);
    }

    public List<StudentDTO> GetStudentByName(string name)
    {
        return _studentRepository.GetStudentByName(name);
    }

    public bool DeleteStudent(int ra)
    {
        return _studentRepository.DeleteStudent(ra);
    }

    public bool UpdateStudent(int ra, Student updatedStudent)
    {
        // Verifica se o estudante existe antes de atualizar
        var existingStudent = _studentRepository.GetStudentById(ra);
        if (existingStudent == null)
        {
            throw new Exception("Estudante não encontrado.");
        }

        return _studentRepository.UpdateStudent(ra, updatedStudent);
    }
}
