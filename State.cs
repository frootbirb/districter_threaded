using System;
using System.Collections.Generic;
using System.Linq;

class State
{
    public List<Unit> unitlist;
    public IEnumerable<Unit> unplaced => unitlist.Where(u => u.group == null).ToList();
    public readonly List<Group> groups;
    public readonly double maxAcceptableMetric,
        minAcceptableMetric;

    public State(int numDist)
    {
        unitlist = new List<Unit>(Globals.unitlist);
        groups = new List<Group>(from i in Enumerable.Range(0, numDist) select new Group(this));

        double sumMetrics = unitlist.Sum(u => u.metric);
        double avgMetric = sumMetrics / unitlist.Count;
        double biggestMetric = unitlist.Max(u => u.metric);
        maxAcceptableMetric = Math.Max(avgMetric * 1.05, biggestMetric);
        minAcceptableMetric = avgMetric * 0.95;
    }

    public void DoStep()
    {
        Group group = groups.MinBy(g => g.metric);
        Console.WriteLine(groups.IndexOf(group) + ":");
        Print(group);
        Unit unit = group.PlaceableIn().First();
        Console.WriteLine("  " + unit + "\n");
        unit.group = group;
    }

    public bool isDone()
    {
        return !unplaced.Any();
    }

    public void PrintList(IEnumerable<Unit> list, Group group = null)
    {
        Console.Write(" [");
        foreach (Unit u in list)
        {
            if (u.CanBePlacedIn(group) && u.CanBePlaced())
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (u.CanBePlacedIn(group))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (!u.CanBePlaced())
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(u);
            Console.ResetColor();

            if (u != list.Last())
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
    }

    public void Print(Group group = null)
    {
        groups.ForEach(g => g.Print(group));
        PrintList(unplaced, group);
    }
}