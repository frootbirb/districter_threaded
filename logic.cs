using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace districter_threaded
{
    static class Globals
    {
        static public string metricID;
        static public string scale;

        static private Dictionary<string, List<Unit>> _unitlists =
            new Dictionary<string, List<Unit>>();
        static public List<Unit> unitlist => _unitlists[scale];

        static internal void _read()
        {
            _unitlists["states"] = new List<Unit>();

            _unitlists["states"].Add(
                new Unit(
                    "AL",
                    "Alabama",
                    new Dictionary<string, double>()
                    {
                        { "Population", 4908621 },
                        { "Firearms", 161641 },
                        { "Area (mi2)", 52420 },
                        { "Land (mi2)", 50645 },
                        { "GDP (4908621m)", 232145 },
                        { "Food ($1k)", 4103235 },
                        { "% Urban", 59.00 }
                    },
                    new HashSet<string>("MS|TN|GA|FL".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "AK",
                    "Alaska",
                    new Dictionary<string, double>()
                    {
                        { "Population", 734002 },
                        { "Firearms", 15824 },
                        { "Area (mi2)", 665384 },
                        { "Land (mi2)", 570641 },
                        { "GDP (734002m)", 55430 },
                        { "Food ($1k)", 52987 },
                        { "% Urban", 66.00 }
                    },
                    new HashSet<string>()
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "AZ",
                    "Arizona",
                    new Dictionary<string, double>()
                    {
                        { "Population", 7378494 },
                        { "Firearms", 179738 },
                        { "Area (mi2)", 113990 },
                        { "Land (mi2)", 113594 },
                        { "GDP (7378494m)", 368556 },
                        { "Food ($1k)", 3065603 },
                        { "% Urban", 89.80 }
                    },
                    new HashSet<string>("CA|NV|UT|NM".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "AR",
                    "Arkansas",
                    new Dictionary<string, double>()
                    {
                        { "Population", 3038999 },
                        { "Firearms", 79841 },
                        { "Area (mi2)", 53179 },
                        { "Land (mi2)", 52035 },
                        { "GDP (3038999m)", 134022 },
                        { "Food ($1k)", 6604400 },
                        { "% Urban", 56.20 }
                    },
                    new HashSet<string>("MO|TN|MS|LA|TX|OK".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "CA",
                    "California",
                    new Dictionary<string, double>()
                    {
                        { "Population", 39937489 },
                        { "Firearms", 344622 },
                        { "Area (mi2)", 163695 },
                        { "Land (mi2)", 155779 },
                        { "GDP (39937489m)", 3155224 },
                        { "Food ($1k)", 31835183 },
                        { "% Urban", 95.00 }
                    },
                    new HashSet<string>("OR|NV|AZ".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "CO",
                    "Colorado",
                    new Dictionary<string, double>()
                    {
                        { "Population", 5845526 },
                        { "Firearms", 92435 },
                        { "Area (mi2)", 104094 },
                        { "Land (mi2)", 103642 },
                        { "GDP (5845526m)", 392348 },
                        { "Food ($1k)", 5501155 },
                        { "% Urban", 86.20 }
                    },
                    new HashSet<string>("WY|NE|KS|OK|NM|UT".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "CT",
                    "Connecticut",
                    new Dictionary<string, double>()
                    {
                        { "Population", 3563077 },
                        { "Firearms", 82400 },
                        { "Area (mi2)", 5543 },
                        { "Land (mi2)", 4842 },
                        { "GDP (3563077m)", 287560 },
                        { "Food ($1k)", 526580 },
                        { "% Urban", 88.00 }
                    },
                    new HashSet<string>("NY|MA|RI".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "DE",
                    "Delaware",
                    new Dictionary<string, double>()
                    {
                        { "Population", 982895 },
                        { "Firearms", 4852 },
                        { "Area (mi2)", 2489 },
                        { "Land (mi2)", 1949 },
                        { "GDP (982895m)", 75765 },
                        { "Food ($1k)", 933843 },
                        { "% Urban", 83.30 }
                    },
                    new HashSet<string>("MD|PA|NJ".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "DC",
                    "District of Columbia",
                    new Dictionary<string, double>()
                    {
                        { "Population", 633427 },
                        { "Firearms", 164058 },
                        { "Area (mi2)", 68 },
                        { "Land (mi2)", 61 },
                        { "GDP (633427m)", 146996 },
                        { "Food ($1k)", 0 },
                        { "% Urban", 100.00 }
                    },
                    new HashSet<string>("MD|VA".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "FL",
                    "Florida",
                    new Dictionary<string, double>()
                    {
                        { "Population", 21992985 },
                        { "Firearms", 343288 },
                        { "Area (mi2)", 65758 },
                        { "Land (mi2)", 53625 },
                        { "GDP (21992985m)", 1100721 },
                        { "Food ($1k)", 6843731 },
                        { "% Urban", 91.20 }
                    },
                    new HashSet<string>("AL|GA".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "GA",
                    "Georgia",
                    new Dictionary<string, double>()
                    {
                        { "Population", 10736059 },
                        { "Firearms", 190050 },
                        { "Area (mi2)", 59425 },
                        { "Land (mi2)", 57513 },
                        { "GDP (10736059m)", 619818 },
                        { "Food ($1k)", 6107025 },
                        { "% Urban", 75.10 }
                    },
                    new HashSet<string>("FL|AL|TN|NC|SC".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "HI",
                    "Hawaii",
                    new Dictionary<string, double>()
                    {
                        { "Population", 1412687 },
                        { "Firearms", 7859 },
                        { "Area (mi2)", 10932 },
                        { "Land (mi2)", 6423 },
                        { "GDP (1412687m)", 97664 },
                        { "Food ($1k)", 549830 },
                        { "% Urban", 91.90 }
                    },
                    new HashSet<string>()
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "ID",
                    "Idaho",
                    new Dictionary<string, double>()
                    {
                        { "Population", 1826156 },
                        { "Firearms", 49566 },
                        { "Area (mi2)", 83569 },
                        { "Land (mi2)", 82643 },
                        { "GDP (1826156m)", 81493 },
                        { "Food ($1k)", 4349253 },
                        { "% Urban", 70.60 }
                    },
                    new HashSet<string>("MT|WY|UT|NV|OR|WA".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "IL",
                    "Illinois",
                    new Dictionary<string, double>()
                    {
                        { "Population", 12659682 },
                        { "Firearms", 146487 },
                        { "Area (mi2)", 57914 },
                        { "Land (mi2)", 55519 },
                        { "GDP (12659682m)", 901572 },
                        { "Food ($1k)", 9708304 },
                        { "% Urban", 88.50 }
                    },
                    new HashSet<string>("IN|KY|MO|IA|WI".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "IN",
                    "Indiana",
                    new Dictionary<string, double>()
                    {
                        { "Population", 6745354 },
                        { "Firearms", 114019 },
                        { "Area (mi2)", 36420 },
                        { "Land (mi2)", 35826 },
                        { "GDP (6745354m)", 379133 },
                        { "Food ($1k)", 6043191 },
                        { "% Urban", 72.40 }
                    },
                    new HashSet<string>("MI|OH|KY|IL".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "IA",
                    "Iowa",
                    new Dictionary<string, double>()
                    {
                        { "Population", 3179849 },
                        { "Firearms", 28494 },
                        { "Area (mi2)", 56273 },
                        { "Land (mi2)", 55857 },
                        { "GDP (3179849m)", 195858 },
                        { "Food ($1k)", 14652946 },
                        { "% Urban", 64.00 }
                    },
                    new HashSet<string>("MN|WI|IL|MO|NE|SD".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "KS",
                    "Kansas",
                    new Dictionary<string, double>()
                    {
                        { "Population", 2910357 },
                        { "Firearms", 52634 },
                        { "Area (mi2)", 82278 },
                        { "Land (mi2)", 81759 },
                        { "GDP (2910357m)", 174183 },
                        { "Food ($1k)", 9502727 },
                        { "% Urban", 74.20 }
                    },
                    new HashSet<string>("NE|MO|OK|CO".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "KY",
                    "Kentucky",
                    new Dictionary<string, double>()
                    {
                        { "Population", 4499692 },
                        { "Firearms", 81058 },
                        { "Area (mi2)", 40408 },
                        { "Land (mi2)", 39486 },
                        { "GDP (4499692m)", 215586 },
                        { "Food ($1k)", 4126185 },
                        { "% Urban", 58.40 }
                    },
                    new HashSet<string>("IN|OH|WV|VA|TN|MO|IL".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "LA",
                    "Louisiana",
                    new Dictionary<string, double>()
                    {
                        { "Population", 4645184 },
                        { "Firearms", 116831 },
                        { "Area (mi2)", 52378 },
                        { "Land (mi2)", 43204 },
                        { "GDP (4645184m)", 264853 },
                        { "Food ($1k)", 2225803 },
                        { "% Urban", 73.20 }
                    },
                    new HashSet<string>("TX|AR|MS".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "ME",
                    "Maine",
                    new Dictionary<string, double>()
                    {
                        { "Population", 1345790 },
                        { "Firearms", 15371 },
                        { "Area (mi2)", 35380 },
                        { "Land (mi2)", 30843 },
                        { "GDP (1345790m)", 67905 },
                        { "Food ($1k)", 553830 },
                        { "% Urban", 38.70 }
                    },
                    new HashSet<string>("NH".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "MD",
                    "Maryland",
                    new Dictionary<string, double>()
                    {
                        { "Population", 6083116 },
                        { "Firearms", 103109 },
                        { "Area (mi2)", 12406 },
                        { "Land (mi2)", 9707 },
                        { "GDP (6083116m)", 430388 },
                        { "Food ($1k)", 1743357 },
                        { "% Urban", 87.20 }
                    },
                    new HashSet<string>("VA|WV|PA|DC|DE".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "MA",
                    "Massachusetts",
                    new Dictionary<string, double>()
                    {
                        { "Population", 6976597 },
                        { "Firearms", 37152 },
                        { "Area (mi2)", 10554 },
                        { "Land (mi2)", 7800 },
                        { "GDP (6976597m)", 599092 },
                        { "Food ($1k)", 413954 },
                        { "% Urban", 92.00 }
                    },
                    new HashSet<string>("RI|CT|NY|NH|VT".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "MI",
                    "Michigan",
                    new Dictionary<string, double>()
                    {
                        { "Population", 10045029 },
                        { "Firearms", 65742 },
                        { "Area (mi2)", 96714 },
                        { "Land (mi2)", 56539 },
                        { "GDP (10045029m)", 543977 },
                        { "Food ($1k)", 4312320 },
                        { "% Urban", 74.60 }
                    },
                    new HashSet<string>("WI|IN|OH".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "MN",
                    "Minnesota",
                    new Dictionary<string, double>()
                    {
                        { "Population", 5700671 },
                        { "Firearms", 79307 },
                        { "Area (mi2)", 86936 },
                        { "Land (mi2)", 79627 },
                        { "GDP (5700671m)", 383094 },
                        { "Food ($1k)", 9794912 },
                        { "% Urban", 73.30 }
                    },
                    new HashSet<string>("WI|IA|SD|ND".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "MS",
                    "Mississippi",
                    new Dictionary<string, double>()
                    {
                        { "Population", 2989260 },
                        { "Firearms", 35494 },
                        { "Area (mi2)", 48432 },
                        { "Land (mi2)", 46923 },
                        { "GDP (2989260m)", 119497 },
                        { "Food ($1k)", 4089158 },
                        { "% Urban", 49.30 }
                    },
                    new HashSet<string>("LA|AR|TN|AL".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "MO",
                    "Missouri",
                    new Dictionary<string, double>()
                    {
                        { "Population", 6169270 },
                        { "Firearms", 72995 },
                        { "Area (mi2)", 69707 },
                        { "Land (mi2)", 68742 },
                        { "GDP (6169270m)", 334286 },
                        { "Food ($1k)", 5818727 },
                        { "% Urban", 70.40 }
                    },
                    new HashSet<string>("IA|IL|KY|TN|AR|OK|KS|NE".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "MT",
                    "Montana",
                    new Dictionary<string, double>()
                    {
                        { "Population", 1086759 },
                        { "Firearms", 22133 },
                        { "Area (mi2)", 147040 },
                        { "Land (mi2)", 145546 },
                        { "GDP (1086759m)", 52470 },
                        { "Food ($1k)", 2238979 },
                        { "% Urban", 55.90 }
                    },
                    new HashSet<string>("ND|SD|WY|ID".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "NE",
                    "Nebraska",
                    new Dictionary<string, double>()
                    {
                        { "Population", 1952570 },
                        { "Firearms", 22234 },
                        { "Area (mi2)", 77348 },
                        { "Land (mi2)", 76824 },
                        { "GDP (1952570m)", 127941 },
                        { "Food ($1k)", 11779728 },
                        { "% Urban", 73.10 }
                    },
                    new HashSet<string>("SD|IA|MO|KS|CO|WY".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "NV",
                    "Nevada",
                    new Dictionary<string, double>()
                    {
                        { "Population", 3139658 },
                        { "Firearms", 76888 },
                        { "Area (mi2)", 110572 },
                        { "Land (mi2)", 109781 },
                        { "GDP (3139658m)", 178622 },
                        { "Food ($1k)", 454344 },
                        { "% Urban", 94.20 }
                    },
                    new HashSet<string>("ID|UT|AZ|CA|OR".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "NH",
                    "New Hampshire",
                    new Dictionary<string, double>()
                    {
                        { "Population", 1371246 },
                        { "Firearms", 64135 },
                        { "Area (mi2)", 9349 },
                        { "Land (mi2)", 8953 },
                        { "GDP (1371246m)", 89152 },
                        { "Food ($1k)", 168871 },
                        { "% Urban", 60.30 }
                    },
                    new HashSet<string>("VT|ME|MA".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "NJ",
                    "New Jersey",
                    new Dictionary<string, double>()
                    {
                        { "Population", 8936574 },
                        { "Firearms", 57505 },
                        { "Area (mi2)", 8723 },
                        { "Land (mi2)", 7354 },
                        { "GDP (8936574m)", 648984 },
                        { "Food ($1k)", 866719 },
                        { "% Urban", 94.70 }
                    },
                    new HashSet<string>("DE|PA|NY".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "NM",
                    "New Mexico",
                    new Dictionary<string, double>()
                    {
                        { "Population", 2096640 },
                        { "Firearms", 97580 },
                        { "Area (mi2)", 121590 },
                        { "Land (mi2)", 121298 },
                        { "GDP (2096640m)", 104349 },
                        { "Food ($1k)", 2564863 },
                        { "% Urban", 77.40 }
                    },
                    new HashSet<string>("AZ|CO|OK|TX".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "NY",
                    "New York",
                    new Dictionary<string, double>()
                    {
                        { "Population", 19440469 },
                        { "Firearms", 76207 },
                        { "Area (mi2)", 54555 },
                        { "Land (mi2)", 47126 },
                        { "GDP (19440469m)", 1740745 },
                        { "Food ($1k)", 3653431 },
                        { "% Urban", 87.90 }
                    },
                    new HashSet<string>("NJ|PA|VT|MA|CT".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "NC",
                    "North Carolina",
                    new Dictionary<string, double>()
                    {
                        { "Population", 10611862 },
                        { "Firearms", 152238 },
                        { "Area (mi2)", 53819 },
                        { "Land (mi2)", 48618 },
                        { "GDP (10611862m)", 590711 },
                        { "Food ($1k)", 8210497 },
                        { "% Urban", 66.10 }
                    },
                    new HashSet<string>("VA|TN|GA|SC".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "ND",
                    "North Dakota",
                    new Dictionary<string, double>()
                    {
                        { "Population", 761723 },
                        { "Firearms", 13272 },
                        { "Area (mi2)", 70698 },
                        { "Land (mi2)", 69001 },
                        { "GDP (761723m)", 57106 },
                        { "Food ($1k)", 4090864 },
                        { "% Urban", 59.90 }
                    },
                    new HashSet<string>("MN|SD|MT".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "OH",
                    "Ohio",
                    new Dictionary<string, double>()
                    {
                        { "Population", 11747694 },
                        { "Firearms", 173405 },
                        { "Area (mi2)", 44826 },
                        { "Land (mi2)", 40861 },
                        { "GDP (11747694m)", 701438 },
                        { "Food ($1k)", 5459380 },
                        { "% Urban", 77.90 }
                    },
                    new HashSet<string>("PA|WV|KY|IN|MI".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "OK",
                    "Oklahoma",
                    new Dictionary<string, double>()
                    {
                        { "Population", 3954821 },
                        { "Firearms", 71269 },
                        { "Area (mi2)", 69899 },
                        { "Land (mi2)", 68595 },
                        { "GDP (3954821m)", 206254 },
                        { "Food ($1k)", 5054570 },
                        { "% Urban", 66.20 }
                    },
                    new HashSet<string>("KS|MO|AR|TX|NM|CO".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "OR",
                    "Oregon",
                    new Dictionary<string, double>()
                    {
                        { "Population", 4301089 },
                        { "Firearms", 61383 },
                        { "Area (mi2)", 98379 },
                        { "Land (mi2)", 95988 },
                        { "GDP (4301089m)", 253036 },
                        { "Food ($1k)", 3691554 },
                        { "% Urban", 81.00 }
                    },
                    new HashSet<string>("CA|NV|ID|WA".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "PA",
                    "Pennsylvania",
                    new Dictionary<string, double>()
                    {
                        { "Population", 12820878 },
                        { "Firearms", 236377 },
                        { "Area (mi2)", 46054 },
                        { "Land (mi2)", 44743 },
                        { "GDP (12820878m)", 817216 },
                        { "Food ($1k)", 4859336 },
                        { "% Urban", 78.70 }
                    },
                    new HashSet<string>("NY|NJ|DE|MD|WV|OH".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "RI",
                    "Rhode Island",
                    new Dictionary<string, double>()
                    {
                        { "Population", 1056161 },
                        { "Firearms", 4223 },
                        { "Area (mi2)", 1545 },
                        { "Land (mi2)", 1034 },
                        { "GDP (1056161m)", 63903 },
                        { "Food ($1k)", 63825 },
                        { "% Urban", 90.70 }
                    },
                    new HashSet<string>("CT|MA".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "SC",
                    "South Carolina",
                    new Dictionary<string, double>()
                    {
                        { "Population", 5210095 },
                        { "Firearms", 105601 },
                        { "Area (mi2)", 32020 },
                        { "Land (mi2)", 30061 },
                        { "GDP (5210095m)", 247711 },
                        { "Food ($1k)", 1909099 },
                        { "% Urban", 66.30 }
                    },
                    new HashSet<string>("GA|NC".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "SD",
                    "South Dakota",
                    new Dictionary<string, double>()
                    {
                        { "Population", 903027 },
                        { "Firearms", 21130 },
                        { "Area (mi2)", 77116 },
                        { "Land (mi2)", 75811 },
                        { "GDP (903027m)", 53692 },
                        { "Food ($1k)", 4877484 },
                        { "% Urban", 56.70 }
                    },
                    new HashSet<string>("ND|MN|IA|NE|WY|MT".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "TN",
                    "Tennessee",
                    new Dictionary<string, double>()
                    {
                        { "Population", 6897576 },
                        { "Firearms", 99159 },
                        { "Area (mi2)", 42144 },
                        { "Land (mi2)", 41235 },
                        { "GDP (6897576m)", 382275 },
                        { "Food ($1k)", 2561984 },
                        { "% Urban", 66.40 }
                    },
                    new HashSet<string>("KY|VA|NC|GA|AL|MS|AR|MO".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "TX",
                    "Texas",
                    new Dictionary<string, double>()
                    {
                        { "Population", 29472295 },
                        { "Firearms", 588696 },
                        { "Area (mi2)", 268596 },
                        { "Land (mi2)", 261232 },
                        { "GDP (29472295m)", 1896063 },
                        { "Food ($1k)", 16498398 },
                        { "% Urban", 84.70 }
                    },
                    new HashSet<string>("NM|OK|AR|LA".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "UT",
                    "Utah",
                    new Dictionary<string, double>()
                    {
                        { "Population", 3282115 },
                        { "Firearms", 72856 },
                        { "Area (mi2)", 84897 },
                        { "Land (mi2)", 82170 },
                        { "GDP (3282115m)", 189809 },
                        { "Food ($1k)", 1253154 },
                        { "% Urban", 90.60 }
                    },
                    new HashSet<string>("ID|WY|CO|AZ|NV".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "VT",
                    "Vermont",
                    new Dictionary<string, double>()
                    {
                        { "Population", 628061 },
                        { "Firearms", 5872 },
                        { "Area (mi2)", 9616 },
                        { "Land (mi2)", 9217 },
                        { "GDP (628061m)", 34973 },
                        { "Food ($1k)", 581773 },
                        { "% Urban", 38.90 }
                    },
                    new HashSet<string>("NY|NH|MA".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "VA",
                    "Virginia",
                    new Dictionary<string, double>()
                    {
                        { "Population", 8626207 },
                        { "Firearms", 307822 },
                        { "Area (mi2)", 42775 },
                        { "Land (mi2)", 39490 },
                        { "GDP (8626207m)", 557144 },
                        { "Food ($1k)", 2684393 },
                        { "% Urban", 75.50 }
                    },
                    new HashSet<string>("NC|TN|KY|WV|MD|DC".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "WA",
                    "Washington",
                    new Dictionary<string, double>()
                    {
                        { "Population", 7797095 },
                        { "Firearms", 91835 },
                        { "Area (mi2)", 71298 },
                        { "Land (mi2)", 66456 },
                        { "GDP (7797095m)", 603772 },
                        { "Food ($1k)", 5868196 },
                        { "% Urban", 84.00 }
                    },
                    new HashSet<string>("ID|OR".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "WV",
                    "West Virginia",
                    new Dictionary<string, double>()
                    {
                        { "Population", 1778070 },
                        { "Firearms", 35264 },
                        { "Area (mi2)", 24230 },
                        { "Land (mi2)", 24038 },
                        { "GDP (1778070m)", 78270 },
                        { "Food ($1k)", 422871 },
                        { "% Urban", 48.70 }
                    },
                    new HashSet<string>("OH|PA|MD|VA|KY".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "WI",
                    "Wisconsin",
                    new Dictionary<string, double>()
                    {
                        { "Population", 5851754 },
                        { "Firearms", 64878 },
                        { "Area (mi2)", 65496 },
                        { "Land (mi2)", 54158 },
                        { "GDP (5851754m)", 348822 },
                        { "Food ($1k)", 6864150 },
                        { "% Urban", 70.20 }
                    },
                    new HashSet<string>("MI|MN|IA|IL".Split('|'))
                )
            );
            _unitlists["states"].Add(
                new Unit(
                    "WY",
                    "Wyoming",
                    new Dictionary<string, double>()
                    {
                        { "Population", 567025 },
                        { "Firearms", 132806 },
                        { "Area (mi2)", 97813 },
                        { "Land (mi2)", 97093 },
                        { "GDP (567025m)", 39610 },
                        { "Food ($1k)", 1104702 },
                        { "% Urban", 64.80 }
                    },
                    new HashSet<string>("MT|SD|NE|CO|UT|ID".Split('|'))
                )
            );
        }
    }

    class Unit : IComparable
    {
        public readonly string code;
        public readonly string name;

        private Group _group;
        public Group group
        {
            get { return _group; }
            set
            {
                _group?.units.Remove(this);
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

        public bool CanBeLost() => group?.CanLose(this) ?? true;

        // less than zero for smaller, zero for same position, greater than zero for larger
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Unit other = obj as Unit;
            if (other != null)
                if ((other.group == null) != (group == null))
                {
                    return group == null ? 1 : -1;
                }
                else
                {
                    return this.metric.CompareTo(other.metric);
                }
            else
                throw new ArgumentException("Object is not a Unit");
        }

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

    class Group : IComparable
    {
        public double metric => units.Sum(unit => unit.metric);
        public HashSet<Unit> units = new HashSet<Unit>();
        public HashSet<string> adjacent =>
            new HashSet<string>(
                units
                    .SelectMany(unit => unit.adjacent)
                    .Where(code => !units.Any(u => u.code == code))
            );

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

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Group other = obj as Group;
            if (other != null)
                return this.metric.CompareTo(other.metric);
            else
                throw new ArgumentException("Object is not a Group");
        }

        public void Print()
        {
            Console.WriteLine(" [" + string.Join(", ", units) + "]");
        }
    }

    class State
    {
        public List<Unit> unitlist;
        public IEnumerable<Unit> canPlace => unitlist.Where(u => u.CanBeLost());
        public IEnumerable<Unit> unplaced => unitlist.Where(u => u.group == null).ToList();
        public readonly List<Group> groups;

        public State(int numDist)
        {
            unitlist = new List<Unit>(Globals.unitlist);
            groups = new List<Group>(from i in Enumerable.Range(0, numDist) select new Group());
        }

        public void DoStep()
        {
            Group group = groups.Min();

            // TODO: precalc some of these?
            List<Unit> placeable = canPlace
                .Where(
                    unit =>
                        !group.units.Contains(unit)
                        && (
                            !group.adjacent.Any()
                            || group.adjacent.Contains(unit.code)
                            || unit.adjacent.Count == 0
                        )
                )
                .ToList();
            Unit unit = canPlace
                .Where(
                    unit =>
                        !group.units.Contains(unit)
                        && (
                            !group.adjacent.Any()
                            || group.adjacent.Contains(unit.code)
                            || unit.adjacent.Count == 0
                        )
                )
                .Max();
            unit.group = group;

            Console.WriteLine(unit);
            Console.WriteLine(" [" + string.Join(", ", placeable) + "]");
            groups.ForEach(g => g.Print());
            Console.WriteLine(" [" + string.Join(", ", unplaced) + "]");
            Console.WriteLine("");
        }

        public bool isDone()
        {
            return !unplaced.Any();
        }
    }
}
