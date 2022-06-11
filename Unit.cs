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

    public HashSet<string> adjacent;

    private readonly Dictionary<string, double> _metrics;
    internal IEnumerable<string> keys => _metrics.Keys;
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

    public Unit(Unit other)
    {
        this.code = other.code;
        this.name = other.name;
        this._metrics = other._metrics;
        this.adjacent = other.adjacent;
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
        Console.WriteLine($"{code}: {group?.index ?? -1}");
        foreach (KeyValuePair<string, double> entry in _metrics)
        {
            Console.WriteLine("  {0} {1}", entry.Key, entry.Value);
        }
        Console.WriteLine("");
    }
}
