using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Reflection;

public static class Globals
{
    static public string metricID;
    static public string scale;

    static private Dictionary<string, Dictionary<string, Unit>> _unitdicts;
    static private Dictionary<string, Dictionary<string, Unit>> unitdicts
    {
        get { return _unitdicts ??= read(); }
    }
    static internal Dictionary<string, Unit> unitdict => unitdicts[scale];

    static public IEnumerable<string> scales => unitdicts.Keys;
    // Get the values from the list
    static public IEnumerable<string> metricIDs => unitdict.First().Value.keys;

    static private Dictionary<string, Unit> read_data(string resource)
    {
        Dictionary<string, Unit> unitdict = new Dictionary<string, Unit>();
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

                unitdict[vals[0]] = new Unit(
                    vals[0],
                    vals[1],
                    keys.Zip(vals.Skip(3))
                        .ToDictionary(x => x.First, x => Convert.ToDouble(x.Second)),
                    new HashSet<string>(vals[2].Split('|', StringSplitOptions.RemoveEmptyEntries))
                );
            }
        }
        return unitdict;
    }

    static private Dictionary<string, Dictionary<string, Unit>> read()
    {
        Dictionary<string, Dictionary<string, Unit>> unitdicts =
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
                    unitdicts[scale] = read_data(resource);
                    break;
            }
        }

        return unitdicts;
    }
}
