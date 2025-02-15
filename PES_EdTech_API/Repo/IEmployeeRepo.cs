using PES_EdTech_API.Model;

namespace PES_EdTech_API.Repo
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetAll();
        Task<Employee> Getbycode(int code);
        Task<string> Insert(Employee employee);
        Task<string> Update(Employee employee);
        Task<string> Delete(int emp_id);
    }
}
