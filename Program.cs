using System;
using System.Linq;

namespace districter_threaded
{
    class Program
    {
        static void Main(string[] args)
        {
            Globals._read();
            State state = Program.Solve(3, "Area (mi2)", "states");

            state.groups.ForEach(g => g.Print());
            Console.WriteLine(state.groups.Sum(g => g.units.Count));
        }

        static public State Solve(int numGroups, string metricID, string scale) 
        {
            Globals.scale = scale;
            Globals.metricID = metricID;
            State state = new State(numGroups);
            while (!state.isDone())
            {
                state.DoStep();
            }

            return state;
        }
    }
}
