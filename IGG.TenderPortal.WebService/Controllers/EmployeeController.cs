using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using IGG.TenderPortal.DtoModel.Models;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employee
        public IEnumerable<EmployeeModel> Get()
        {
            var employees = _employeeService.GetEmployees();
            return Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeModel>>(employees);
        }

        // GET: api/Employee/5
        public EmployeeModel Get(int id)
        {
            var employee = _employeeService.GetEmployee(id);
            return Mapper.Map<Employee, EmployeeModel>(employee);
        }

        // POST: api/Employee
        public void Post([FromBody]EmployeeModel value)
        {
            var employee = Mapper.Map<EmployeeModel, Employee>(value);
            employee.Person = Mapper.Map<EmployeeModel, Person>(value);

            _employeeService.CreateEmployee(employee);
            _employeeService.SaveEmployee();
        }

        // PUT: api/Employee/5
        public void Put(int id, [FromBody]EmployeeModel value)
        {
            var employee = _employeeService.GetEmployee(id);

            Mapper.Map(value, employee);
            Mapper.Map(value, employee.Person);

            _employeeService.UpdateEmployee(employee);
            _employeeService.SaveEmployee();
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
            var employee = _employeeService.GetEmployee(id);

            _employeeService.RemoveEmployee(employee);
            _employeeService.SaveEmployee();
        }
    }
}
