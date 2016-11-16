using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput
{
    public class ConsoleArguments {

        public Dictionary<string, string[]> Arguments = new Dictionary<string, string[]>();

        string[] NameIndicators = {"/"};
        char[] ValueSeperators = {':'};

        /// <param name="_NameIndicators">Strings that signal the start of a argument (default: '/')</param>
        /// <param name="_ValueSeperators">Strings that signal the start of the value in an argument (default: ':')
        /// Note: A space is ALWAYS a seperator unless values are escaped with double quotes</param>
        public ConsoleArguments(string[] _Arguments, string[] _NameIndicators = null, char[] _ValueSeperators = null)
        {
            if (_NameIndicators != null) {
                //order by length to make sure that longer indicators are processed first ('--' before '-').
                NameIndicators = _NameIndicators.OrderByDescending(a => a.Length).ToArray();
            }

            if (_ValueSeperators != null) {
                ValueSeperators = _ValueSeperators;
            }

            ReadArguments(_Arguments);
        }

        public ConsoleArguments(string[] _Arguments)
        {
            ReadArguments(_Arguments);
        }

        private void ReadArguments(string[] args)
        {
            string lastArgName = "";

            foreach (string arg in args)
            {
                bool hasNameIndicator = false;
                string nameIndicator = "";

                foreach (string nameStart in NameIndicators) {
                    if (arg.StartsWith(nameStart)) {
                        //the argument starts with a name indicator
                        hasNameIndicator = true;
                        nameIndicator = nameStart;
                    }
                }

                if (hasNameIndicator == false)
                {
                    //argument does not start with with an indicator, so assume it is part of a list
                    AppendArgumentValue(lastArgName, arg);
                }
                else
                {
                    string name = arg.Remove(nameIndicator.Length);
                    string[] values = { };
                    char valueSeperator = ValueSeperators[0];

                    foreach (char seperator in ValueSeperators)
                    {
                        if (name.Contains(seperator)) {
                            values = name.Split(seperator);
                            valueSeperator = seperator;
                        }
                    }



                    if (values.Length > 0)
                    {
                        //Is an array
                        foreach (string v in values)
                        {
                            AppendArgumentValue(values[0], v);
                        }
                    }
                    else
                    {
                        //Is not an array
                        AddArgument(name, null);
                    }

                }

            }
        }

        private void AppendArgumentValue(string Name, string Value) {
            Name = Name.ToLower();

            if (ArgumentExists(Name))
            {
                var o = GetValueList(Name).ToList();
                o.Add(Value);
                Arguments[Name] = o.ToArray();
            }
            else
            {
                AddArgument(Name, Value);
            }
        }

        private void AddArgument(string Name, string Value)
        {
            Name = Name.ToLower();

            if (ArgumentExists(Name))
            {
                throw new ArgumentException("The argument: '" + Name + "' was already parsed");
            }
            else
            {
                string[] s = { Value };
                Arguments.Add(Name, s);
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

        public string[] GetValueList(string Name)
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
