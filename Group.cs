using System.Collections.Generic;
using System.Linq;

internal class Group
{
    public double metric => units.Sum(unit => unit.metric);
    public HashSet<Unit> units = new HashSet<Unit>();
    private readonly State state;
    public int index => state.groups.IndexOf(this);
    public HashSet<string> adjacent =>
        new HashSet<string>(
            units.SelectMany(unit => unit.adjacent).Where(code => !units.Any(u => u.code == code))
        );

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

    public IEnumerable<Unit> PlaceableIn()
    {
        return state.unitlist
            .Where(unit => unit.CanBePlacedIn(this) && unit.CanBePlaced())
            .OrderBy(u => u.group != null)
            .ThenBy(u => metric + u.metric < state.maxAcceptableMetric)
            .ThenBy(u => u.group?.metric ?? 0)
            .ThenBy(u => u.metric);
    }

    public void Print(Group group = null)
    {
        state.PrintList(units, group);
    }
}
