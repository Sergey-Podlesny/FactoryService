using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryService.Models;
using FactoryService.Parser;

namespace FactoryService.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult List()
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            EmployeeParser parser = new EmployeeParser();
            dataBase.ParseData(parser);
            List<Employee> employees = parser.GetData();
            return View(employees);
        }
        public IActionResult Create()
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            CompanyParser parser = new CompanyParser();
            dataBase.ParseData(parser);
            List<Company> companies = parser.GetData();
            ViewBag.Companies = companies;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            dataBase.InsertData(employee);
            return Redirect("/employees/list");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            CompanyParser companyParser = new CompanyParser();
            dataBase.ParseData(companyParser);
            List<Company> companies = companyParser.GetData();
            ViewBag.Companies = companies;

            EmployeeParser employeeParser = new EmployeeParser();
            dataBase.ParseData(employeeParser);
            List<Employee> employees = employeeParser.GetData();
            Employee employee = employees.Where(em => em.Id == id).ToList()[0];

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            dataBase.EditData(employee);
            return Redirect("/employees/list");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            EmployeeParser employeeParser = new EmployeeParser();
            dataBase.ParseData(employeeParser);
            List<Employee> employees = employeeParser.GetData();
            Employee employee = employees.Where(em => em.Id == id).ToList()[0];
            dataBase.DeleteData(employee);

            return Redirect("/employees/list");
        }
    }
}
