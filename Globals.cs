using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Reflection;

public static class Globals
{
    static public string metricID;
    static public string scale;

    static private Dictionary<string, Dictionary<string, Unit>> _unitlists;
    static private Dictionary<string, Dictionary<string, Unit>> unitlists
    {
        get { return _unitlists ??= read(); }
    }
    static internal Dictionary<string, Unit> unitlist => unitlists[scale];

    static public IEnumerable<string> scales => unitlists.Keys;
    // Get the values from the list
    static public IEnumerable<string> metricIDs => unitlist.First().Value.keys;

    static private Dictionary<string, Unit> read_data(string resource)
    {
        Dictionary<string, Unit> unitlist = new Dictionary<string, Unit>();
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
        using (StreamReader reader = new StreamReader(stream))
        {
            List<string> vals,
                keys = null;
            while ((vals = reader.ReadLine()?.Split(",").ToList()) != null)
            {
                if (keys == null)
                {
                    keys = vals.Skip(3).ToList();
                    continue;
                }

                unitlist[vals[0]] = new Unit(
                    vals[0],
                    vals[1],
                    keys.Zip(vals.Skip(3))
                        .ToDictionary(x => x.First, x => Convert.ToDouble(x.Second)),
                    new HashSet<string>(vals[2].Split('|', StringSplitOptions.RemoveEmptyEntries))
                );
            }
        }
        return unitlist;
    }

    static private Dictionary<string, Dictionary<string, Unit>> read()
    {
        Dictionary<string, Dictionary<string, Unit>> unitlists =
            new Dictionary<string, Dictionary<string, Unit>>();
        foreach (string resource in Assembly.GetExecutingAssembly().GetManifestResourceNames())
        {
            List<string> meta = resource.Split(".").ToList();
            List<string> resourcename = meta[meta.Count - 2].Split("_").ToList();
            string scale = resourcename[0];
            string type = resourcename[1];
            switch (type)
            {
                case "data":
                    unitlists[scale] = read_data(resource);
                    break;
            }
        }

        return unitlists;
    }
}
