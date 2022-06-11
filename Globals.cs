using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public static class Globals
{
    static public string metricID;
    static public string scale;

    static private Dictionary<string, List<Unit>> _unitlists = new Dictionary<string, List<Unit>>();
    static internal List<Unit> unitlist => _unitlists[scale];

    static public IEnumerable<string> scales => _unitlists.Keys;
    static public IEnumerable<string> metricIDs => unitlist[0].keys;

    static private void _read_data(string scale, string filename)
    {
        _unitlists[scale] = new List<Unit>();
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

                _unitlists[scale].Add(
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
    }

    static internal void read()
    {
        string directory = Directory.GetCurrentDirectory() + @"\assets";
        foreach (string filename in Directory.GetFiles(directory))
        {
            List<string> meta = Path.GetFileNameWithoutExtension(filename).Split("_").ToList();
            string scale = meta[0];
            string type = meta[1];
            switch (type)
            {
                case "data":
                    _read_data(scale, filename);
                    break;
            }
        }
    }
}
