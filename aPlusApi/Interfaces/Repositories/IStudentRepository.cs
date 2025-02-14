using aPlusApi.Models;

namespace aPlusApi.Interfaces.Repositories;

public interface IStudentRepository
{
    // Cria um novo estudante
    string CreateStudent(Student studentData);
    
    // Retorna a lista paginada e o total de alunos
    StudentListResponse GetAll(int pageNumber, int pageSize);

    // Obtém um estudante por RA
    StudentDTO GetStudentByRa(string ra);

    // Obtém um estudante por CPF
    StudentDTO GetStudentByCpf(string cpf);

    // Obtém estudantes pelo nome
    List<StudentDTO> GetStudentByName(string name);

    // Atualiza um estudante
    bool UpdateStudent(string ra, Student studentData);

    // Deleta um estudante
    bool DeleteStudent(string ra);
}
