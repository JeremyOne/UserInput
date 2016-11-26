using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput {
    public class ConsoleArguments : List<Argument> {

        string[] NameIndicators = { "/" };
        char[] ValueSeperators = { ':' };

	/// <summary>A library for parcing arguments from c# and other dotnet console applications</summary
        /// <param name="_Arguments">Standard arguments array from console application</param>
        /// <param name="_NameIndicators">Strings that signal the start of a argument (default: '/')</param>
        /// <param name="_ValueSeperators">Strings that signal the start of the value in an argument (default: ':')
        /// Note: A space is ALWAYS a separator unless values are escaped with double quotes</param>
        public ConsoleArguments(string[] _Arguments, string[] _NameIndicators = null, char[] _ValueSeperators = null) {
            if (_NameIndicators != null) {
                //order by length to make sure that longer indicators are processed first ('--' before '-').
                NameIndicators = _NameIndicators.OrderByDescending(a => a.Length).ToArray();
            }

            if (_ValueSeperators != null) {
                ValueSeperators = _ValueSeperators;
            }

            ReadArguments(_Arguments);
        }

        /// <param name="_Arguments">Standard arguments array from console application</param>
		public ConsoleArguments(string[] _Arguments) {
            ReadArguments(_Arguments);
        }

        /// <summary>
        /// Parses the arguments
        /// </summary>
		private void ReadArguments(string[] args) {
            Argument lastArg = new Argument("", true);
            this.Add(lastArg);

            foreach (string arg in args) {
                bool hasNameIndicator = false;
                string nameIndicator = "";

                foreach (string nameStart in NameIndicators) {
                    if (arg.StartsWith(nameStart)) {
                        //the argument starts with a name indicator
                        hasNameIndicator = true;
                        nameIndicator = nameStart;
                        break;
                    }
                }

                if (hasNameIndicator == false) {
                    //argument does not start with with an indicator, so assume it is part of a list
                    if (arg.Contains(" ")) {
                        lastArg.AddValue(arg.Trim('\'').Trim('"'));
                    } else {
                        lastArg.AddValue(arg);
                    }
                } else {
                    string name = arg.Remove(0, nameIndicator.Length).ToLower();
                    string[] values = { };
                    char valueSeperator = ValueSeperators[0];

                    foreach (char seperator in ValueSeperators) {
                        if (name.Contains(seperator)) {
                            if (name.EndsWith(seperator.ToString())) {
                                name = name.TrimEnd(seperator);
                            }

                            values = name.Split(seperator);
                            valueSeperator = seperator;
                            name = values[0];
                        }
                    }

                    lastArg = new Argument(name, true);
                    this.Add(lastArg);

                    if (values.Length > 1) {
                        //Is an array
                        for (int i = 1; i < values.Length; i++) {
                            if (values[i].Contains(" ")) {
                                values[i] = values[i].Trim('\'').Trim('"');
                            }

                            lastArg.AddValue(values[i]);
                        }
                    }

                }

            }
        }

        /// <summary>
        /// Gets an argument by name
        /// </summary>
        /// <param name="Name">Name of argument</param>
        /// <returns>An Argument object, or if the argument does not exist a empty argument with no value is returned</returns>
        public Argument GetArgument(string Name) {
            Name = Name.ToLower();
            var a = (from aa in this where aa.Name == Name select aa).FirstOrDefault();

            if (a != null) {
                return a;
            } else {
                return new Argument(Name.ToLower(), false);
            }
        }

    }
}
