using MathCoursesCS.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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


        [HttpGet]
        [Route("convergent_series_problem")]
        public IActionResult convergentSeriesProblem()
        {
            int problem = new Random().Next(4);
            int a = (new Random().Next(6) + 1) * ((new Random().Next(2) * 2) - 1);
            String question = "";
            String solution = "";

            if (problem == 0)
            {
                question = "for de next function: tan(x), defines the function type when x tends to "+Convert.ToString(a)+ "*π/2";
                solution = a % 2 == 0 ? "convergent" : "divergent";
            }
            else if (problem == 1)
            {
                question = "for de next function: cot(x), defines the function type when x tends to " + Convert.ToString(a) + "*π/2";
                solution = a % 2 != 0 ? "convergent" : "divergent";
            }
            else if (problem == 2) 
            {
                question = "for de next function: sec(x), defines the function type when x tends to " + Convert.ToString(a) + "*π/2";
                solution = a % 2 == 0 ? "convergent" : "divergent";
            }
            else
            {
                question = "for de next function: csc(x), defines the function type when x tends to " + Convert.ToString(a) + "*π/2";
                solution = a % 2 != 0 ? "convergent" : "divergent";
            }
            List<string> options = new List<string> { "divergent", "convergent" };
            return Content(gf.jsonResponse(question, solution, options), "application/json");
        }


        // the next problem is deduce from the next principle:
        // Sum from 1 to n = n*(n+1)/2
        // therefore: Sum of the first n terms in a+bx will be = a*n + b*n*(n+1)/2
        // a*n since a is a constant
        // b*n*(n+1)/2 because sum of the first n terms in bx is just the sum of the first n terms in x but multiply for b.
        [HttpGet]
        [Route("gauss_series_problem")]
        public IActionResult gaussSeriesProblem()
        {
            int a = new Random().Next(51) + 50;
            int b = (new Random().Next(6) + 1) * ((new Random().Next(2) * 2) - 1);
            int c = (new Random().Next(6) + 1) * ((new Random().Next(2) * 2) - 1);
            string question = "Find the sum of the first "+Convert.ToString(a)+" terms in the function: " + Convert.ToString(b) + gf.addPlus(c)+ "x";
            int solution = (b*a) + (c * a * (a + 1) / 2);
            List<int> options = pb.getContinuousOptions(solution);
            return Content(gf.jsonResponse(question, solution, options), "application/json");
        }





        // Sum from 1 to n = n*(n+1)/2
        [HttpGet]
        [Route("simple_gauss_problem")]
        public IActionResult simpleGaussProblem()
        {
            int a = new Random().Next(151) + 50;
            string question = "Find the sum of the first " + Convert.ToString(a) + " terms";
            int solution = a * (a + 1) / 2;
            List<int> options = pb.getContinuousOptions(solution);
            return Content(gf.jsonResponse(question, solution, options), "application/json");
        }



        // the next problem is deduce from the next principle:
        // Sum from 1 to n of a^x = 1 + a + a^2 + ... + a^n = S
        // aS =  a + a^2 + ... + a^(n+1)
        // aS-S = 1-a^(n+1)
        // S(a-1) = 1-a^(n+1)
        // S= (1-a^(n+1))/(a-1)
        // link explaining this series: https://www.youtube.com/watch?v=ZBagdQmAdQw&ab_channel=MateFacil
        [HttpGet]
        [Route("geometric_series_problem")]
        public IActionResult geometricSeriesProblem()
        {
            int a = (new Random().Next(6) + 5);
            int b = (new Random().Next(5) + 1);
            int c = (new Random().Next(5) + 1);
            int d = (new Random().Next(5) + 1);
            string question = "Find the sum of the first " + Convert.ToString(a) + " terms in the function: " + Convert.ToString(b) + "+(" + Convert.ToString(c) + "/" + Convert.ToString(d) + ")^x";
            double solution = (a * b) + (1 - Math.Pow(c / d, a + 1)) / (1 - a);
            List<string> options = pb.getContinuousOptions(solution);
            return Content(gf.jsonResponse(question, Convert.ToString(solution), options), "application/json");
        }





        [HttpGet]
        [Route("n_arithmetic_series_problem")]
        public IActionResult nArithmeticSeriesProblem()
        {
            int a = (new Random().Next(91) + 10);
            int b = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int c = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int sol = b + (c * a);
            List<int> series = new List<int>();
            for (int i = 1; i <= 5; i++)
            {
                series.Add(b + (c * i));
            }
            // question="The first five element of a series are "+str(items[0])+","+str(items[1])+","+str(items[2])+","+str(items[3])+","+str(items[4])+", which is the value of the "+str(a)+"th item?:"
            string question = "The first five element of a series (from 1th to 5th) are: "+String.Join(", ",series)+ ". Which is the value of the "+Convert.ToString(a)+"th item?";
            List<int> options = pb.getContinuousOptions(sol);
            return Content(gf.jsonResponse(question, Convert.ToString(sol), options), "application/json");
        }





        
        [HttpGet]
        [Route("n_geometric_series_problem")]
        public IActionResult nGeometricSeriesProblem()
        {
            int a = (new Random().Next(6) + 10);
            int b = (new Random().Next(5) + 2) * ((new Random().Next(2) * 2) - 1);
            int c = (new Random().Next(5) + 2) * ((new Random().Next(2) * 2) - 1);
            double sol = b + Math.Pow(c,a);
            List<double> series = new List<double>();
            for (int i = 1; i <= 5; i++)
            {
                series.Add(b + Math.Pow(c, i));
            }
            // question="The first five element of a series are "+str(items[0])+","+str(items[1])+","+str(items[2])+","+str(items[3])+","+str(items[4])+", which is the value of the "+str(a)+"th item?:"
            string question = "The first five elements of a series (from 1th to 5th) are: " + String.Join(", ", series) + ". Which is the value of the " + Convert.ToString(a) + "th item?";
            List<string> options = pb.getContinuousOptions(sol);
            return Content(gf.jsonResponse(question, Convert.ToString(sol), options), "application/json");
        }




