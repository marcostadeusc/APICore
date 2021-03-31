using APICore.DAL;
using APICore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ReplyDBContext replyDB;
        public EmployeesController(ReplyDBContext dBContext)
        {
            replyDB = dBContext;
        }

        [HttpGet("list")]
        public List<EmployeesModel> GET (int? id)
        {
            List<EmployeesModel>employees = id.HasValue ? 
                replyDB.Employees.Where(employee => employee.Id == id).ToList() : 
                replyDB.Employees.ToList();
            return employees;
        }

        [HttpPost("create")]
        public List<EmployeesModel> CREATE(EmployeesModel employee)
        {
            replyDB.Employees.Add(employee);
            replyDB.SaveChanges();
            List<EmployeesModel> employeeRes = replyDB.Employees.Where(Employee => Employee.Id == employee.Id).ToList();
            return employeeRes;
        }

        [HttpPut("update")]
        public List<EmployeesModel> UPDATE (int id, EmployeesModel employee)
        {
            EmployeesModel employeeUpdate = replyDB.Employees.FirstOrDefault(employee => employee.Id == id);
            employeeUpdate.FirstName = employee.FirstName;
            employeeUpdate.LastName = employee.LastName;
            employeeUpdate.Gender = employee.Gender;
            employeeUpdate.Salary = employee.Salary;
            replyDB.Entry(employeeUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            replyDB.SaveChanges();
            List<EmployeesModel> employeeRes = replyDB.Employees.Where(employee => employee.Id == id).ToList();
            return employeeRes;
        }

        [HttpDelete("delete")]
        public void DELETE (int id)
        {
            EmployeesModel employeeDelete = replyDB.Employees.FirstOrDefault(employee => employee.Id == id);
            replyDB.Employees.Remove(employeeDelete);
            replyDB.Entry(employeeDelete).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            replyDB.SaveChanges();
        }
    }
}
