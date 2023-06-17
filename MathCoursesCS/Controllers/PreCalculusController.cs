using MathCoursesCS.Business;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MathCoursesCS.Controllers
{
    [Route("precalculus")]
    public class PrecalculusController : Controller
    {
        PreCalculusBusiness pb = new PreCalculusBusiness();
        GlobalFunctionBusiness gf = new GlobalFunctionBusiness();

        // this challenge generate a series, this series has 5 values and the solution is the next value in the series
        // the series are in either of the next ways:
        // 1) a+bx
        // 2) a+b^x
        // 3) a+x^|b|
        // the options are five continuous numbers where one of them include the solution
        [HttpGet]
        [Route("series1")]
        public IActionResult series1()
        {
            int addNumber = (new Random().Next(6) +1) * ((new Random().Next(2)*2)-1);
            int opNumber = (new Random().Next(6) + 1) * ((new Random().Next(2) * 2) - 1);
            int seriesType = new Random().Next(3);
            List<int> series = new List<int>();
            if (seriesType==0)
            {
                series = Enumerable.Range(0, 6).Select(i => addNumber+(opNumber*i)).ToList();
            }
            else if(seriesType == 1)
            {
                series = Enumerable.Range(0, 6).Select(i => addNumber + Convert.ToInt32(Math.Pow(opNumber, i))).ToList();
            }
            else
            {
                series = Enumerable.Range(0, 6).Select(i => addNumber + Convert.ToInt32(Math.Pow(i,Math.Abs(opNumber)))).ToList();
            }
            string question = "Which is the next number in the series: " + String.Join(", ",series.GetRange(0, 5));
            int solution = series.Last();
            List<int> options = pb.getContinuousOptions(solution);
            return Content(gf.jsonResponse(question, solution, options), "application/json");
        }



        // this challenge generate a function which can be either: increasing, decreasing, alterning or undefined
        // the struture of the function can be either of the next six:
        // 1) a+bx
        // 2) a+(b^x)
        // 3) a+x^(b)
        // 4) a+ln(bx)
        // 5) a+ln(b^x)
        // 6) a+(x^b)
        // where a and b are number -5 and 5 except for 0.
        [HttpGet]
        [Route("type_series_problem")]
        public IActionResult typeSeriesProblem()
        {
            int addNumber = (new Random().Next(6) + 1) * ((new Random().Next(2) * 2) - 1);
            int opNumber = (new Random().Next(6) + 1) * ((new Random().Next(2) * 2) - 1);
            int limitType = new Random().Next(6);
            string func = "";
            string solution = "";
            if (limitType == 0)
            {
                func = Convert.ToString(addNumber)+ gf.addPlus(opNumber)+"x";
                solution = opNumber > 0 ? "increasing" : "decreasing";
            }
            else if (limitType == 1)
            {
                func = Convert.ToString(addNumber) + "+("+Convert.ToString(opNumber) + "^x)";
                solution = opNumber > 0 ? "increasing" : "alternating";
            }
            else if (limitType == 2)
            {
                func = Convert.ToString(addNumber) + "+x^(" + Convert.ToString(opNumber) + ")";
                solution = opNumber > 0 ? "increasing" : "decreasing";
            }
            else if (limitType == 3)
            { 
                func = Convert.ToString(addNumber) + "+ln(" + Convert.ToString(opNumber) + "x)";
                solution = opNumber > 0 ? "increasing" : "undefined";
            }
            else if (limitType == 4)
            {
                func = Convert.ToString(addNumber) + "+ln((" + Convert.ToString(opNumber) + ")^x)";
                solution = opNumber > 0 ? "increasing" : "undefined";
            }
            else
            {
                func = Convert.ToString(addNumber) + "+ln(x^(" + Convert.ToString(opNumber) + "))";
                solution = opNumber > 0 ? "increasing" : "decreased";
            }
            string question = "for the next function:"+func+", define among the options which one describe it better when x->∞";
            List<string> options = new List<string> { "increasing", "decreasing", "alterning", "undefined" };
            return Content(gf.jsonResponse(question, solution, options), "application/json");
        }

        [HttpGet]
        [Route("bounded_series_problem")]
        public IActionResult boundedSeriesProblem()
        {
            int a = (new Random().Next(6) + 1) * ((new Random().Next(2) * 2) - 1);
            int b = (new Random().Next(6) + 1) * ((new Random().Next(2) * 2) - 1);
            int c = (new Random().Next(6) + 1) * ((new Random().Next(2) * 2) - 1);
            int limitType = new Random().Next(6);
            string func = "";
            string solution = "";
            if (limitType==0)
            {
                func = Convert.ToString(a)+"*(x^"+Convert.ToString(Math.Abs(b*2))+")";
                solution = a < 0 ? "inferior bounded" : "superior bounded";
            }
            else if (limitType == 1)
            {
                func = Convert.ToString(a) + "*ln(" + Convert.ToString(b)+"x)";
                solution = a < 0 ? "inferior bounded" : "superior bounded";
            }
            else if (limitType == 2)
            {
                func = Convert.ToString(Math.Abs(a)) + "/((" + Convert.ToString(b) + "+x)^("+ Convert.ToString(c) + "))";
                solution = (c % 2) == 0 ? "inferior bounded" : "not bounded";
            }
            else if (limitType == 3)
            {
                func = Convert.ToString(Math.Abs(a)) + "/((" + Convert.ToString(b) + ")^(x+" + Convert.ToString(c) + "))";
                solution = b<0 ? "superior bounded" : "superior bounded";
            }
            else if (limitType == 4)
            {
                func = Convert.ToString(Math.Abs(a)) + "/(" + Convert.ToString(b) + "+(" + Convert.ToString(c) + "x)^2)";
                solution = c < 0 ? "not bounded" : "bounded";
            }
            else if (limitType == 5)
            {
                func = "cos(x)*sin(x)^("+ Convert.ToString(a) + ")";
                solution = a < 0 ? "not bounded" : "bounded";
            }
            string question = "Select the option that better describes the next function: " + func;
            List<string> options = new List<string> { "superior bounded", "inferior bounded", "bounded", "not bounded" };
            return Content(gf.jsonResponse(question, solution, options), "application/json");
        }
    }
}
