using System.Collections.Generic;
using System.Linq;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<Employee> GetEmployees()
        {
           return DbContext
                .Employee
                .Include("Person")
                .ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return DbContext
                .Employee
                .Include("Person")
                .SingleOrDefault(e => e.EmployeeId == id);
        }
    }

    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
    }
}