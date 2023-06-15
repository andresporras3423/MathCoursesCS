namespace MathCoursesCS.Business
{
    public class PreCalculusBusiness
    {
        public PreCalculusBusiness()
        {

        }

        public List<int> getContinuousOptions(int Solution)
        {
            int initial = new Random().Next(6);
            List<int> options = Enumerable.Range(Solution-initial, 5).ToList();
            return options;
        }
    }
}
