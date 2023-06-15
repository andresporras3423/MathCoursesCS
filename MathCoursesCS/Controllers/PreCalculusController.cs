using MathCoursesCS.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MathCoursesCS.Controllers
{
    [Route("precalculus")]
    public class PrecalculusController : Controller
    {
        PreCalculusBusiness pb = new PreCalculusBusiness();


        [HttpGet]
        [Route("series1")]
        public IActionResult Get()
        {
            int addNumber = new Random().Next(11) - 5;
            int opNumber = new Random().Next(11) - 5;
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
            Dictionary<String, Object> questionInfo = new Dictionary<String, Object>();
            questionInfo["question"] = question;
            questionInfo["solution"] = solution;
            questionInfo["options"] = options;
            return Ok(JsonSerializer.Serialize(questionInfo));
        }


    }
}
