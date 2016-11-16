using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne
{
    namespace UserInput
    {
        public class ConsoleArguments
        {
            public Dictionary<string, string> Arguments = new Dictionary<string, string>();
            char ArgStart = '/';
            char ValueSeperator = ':';
            char ValueEscape = '"';
            char ValueListSeperator = ',';

            /// <param name="ArgumentStart">The character that signals the start of a argument (default: '/')</param>
            /// <param name="ArgumentValueSeperator">The character that signals the start of the value in an argument (default: ':', can't be the same as ArgumentStart)</param>
            public ConsoleArguments(char ArgumentStart, char ArgumentValueSeperator, string[] _Arguments)
            {
                ArgStart = ArgumentStart;
                ValueSeperator = ArgumentValueSeperator;
                ReadArguments(_Arguments);
            }

            public ConsoleArguments(string[] _Arguments)
            {
                ReadArguments(_Arguments);
            }

            private void ReadArguments(string[] args)
            {
                foreach (string arg in args)
                {
                    string[] cleanArg = arg.TrimStart(ArgStart).ToLower().Split(ValueSeperator);

                    if (cleanArg.Length == 1)
                    {
                        Arguments.Add(cleanArg[0], null);
                    }
                    else
                    {
                        string value = arg.Remove(0, arg.IndexOf(ValueSeperator) + 1);
                        value = value.Trim(ValueEscape);

                        Arguments.Add(cleanArg[0], value);
                    }
                }
            }

            public bool ArgumentExists(string Name)
            {
                Name = Name.ToLower();
                return (Arguments.ContainsKey(Name));
            }

            public bool ArgumentsExists(params string[] Names)
            {
                foreach (string name in Names)
                {
                    if (ArgumentExists(name) == false)
                    {
                        return false;
                    }
                }

                return true;
            }

            public string GetValue(string Name, string DefaultValue)
            {
                Name = Name.ToLower();
                if (ArgumentExists(Name))
                {
                    return Arguments[Name.ToLower()];
                }
                else
                {
                    return DefaultValue;
                }
            }

            public string GetValue(string Name)
            {
                return GetValue(Name, null);
            }

            public string[] GetCsvValue(string Name)
            {
                Name = Name.ToLower();
                if (ArgumentExists(Name) &&
                    string.IsNullOrEmpty(Arguments[Name.ToLower()]) == false)
                {

                    return Arguments[Name.ToLower()].Split(ValueListSeperator);
                }
                else
                {
                    return null;
                }
            }

            public int GetIntValue(string Name, int DefaultValue)
            {
                int intValue;
                string stringValue = GetValue(Name);

                if ((string.IsNullOrEmpty(stringValue) == false) &&
                    int.TryParse(stringValue, out intValue))
                {
                    return intValue;
                }
                else
                {
                    return DefaultValue;
                }
            }

            public Nullable<int> GetIntValue(string Name)
            {
                int intValue;
                string stringValue = GetValue(Name);

                if ((string.IsNullOrEmpty(stringValue) == false) &&
                    int.TryParse(stringValue, out intValue))
                {
                    return intValue;
                }
                else
                {
                    return null;
                }
            }

            public decimal GetDecimalValue(string Name, decimal DefaultValue)
            {
                decimal decimalValue;
                string stringValue = GetValue(Name);

                if ((string.IsNullOrEmpty(stringValue) == false) &&
                    decimal.TryParse(stringValue, out decimalValue))
                {
                    return decimalValue;
                }
                else
                {
                    return DefaultValue;
                }
            }

            public Nullable<decimal> GetDecimalValue(string Name)
            {
                decimal decimalValue;
                string stringValue = GetValue(Name);

                if ((string.IsNullOrEmpty(stringValue) == false) &&
                    decimal.TryParse(stringValue, out decimalValue))
                {
                    return decimalValue;
                }
                else
                {
                    return null;
                }
            }

            public long GetLongValue(string Name, long DefaultValue)
            {
                long lValue;
                string stringValue = GetValue(Name);

                if ((string.IsNullOrEmpty(stringValue) == false) &&
                    long.TryParse(stringValue, out lValue))
                {
                    return lValue;
                }
                else
                {
                    return DefaultValue;
                }
            }

            public Nullable<long> GetLongValue(string Name)
            {
                long lValue;
                string stringValue = GetValue(Name);

                if ((string.IsNullOrEmpty(stringValue) == false) &&
                    long.TryParse(stringValue, out lValue))
                {
                    return lValue;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
