using Runner._2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public class PassportData
    {
        public Dictionary<string, string> Properties { get; set; }
    }
    public class Day4 : PuzzleBase<IEnumerable<PassportData>>
    {
        //note using updated sample data, which also includes some useful bits of the second problem
        public Day4() : base(4, 2020, "8", "4") { }
        public override IEnumerable<PassportData> ParseInput(IEnumerable<string> input)
        {
            var passports = new List<PassportData>();

            PassportData currentPassport = new PassportData() { Properties = new() };
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentPassport != null) { passports.Add(currentPassport); }
                    currentPassport = new PassportData() { Properties = new() };
                    continue;
                }

                var keyValuePairs = line.Split(" ").ToDictionary(m => m.Split(":")[0], m => m.Split(":")[1]);//.Select(m => m.Split(":"));
                foreach (var item in keyValuePairs)
                {
                    currentPassport.Properties.Add(item.Key, item.Value);
                }
            }
            passports.Add(currentPassport);

            return passports;
        }

        private string[] _requiredProperties = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

        public override string SolveProblem1(IEnumerable<PassportData> input)
        {
            int validPassports = 0;
            foreach (var passport in input)
            {
                if (_requiredProperties.All(k => passport.Properties.Keys.Any(r => r == k)))
                {
                    validPassports++;
                }
            }

            //157 too low
            return validPassports.ToString();
        }

        private bool IsValid(string dataItem, string dataValue)
        {
            //hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f
            var hclRegex = new Regex("#[a-f0-9]{6}");
            //(Passport ID) - a nine-digit number, including leading zeroes
            var pidRegex = new Regex("^[0-9]{9}$");
            var ecl = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            switch (dataItem)
            {
                case "byr":
                    var byr = int.Parse(dataValue);
                    if (byr >= 1920 && byr <= 2002) { return true; } else { return false; };
                case "iyr":
                    var iyr = int.Parse(dataValue);
                    if (iyr >= 2010 && iyr <= 2020) { return true; } else { return false; };
                case "eyr":
                    var eyr = int.Parse(dataValue);
                    if (eyr >= 2020 && eyr <= 2030) { return true; } else { return false; };
                case "hgt":
                    if (!dataValue.EndsWith("in") && !dataValue.EndsWith("cm")) { return false; }
                    var height = int.Parse(dataValue.Substring(0, dataValue.Length - 2));
                    if (dataValue.EndsWith("cm"))
                    {
                        return height >= 150 && height <= 193;
                    }
                    if (dataValue.EndsWith("in"))
                    {
                        return height >= 59 && height <= 76;
                    }
                    return false;
                case "hcl":
                    return hclRegex.IsMatch(dataValue);
                case "ecl":
                    return ecl.Any(m => m == dataValue);
                case "pid":
                    return pidRegex.IsMatch(dataValue);
                default:
                    return false;
            }
        }

        public override string SolveProblem2(IEnumerable<PassportData> input)
        {
            // 
            int validPassports = 0;
            foreach (var passport in input)
            {
                if (_requiredProperties.All(k => passport.Properties.Keys.Any(r => r == k)&& IsValid(k, passport.Properties[k])))
                {
                    validPassports++;
                }
            }

            //157 too low
            return validPassports.ToString();
        }
    }
}
