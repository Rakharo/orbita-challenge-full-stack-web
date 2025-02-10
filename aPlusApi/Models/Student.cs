namespace aPlusApi.Models;


public class Student
{
    // [Key]
    public int Id { get; set; }
    // [Key]
    public int Ra  { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
}