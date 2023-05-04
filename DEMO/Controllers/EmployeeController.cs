using DEMO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeModel empObj = new EmployeeModel();

        public IActionResult Index()
        {
            empObj = new EmployeeModel();
            List<EmployeeModel> lst = empObj.getData();
            return View(lst);
        }
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeModel emp)
        {
            bool res;
            if (ModelState.IsValid)
            {
                empObj = new EmployeeModel();
                res = empObj.insert(emp);
                if (res)
                {
                    TempData["msg"] = "Added successfully";
                }
                //}
                else
                {
                    TempData["msg"] = "Not Added. something went wrong..!!";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditEmployee(string id)
        {
            EmployeeModel emp = empObj.getData(id);
            return View(emp);
        }      

        [HttpPost]
        public IActionResult EditEmployee(EmployeeModel emp)
        {
            bool res;
            if (ModelState.IsValid)
            {
                empObj = new EmployeeModel();
                res = empObj.update(emp);
                if (res)
                {
                    TempData["msg"] = "Updated successfully";
                }

                else
                {
                    TempData["msg"] = "Not Updated. something went wrong..!!";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteEmployee(string id)
        {
            EmployeeModel emp = empObj.getData(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult DeleteEmployee(EmployeeModel emp)
        {
            bool res;
            if (ModelState.IsValid)
            {
                empObj = new EmployeeModel();
                res = empObj.delete(emp);
                if (res)
                {
                    TempData["msg"] = "Deleted successfully";
                }

                else
                {
                    TempData["msg"] = "Not Deleted. something went wrong..!!";
                }
            }
            return View();
        }

    }
}
