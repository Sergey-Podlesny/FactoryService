using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryService.Models;
using FactoryService.Parser;

namespace FactoryService.Controllers
{
    public class CompaniesController : Controller
    {
        public IActionResult List()
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            CompanyParser parser = new CompanyParser();
            dataBase.ParseData(parser);
            List<Company> companies = parser.GetData();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            dataBase.InsertData(company);
            return Redirect("/companies/list");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            CompanyParser parser = new CompanyParser();
            dataBase.ParseData(parser);
            List<Company> companies = parser.GetData();
            Company company = companies.Where(com => com.Id == id).ToList()[0];

            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            dataBase.EditData(company);
            return Redirect("/companies/list");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            DataBase dataBase = new DataBase() { ServerName = "localhost", DbName = "AdoDB" };
            CompanyParser parser = new CompanyParser();
            dataBase.ParseData(parser);
            List<Company> companies = parser.GetData();
            Company company = companies.Where(em => em.Id == id).ToList()[0];
            dataBase.DeleteData(company);

            return Redirect("/companies/list");
        }

        
    }
}
