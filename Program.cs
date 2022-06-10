using System;
using System.Linq;

namespace districter_threaded
{
    class Program
    {
        static void Main(string[] args)
        {
            Globals._read();
            State state = Program.Solve(2, "Population", "states");

            state.groups.ForEach(g => g.Print());
            Console.WriteLine(state.groups.Sum(g => g.units.Count));
            state.PrintMap();
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
