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
    }
}
