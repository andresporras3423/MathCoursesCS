using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MathCoursesCS.Business
{
    public class GlobalFunctionBusiness
    {
        public GlobalFunctionBusiness()
        {

        }

        public string addPlus(int number)
        {
            if (number >= 0)
            {
                return "+" + Convert.ToString(number);
            }
            else
            {
                return Convert.ToString(number);
            }
        }

        public string addPlus(double number)
        {
            if (number >= 0)
            {
                return "+" + Convert.ToString(number);
            }
            else
            {
                return Convert.ToString(number);
            }
        }

        public string jsonResponse(String question, String solution, List<String> options)
        {
            Dictionary<String, Object> questionInfo = new Dictionary<String, Object>();
            questionInfo["question"] = question;
            questionInfo["solution"] = solution;
            questionInfo["options"] = options;
            //return Ok(JsonSerializer.Serialize("4+ln(5x)"));
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(questionInfo, jsonOptions);
            return json;
        }

        public string jsonResponse(String question, int solution, List<String> options)
        {
            String strSolution = Convert.ToString(solution);
            return jsonResponse( question, strSolution, options);
        }

        public string jsonResponse(String question, String solution, List<int> options)
        {
            List<string> strOptions = options.Select(opt => Convert.ToString(opt)).ToList();
            return jsonResponse(question, solution, strOptions);
        }

        public string jsonResponse(String question, int solution, List<int> options)
        {
            String strSolution = Convert.ToString(solution);
            List<string> strOptions = options.Select(opt => Convert.ToString(opt)).ToList();
            return jsonResponse(question, strSolution, strOptions);
        }
    }
}
