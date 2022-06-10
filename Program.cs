using System;
using System.Linq;

namespace districter_threaded
{
    class Program
    {
        static void Main(string[] args)
        {
            Globals._read();
            Program.solve("states", "Area (mi2)");
        }

        static public void solve(string scale, string metricID) 
        {
            Globals.scale = scale;
            Globals.metricID = metricID;
            State state = new State(2);
            while (!state.isDone()) 
            {
                state.doStep();
            }

            state.groups.ForEach(g => g.print());
            Console.WriteLine(state.groups.Sum(g => g.units.Count));
        }
    }
}
