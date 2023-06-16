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


        
        // convert the int parameter to a double and the pass it to the addPlus function and return the given string for the function.
        public string addPlus(int number)
        {
            Double dblNumber = number;
            return addPlus(dblNumber);
        }

        // if the given double parameter is positive then add a plus sign at the start
        // return a string
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

        // convert the string parameter to a double and the pass it to the addPlus function and return the given string for the function.
        public string addPlus(string number)
        {
            Double dblNumber = Convert.ToDouble(number);
            return addPlus(dblNumber);
        }

        // this function receives two string parameters (question, solution) and one list<string> parameter (options)
        // add the three parameters to a Dictionary of string, object
        // use respectively the keys: question, solution, options
        // convert the dictionary to a json string and return the json.
        public string jsonResponse(String question, String solution, List<String> options)
        {
            Dictionary<String, Object> questionInfo = new Dictionary<String, Object>();
            questionInfo["question"] = question;
            questionInfo["solution"] = solution;
            questionInfo["options"] = options;
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
