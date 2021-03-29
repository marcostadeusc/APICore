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
                replyDB.Employees.Where(employee => employee.id == id).ToList() : 
                replyDB.Employees.ToList();
            return employees;
        }

        [HttpPost("create")]
        public void CREATE (EmployeesModel employee)
        {
            replyDB.Employees.Add(employee);
            replyDB.SaveChanges();
        }

        [HttpPut("update")]
        public void UPDATE (int id, EmployeesModel employee)
        {
            EmployeesModel employeeUpdate = replyDB.Employees.FirstOrDefault(employee => employee.id == id);
            employeeUpdate.firstName = employee.firstName;
            employeeUpdate.lastName = employee.lastName;
            employeeUpdate.gender = employee.gender;
            employeeUpdate.salary = employee.salary;
            replyDB.Entry(employeeUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            replyDB.SaveChanges();
        }

        [HttpDelete("delete")]
        public void DELETE (int id)
        {
            EmployeesModel employeeDelete = replyDB.Employees.FirstOrDefault(employee => employee.id == id);
            replyDB.Employees.Remove(employeeDelete);
            replyDB.Entry(employeeDelete).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            replyDB.SaveChanges();
        }
    }
}
