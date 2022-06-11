using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

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

    static private List<Unit> read_data(string filename)
    {
        List<Unit> unitlist = new List<Unit>();
        using (StreamReader reader = new StreamReader(filename))
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
        string directory = Directory.GetCurrentDirectory() + @"\assets";
        foreach (string filename in Directory.GetFiles(directory))
        {
            List<string> meta = Path.GetFileNameWithoutExtension(filename).Split("_").ToList();
            string scale = meta[0];
            string type = meta[1];
            switch (type)
            {
                case "data":
                    unitlists[scale] = read_data(filename);
                    break;
            }
        }

        return unitlists;
    }
}
