namespace districter_threaded
{
    class Program
    {
        static void Main(string[] args)
        {
            //Program.Solve(2, "Population", "counties").PrintMap();
            Program.UnitTest(7);
        }

        static public State Solve(int numGroups, string metricID, string scale)
        {
            Globals.scale = scale;
            Globals.metricID = metricID;
            State state = new State(numGroups);
            while (state.DoStep());

            return state;
        }

        static private void UnitTest(int maxCount)
        {
            // foreach (string scale in Globals.scales)
            foreach (string scale in new string[]{"states"})
            {
                Globals.scale = scale;
                for (int numGroups = 1; numGroups <= maxCount; numGroups++)
                {
                    foreach (string metricID in Globals.metricIDs)
                    {
                        Globals.metricID = metricID;
                        State state = Program.Solve(numGroups, metricID, scale);
                        state.PrintMap();
                        state.Print();
                        System.Console.ReadKey();
                    }
                }
            }
        }
    }
}
