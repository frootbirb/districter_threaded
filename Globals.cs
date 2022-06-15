using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Reflection;

public static class Globals
{
    static public string metricID;
    static public string scale;

    static private Dictionary<string, List<Unit>> _unitlists;
    static private Dictionary<string, List<Unit>> unitlists
    {
        get { return _unitlists ??= read(); }
    }
    static internal List<Unit> unitlist => unitlists[scale];

    static public IEnumerable<string> scales => unitlists.Keys;
    static public IEnumerable<string> metricIDs => unitlist[0].keys;

    static private List<Unit> read_data(string resource)
    {
        List<Unit> unitlist = new List<Unit>();
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

                unitlist.Add(
                    new Unit(
                        vals[0],
                        vals[1],
                        keys.Zip(vals.Skip(3))
                            .ToDictionary(x => x.First, x => Convert.ToDouble(x.Second)),
                        new HashSet<string>(vals[2].Split('|'))
                    )
                );
            }
        }
        return unitlist;
    }

    static private Dictionary<string, List<Unit>> read()
    {
        Dictionary<string, List<Unit>> unitlists = new Dictionary<string, List<Unit>>();
        foreach (string resource in Assembly.GetExecutingAssembly().GetManifestResourceNames())
        {
            List<string> meta = resource.Split(".").ToList();
            List<string> resourcename = meta[meta.Count - 2].Split("_").ToList();
            string scale = resourcename[0];
            string type = resourcename[1];
            Console.WriteLine(resource);
            Console.WriteLine(scale + ": " + type);
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
