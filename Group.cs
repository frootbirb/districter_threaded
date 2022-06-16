using System.Collections.Generic;
using System.Linq;

internal class Group
{
    public double metric => units.Sum(unit => unit.metric);
    public HashSet<Unit> units = new HashSet<Unit>();
    private readonly State state;
    public int index => state.groups.IndexOf(this);
    public IEnumerable<string> adjacent =>
        units
            .SelectMany(unit => unit.adjacent)
            .Distinct()
            .Where(code => !units.Any(unit => unit.code == code));

    public Group(State state)
    {
        this.state = state;
    }

    public bool CanLose(Unit unit)
    {
        if (!units.Contains(unit))
        {
            return true;
        }

        // Don't allow stealing the last unit from a group
        if (units.Count() == 1)
        {
            return false;
        }

        List<Unit> border = units.Where(u => unit.adjacent.Contains(u.code)).ToList();

        if (!border.Any())
        {
            return true;
        }

        Queue<Unit> queue = new Queue<Unit>();
        queue.Enqueue(border[0]);
        while (queue.Any())
        {
            Unit next = queue.Dequeue();
            border.Remove(next);
            foreach (Unit u in border.Where(u => next.adjacent.Contains(u.code)))
            {
                queue.Enqueue(u);
            }
        }

        return !border.Any();
    }

    public int DistanceTo(Unit unit) => -units.Where(u => unit.adjacent.Contains(u.code)).Count();

    public IEnumerable<Unit> placeable =>
        (
            adjacent.Any()
                ? adjacent.Select(code => state.unitdict[code]).Concat(state.noAdjacent)
                : state.unitlist
        ).Where(unit => unit.CanBePlacedIn(this) && unit.CanBePlaced());

    public void Print(Group group = null)
    {
        state.PrintList(units, $"{index} ({metric:.###})", group);
    }
}
