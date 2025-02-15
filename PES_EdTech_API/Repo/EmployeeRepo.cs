using Dapper;
using PES_EdTech_API.Model;
using PES_EdTech_API.Model.Data;
using System.Data;

namespace PES_EdTech_API.Repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DapperDBContext context;
        public EmployeeRepo(DapperDBContext context)
        {
            this.context = context;
        }

        public async Task<string> Insert(Employee employee)
        {
            string resp = string.Empty;
            string query = "InsertEmployee";
            var parameters = new DynamicParameters();
            parameters.Add("emp_id", employee.emp_id, DbType.Int32);
            parameters.Add("ename", employee.ename, DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                resp = "pass";
            }
            return resp;
        }
        public async Task<List<Employee>> GetAll()
        {
            string query = "GetAllEmployee";
            using (var connectin = this.context.CreateConnection())
            {
                var emplist = await connectin.QueryAsync<Employee>(query);
                return emplist.ToList();
            }
        }
        public async Task<Employee> Getbycode(int emp_id)
        {
            string query = "GetEmployeeById";
            using (var connectin = this.context.CreateConnection())
            {
                var emplist = await connectin.QueryFirstOrDefaultAsync<Employee>(query, new { emp_id });
                return emplist!;
            }
        }
        public async Task<string> Delete(int emp_id)
        {
            string response = string.Empty;
            string query = "DeleteEmployee";
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, new { emp_id });
                response = "pass";
            }
            return response;
        }
        public async Task<string> Update(Employee employee)
        {
            string response = string.Empty;
            string query = "UpdateEmployee";
            var parameters = new DynamicParameters();
            parameters.Add("emp_id", employee.emp_id, DbType.Int32);
            parameters.Add("ename", employee.ename, DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }
    }
}
