namespace MathCoursesCS.Business
{
    public class PreCalculusBusiness
    {
        public PreCalculusBusiness()
        {

        }

        // given a number create a series of 5 continuous number that must include the parameter received
        public List<int> getContinuousOptions(int Solution)
        {
            int initial = new Random().Next(6);
            List<int> options = Enumerable.Range(Solution-initial, 5).ToList();
            return options;
        }

        // given a number create a series of 5 continuous number that must include the parameter received
        public List<String> getContinuousOptions(double Solution)
        {
            int seed = new Random().Next(6);
            double initial = Solution - seed;
            List<String> options = Enumerable.Range(0, 5).Select((int i)=> Convert.ToString(initial + i)).ToList();
            return options;
        }

        public class convergentOption
        {
            public string function="";
            public string limit="";
            public convergentOption(string nFunction, string nLimit) {
                function = nFunction;
                limit = nLimit;
            }
        }
    }
}
