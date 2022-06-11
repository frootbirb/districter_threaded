using System.Windows.Forms;

namespace districter_threaded
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.Solve(3, "Firearms", "states").PrintMap();
            //Program.UnitTest(5);
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
            foreach (string scale in Globals.scales)
            {
                Globals.scale = scale;
                for (int numGroups = 1; numGroups <= maxCount; numGroups++)
                {
                    foreach (string metricID in Globals.metricIDs)
                    {
                        Globals.metricID = metricID;
                        Program.Solve(numGroups, metricID, scale).PrintMap();
                    }
                }
            }
        }
    }
}
