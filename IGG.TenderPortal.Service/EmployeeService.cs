using System.Collections.Generic;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Service
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int id);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void RemoveEmployee(Employee employee);
        void SaveEmployee();
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeRepository.GetEmployees();            
        }

        public Employee GetEmployee(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            return employee;
        }

        public void CreateEmployee(Employee employee)
        {
            _personRepository.Add(employee.Person);
            _employeeRepository.Add(employee);            
        }

        public void UpdateEmployee(Employee employee)
        {
            _personRepository.Update(employee.Person);
            _employeeRepository.Update(employee);                        
        }

        public void RemoveEmployee(Employee employee)
        {
            _personRepository.Delete(employee.Person);
            _employeeRepository.Delete(employee);            
        }

        public void SaveEmployee()
        {
            _unitOfWork.Commit();
        }
    }
}
