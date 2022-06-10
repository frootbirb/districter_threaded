using System;
using System.Collections.Generic;
using System.Linq;

internal class Unit
{
    public readonly string code;
    public readonly string name;

    private Group _group;
    private Group lastGroup;
    public Group group
    {
        get { return _group; }
        set
        {
            _group?.units.Remove(this);
            lastGroup = _group;
            _group = value;
            _group.units.Add(this);
        }
    }

    // todo public readonly Dictionary<Unit, int> distances;
    public HashSet<string> adjacent;

    private readonly Dictionary<string, double> _metrics;
    public double metric => _metrics[Globals.metricID];

    public Unit(
        string code,
        string name,
        Dictionary<string, double> metrics,
        HashSet<string> adjacent
    )
    {
        this.code = code;
        this.name = name;
        _metrics = metrics;
        this.adjacent = adjacent;
    }

    public override string ToString() => code;

    public bool CanBePlaced() => group?.CanLose(this) ?? true;

    public bool CanBePlacedIn(Group g) =>
        g is not null
        && g != this.group
        && g != this.lastGroup
        && (!g.adjacent.Any() || g.adjacent.Contains(this.code) || !adjacent.Any());

    public void Print()
    {
        Console.WriteLine(code);
        foreach (KeyValuePair<string, double> entry in _metrics)
        {
            Console.WriteLine("  {0} {1}", entry.Key, entry.Value);
        }
        Console.WriteLine("");
    }
}
