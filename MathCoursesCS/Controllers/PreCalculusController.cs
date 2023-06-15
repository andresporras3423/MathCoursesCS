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
            string question = "for the next function:"+func+" define among the options which one describe it better when x->∞";
            List<string> options = new List<string> { "increasing", "decreasing", "alterning", "undefined" };
            return Content(gf.jsonResponse(question, solution, options), "application/json");
        }

    }
}
