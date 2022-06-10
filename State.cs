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
        groups.ForEach(g => g.Print(group));
        PrintList(unplaced, group);
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
',  \  NV | UT |  CO   |~~~~~~~|    \  | ,'~~\WV/ VA |
 | CA\    |    |       |  KA   | MO  \_-~ KY /`~___--\
 ',   \  ,-----|-------+-------'_____/__----~~/  NC  /
  '_   '\|     |      |~~~| OK  |    |  TN  _/-,~~-,/
    \    | AZ  | NM   |   |_    | AK /~~|~~\    \,/SC
     ~~~-'     |      |     `~~~\___|MS |AL | GA /
         '-,_  | _____|          |  /   | ,-'---~\
             `~'~  \     TX      |LA`--,~~~~-~~,FL\
                    \/~\      /~~~`---`         |  \
                        \    /                   \  |
                         \  |                     '\'
                          `~'";

    public void PrintCode(string code)
    {
        int groupIndex = groups.IndexOf(unitlist.Where(u => u.code == code).FirstOrDefault().group);
        switch(groups.IndexOf(unitlist.Where(u => u.code == code).FirstOrDefault().group))
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case 5:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
        }
        Console.Write(code);
        Console.ResetColor();
    }

    public void PrintMap()
    {
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
        Console.Write(
            @" |~~~~~~~~|  ");
        PrintCode("SD");
        Console.Write(@"  \    \   | | '~\  |_____,|~,-' ");
        PrintCode("MA");
        Console.Write(@"
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
        Console.Write(
            @" |
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
        PrintCode("AK");
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
                         \  |                     '\'
                          `~'"
        );
    }
}