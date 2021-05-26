using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Models;

namespace Lab2 {
    public class CalcController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Manual() {
            if (Request.Method == "POST") {
                try {
                    var calcModel = new CalcModel {
                        X = Int32.Parse(HttpContext.Request.Form["x"]),
                        Operation = HttpContext.Request.Form["operation"],
                        Y = Int32.Parse(HttpContext.Request.Form["y"])
                    };

                    ViewBag.Result = calcModel.Calc();
                }
                catch {
                    ViewBag.Result = "Invalid input";
                }
                
                return View("Result");
            }
            return View("ViewWithHtmlHelpers");
        }

        [HttpGet]
        [ActionName("ManualWithSeparateHandlers")]
        public IActionResult ManualWithSeparateHandlersGet() {
            return View("ViewWithHtmlHelpers");
        }

        [HttpPost]
        [ActionName("ManualWithSeparateHandlers")]
        public IActionResult ManualWithSeparateHandlersPost() {
            try {
                var calcModel = new CalcModel {
                    X = Int32.Parse(HttpContext.Request.Form["x"]),
                    Operation = HttpContext.Request.Form["operation"],
                    Y = Int32.Parse(HttpContext.Request.Form["y"])
                };

                ViewBag.Result = calcModel.Calc();
            }
            catch {
                ViewBag.Result = "Invalid input";
            }

            return View("Result");
        }

        [HttpGet]
        public IActionResult ModelBindingInParameters() {
            return View("ViewWithHtmlHelpers");
        }

        [HttpPost]
        public IActionResult ModelBindingInParameters(int x, string operation, int y) {
            if (ModelState.IsValid) {
                var calcModel = new CalcModel {
                    X = x,
                    Operation = operation,
                    Y = y
                };
                ViewBag.Result = calcModel.Calc();
            }
            else {
                ViewBag.Result = "Invalid input";
            }

            return View("Result");
        }

        [HttpGet]
        public IActionResult ModelBindingInSeparateModel() {
            return View("ViewWithTagHelpers");
        }

        [HttpPost]
        public IActionResult ModelBindingInSeparateModel(CalcModel model) {
            ViewBag.Result = ModelState.IsValid 
                ? model.Calc() 
                : "Invalid input";

            return View("Result");
        }
    }
}
