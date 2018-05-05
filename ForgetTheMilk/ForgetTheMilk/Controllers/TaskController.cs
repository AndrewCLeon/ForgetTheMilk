using ForgetTheMilk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ForgetTheMilk.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult Index()
        {
            return View(Tasks);
        }

        private static readonly List<Task> Tasks = new List<Task>();

        [HttpPost]
        public ActionResult Add(string task)
        {
            if (!string.IsNullOrWhiteSpace(task))
            {
                Task taskItem = new Task(task, DateTime.Today);
                Tasks.Add(taskItem);
            }
            return RedirectToAction("Index", "Task");
        }
    }
}