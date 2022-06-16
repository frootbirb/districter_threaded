//#define PRINT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class State
{
    public IEnumerable<Unit> unitlist => unitdict.Select(entry => entry.Value);
    private Dictionary<string, Unit> unitdict;
    public IEnumerable<Unit> unplaced => unitlist.Where(u => u.group == null);
    public readonly List<Group> groups;
    public readonly double maxAcceptableMetric,
        minAcceptableMetric;

    public State(int numGroups)
    {
        unitdict = Globals.unitlist.ToDictionary(
            entry => entry.Key,
            entry => new Unit(entry.Value)
        );
        groups = new List<Group>(from i in Enumerable.Range(0, numGroups) select new Group(this));

        double sumMetrics = unitlist.Sum(u => u.metric);
        double avgMetric = sumMetrics / numGroups;
        double biggestMetric = unitlist.Max(u => u.metric);
        maxAcceptableMetric = Math.Max(avgMetric * 1.05, biggestMetric);
        minAcceptableMetric = avgMetric * 0.95;

        Console.WriteLine(
            $"Initialized for {numGroups} groups of {Globals.scale}, sorted by {Globals.metricID}"
        );
    }

    public class UnitSorter : IComparer<Unit>
    {
        private readonly Group g;

        internal UnitSorter(Group g)
        {
            this.g = g;
        }

        public int Compare(Unit a, Unit b)
        {
            int aadj,
                badj;
            double ametric,
                bmetric;
            // If the groups are not both null, the one that is is sorted higher
            if ((a.group == null) != (b.group == null))
            {
                return a.group == null ? 1 : -1;
            }
            else if (
                (aadj = g.units.Where(u => a.adjacent.Contains(u.code)).Count())
                != (badj = g.units.Where(u => b.adjacent.Contains(u.code)).Count())
            )
            {
                return aadj.CompareTo(badj);
            }
            else if ((ametric = a.group?.metric ?? 0) != (bmetric = b.group?.metric ?? 0))
            {
                return bmetric.CompareTo(ametric);
            }
            else
            {
                return a.metric.CompareTo(b.metric);
            }
        }
    }

    public bool DoStep()
    {
        Group group = groups
            .OrderBy(g => g.units.Any())
            .ThenBy(g => !g.adjacent.Any(u => unitdict[u].group == null))
            .ThenBy(g => g.metric)
            .First();
#if PRINT
        Console.WriteLine($"Adding to {group.index}:");
        PrintList(group.placeable.OrderBy(u => u, new UnitSorter(group)), "placeable");
        Print(group);
#endif
        Unit unit = group.placeable.MaxBy(u => u, new UnitSorter(group));
        if (unit != null)
        {
#if PRINT
            Console.WriteLine($"  Selected: {unit}");
            PrintMap();
            Console.WriteLine("");
            Console.ReadKey();
#endif
            unit.group = group;
            return !isDone();
        }

#if PRINT
        Console.WriteLine("");
#endif
        return false;
    }

    public bool isDone()
    {
        return !unplaced.Any() && groups.All(g => g.metric > minAcceptableMetric);
    }

    public void PrintList(IEnumerable<Unit> list, string name, Group group = null)
    {
        if (Char.IsDigit(name, 0))
        {
            PrintInColor($"{name, 12}", Convert.ToInt32(Regex.Match(name, @"^\d+").Value));
        }
        else
        {
            Console.Write($"{name, 12}");
        }
        Console.Write(": [");
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
            else if (group != null && !u.CanBePlaced())
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
        foreach (Group g in groups.OrderBy(g => g.metric).Reverse())
        {
            g.Print(group);
        }
        PrintList(unplaced, "unplaced", group);
    }

    const string map =
        @"         ,__                                                  _,
 \~\|  ~~---___              ,                          | \
  | WA   / |   ~~~~~~~|~~~~~| ~~---,                VT_/,ME>
 /~-_--__| |     MT   | ND  \  MN / ~\~~/ MI       /~| ||,'
 |  OR   /  \         |------|   { WI / /~)     __-NY',|_\,NH
/       | ID |~~~~~~~~|  SD  \    \   | | '~\  |_____,|~,-' MA
|~~--__ |    |   WY   |____  |~~~~~|--| |__ /_-' PA .{,~ CO  (RI)
|   |  ~~~|~~|        |    ~~\  IA /  `-' |`~ |~_____{/NJ
|   |     |  '---------,  NE  \----| IL|IN|OH,' ~/~\,|`MD (DE)
',  \  NV | UT |  CO   |~~~~~~~|    \  | ,'~~\WV/ VA |    (DC)
 | CA\    |    |       |  KA   | MO  \_-~ KY /`~___--\
 ',   \  ,-----|-------+-------'_____/__----~~/  NC  /
  '_   '\|     |      |~~~| OK  |    |  TN  _/-,~~-,/
    \    | AZ  | NM   |   |_    | AK /~~|~~\    \,/SC
     ~~~-'     |      |     `~~~\___|MS |AL | GA /
         '-,_  | _____|          |  /   | ,-'---~\
             `~'~  \     TX      |LA`--,~~~~-~~,FL\
                    \/~\      /~~~`---`         |  \
                        \    /                   \  |
      AK      HI         \  |                     '\'
                          `~'";

    private void PrintCode(string code)
    {
        PrintInColor(code, unitlist.Where(u => u.code == code).FirstOrDefault().group?.index ?? -1);
    }

    private void PrintInColor(string val, int group)
    {
        switch (group)
        {
            case 0:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case 1:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case 4:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case 5:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            default:
                break;
        }
        Console.Write(val);
        Console.ResetColor();
    }

    public void PrintMap()
    {
        if (Globals.scale != "states")
        {
            return;
        }

        Console.Write(
            @"         ,__                                                  _,
 \~\|  ~~---___              ,                          | \
  | "
        );
        PrintCode("WA");
        Console.Write(@"   / |   ~~~~~~~|~~~~~| ~~---,                ");
        PrintCode("VT");
        Console.Write(@"_/,");
        PrintCode("ME");
        Console.Write(
            @">
 /~-_--__| |     "
        );
        PrintCode("MT");
        Console.Write(@"   | ");
        PrintCode("ND");
        Console.Write(@"  \  ");
        PrintCode("MN");
        Console.Write(@" / ~\~~/ ");
        PrintCode("MI");
        Console.Write(
            @"       /~| ||,'
 |  "
        );
        PrintCode("OR");
        Console.Write(@"   /  \         |------|   { ");
        PrintCode("WI");
        Console.Write(@" / /~)     __-");
        PrintCode("NY");
        Console.Write(@"',|_\,");
        PrintCode("NH");
        Console.Write(
            @"
/       | "
        );
        PrintCode("ID");
        Console.Write(@" |~~~~~~~~|  ");
        PrintCode("SD");
        Console.Write(@"  \    \   | | '~\  |_____,|~,-' ");
        PrintCode("MA");
        Console.Write(
            @"
|~~--__ |    |   "
        );
        PrintCode("WY");
        Console.Write(@"   |____  |~~~~~|--| |__ /_-' ");
        PrintCode("PA");
        Console.Write(@" .{,~ ");
        PrintCode("CT");
        Console.Write(@"  (");
        PrintCode("RI");
        Console.Write(
            @")
|   |  ~~~|~~|        |    ~~\  "
        );
        PrintCode("IA");
        Console.Write(@" /  `-' |`~ |~_____{/");
        PrintCode("NJ");
        Console.Write(
            @"
|   |     |  '---------,  "
        );
        PrintCode("NE");
        Console.Write(@"  \----| ");
        PrintCode("IL");
        Console.Write(@"|");
        PrintCode("IN");
        Console.Write(@"|");
        PrintCode("OH");
        Console.Write(@",' ~/~\,|`");
        PrintCode("MD");
        Console.Write(@" (");
        PrintCode("DE");
        Console.Write(
            @")
',  \  "
        );
        PrintCode("NV");
        Console.Write(@" | ");
        PrintCode("UT");
        Console.Write(@" |  ");
        PrintCode("CO");
        Console.Write(@"   |~~~~~~~|    \  | ,'~~\");
        PrintCode("WV");
        Console.Write(@"/ ");
        PrintCode("VA");
        Console.Write(@" |    (");
        PrintCode("DC");
        Console.Write(
            @")
 | "
        );
        PrintCode("CA");
        Console.Write(@"\    |    |       |  ");
        PrintCode("KS");
        Console.Write(@"   | ");
        PrintCode("MO");
        Console.Write(@"  \_-~ ");
        PrintCode("KY");
        Console.Write(
            @" /`~___--\
 ',   \  ,-----|-------+-------'_____/__----~~/  "
        );
        PrintCode("NC");
        Console.Write(
            @"  /
  '_   '\|     |      |~~~| "
        );
        PrintCode("OK");
        Console.Write(@"  |    |  ");
        PrintCode("TN");
        Console.Write(
            @"  _/-,~~-,/
    \    | "
        );
        PrintCode("AZ");
        Console.Write(@"  | ");
        PrintCode("NM");
        Console.Write(@"   |   |_    | ");
        PrintCode("AR");
        Console.Write(@" /~~|~~\    \,/");
        PrintCode("SC");
        Console.Write(
            @"
     ~~~-'     |      |     `~~~\___|"
        );
        PrintCode("MS");
        Console.Write(@" |");
        PrintCode("AL");
        Console.Write(@" | ");
        PrintCode("GA");
        Console.Write(
            @" /
         '-,_  | _____|          |  /   | ,-'---~\
             `~'~  \     "
        );
        PrintCode("TX");
        Console.Write(@"      |");
        PrintCode("LA");
        Console.Write(@"`--,~~~~-~~,");
        PrintCode("FL");
        Console.Write(
            @"\
                    \/~\      /~~~`---`         |  \
                        \    /                   \  |
      "
        );
        PrintCode("AK");
        Console.Write(@"      ");
        PrintCode("HI");
        Console.Write(
            @"         \  |                     '\'
                          `~'"
        );
        Console.WriteLine("");
    }
}