// get the product of the first n (from 0 to a) numbers of the geometric series
// clue: the product of the first a items in b*(c^x) is (b^(a+1))*(c^d) where d is the sum from 0 to a
        [HttpGet]
        [Route("geometric_product_series_problem")]
        public IActionResult geometricProductSeriesProblem()
        {
            int a = (new Random().Next(2) + 4); //
            int b = (new Random().Next(5) + 2) * ((new Random().Next(2) * 2) - 1);
            int c = (new Random().Next(2) + 2) * ((new Random().Next(2) * 2) - 1);
            int d = Enumerable.Range(1, a).Sum();
            double sol = Math.Pow(b, a + 1) * Math.Pow(c , d);
            List<double> series = new List<double>();
            for (int i = 0; i < 3; i++)
            {
                series.Add(b * Math.Pow(c, i));
            }
            // question="The first five element of a series are "+str(items[0])+","+str(items[1])+","+str(items[2])+","+str(items[3])+","+str(items[4])+", which is the value of the "+str(a)+"th item?:"
            string question = "The first 3 elements of a geometric series are: " + String.Join(", ", series) + ". Which is the value of the product of the first " + Convert.ToString(a+1) + " elements?";
            List<string> options = pb.getContinuousOptions(sol);
            return Content(gf.jsonResponse(question, Convert.ToString(sol), options), "application/json");
        }


        //# find limit when x->a for the function  (x^2-a^2)/(x-a) + b 
        //        def simpleLimitProblem():
        //    try:
        //        a = random.randint(1,10)*(random.randint(0,1)*2-1)
        //        b = random.randint(1,10)*(random.randint(0,1)*2-1)
        //        solution=(2*a)+b
        //        question = "[lim x->(" + str(a) + ")] for ((x^2)+(" + str(b) + "*x)+(" + str((-a * b) - (a * *2)) + "))/(x-(" + str(a) + "))"
        //        options=coursesFunctionsBll.arithmeticAlternatives(solution)
        //        jsonResponse = json.dumps({"question":coursesFunctionsBll.replaceSpace(question), "solution":coursesFunctionsBll.replaceSpace(solution), "options":coursesFunctionsBll.replaceOptions(options)})
        //        return [jsonResponse]
        //    except Exception as er:
        //        return er
        [HttpGet]
        [Route("simple_limit_problem")]
        public IActionResult simpleLimitProblem()
        {
            int a = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int b = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int sol = (a * 2) + b;
            // question="The first five element of a series are "+str(items[0])+","+str(items[1])+","+str(items[2])+","+str(items[3])+","+str(items[4])+", which is the value of the "+str(a)+"th item?:"
            string question = "Lim x->("+a.ToString()+") (x^2-"+(a*a).ToString()+")/(x"+gf.addPlus(-1*a)+")"+ gf.addPlus(b);
            List<int> options = pb.getContinuousOptions(sol);
            return Content(gf.jsonResponse(question, Convert.ToString(sol), options), "application/json");
        }

        // (x-a)(x-b)/(x-a)
        // (x^2-ax-bx+ab)/(x-a)
        // (x^2-(a+b)x+ab)/(x-a)
        [HttpGet]
        [Route("simple_limit_problem2")]
        public IActionResult simpleLimitProblem2()
        {
            int a = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int b = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int sol = a-b;
            // question="The first five element of a series are "+str(items[0])+","+str(items[1])+","+str(items[2])+","+str(items[3])+","+str(items[4])+", which is the value of the "+str(a)+"th item?:"
            string question = "Lim x->(" + a.ToString() + ") (x^2" +gf.noOneAllowed(gf.addPlus(-a - b))+"x"  + gf.addPlus(b * a) + ")/(x" + gf.addPlus(-a)+")";
            List<int> options = pb.getContinuousOptions(sol);
            return Content(gf.jsonResponse(question, Convert.ToString(sol), options), "application/json");
        }


        // find limit when x->inf for the function  (ax+b)/(cx+d) + (ex+f)/(gx+h)
        [HttpGet]
        [Route("simple_infinite_limit")]
        public IActionResult simpleInfiniteLimit()
        {
            int a = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int b = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int c = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int d = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int e = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int f = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int g = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            int h = (new Random().Next(10) + 1) * ((new Random().Next(2) * 2) - 1);
            double sol = Math.Round((Convert.ToDouble(a)/c)+(Convert.ToDouble(e)/g),4);
            // question="The first five element of a series are "+str(items[0])+","+str(items[1])+","+str(items[2])+","+str(items[3])+","+str(items[4])+", which is the value of the "+str(a)+"th item?:"
            string question = @"Lim x-> ∞ (" + gf.noOneAllowed(a) + "x" + gf.addPlus(b) + ")/(" + gf.noOneAllowed(c) + "x" + gf.addPlus(d) + ")+(" + gf.noOneAllowed(e) + "x" + gf.addPlus(f) + ")/(" + gf.noOneAllowed(g) + "x" + gf.addPlus(h) + ")";
            List<string> options = pb.getContinuousOptions(sol);
            return Content(gf.jsonResponse(question, Convert.ToString(sol), options), "application/json");
        }
    }
}

