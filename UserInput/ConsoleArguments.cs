using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput {
	public class ConsoleArguments {

		public Dictionary<string, string[]> Arguments = new Dictionary<string, string[]>();

		string[] NameIndicators = { "/" };
		char[] ValueSeperators = { ':' };

        /// <param name="_Arguments">Standard arguments array from console application</param>
        /// <param name="_NameIndicators">Strings that signal the start of a argument (default: '/')</param>
        /// <param name="_ValueSeperators">Strings that signal the start of the value in an argument (default: ':')
        /// Note: A space is ALWAYS a seperator unless values are escaped with double quotes</param>
        public ConsoleArguments(string[] _Arguments, string[] _NameIndicators = null, char[] _ValueSeperators = null) {
			if(_NameIndicators != null) {
				//order by length to make sure that longer indicators are processed first ('--' before '-').
				NameIndicators = _NameIndicators.OrderByDescending(a => a.Length).ToArray();
			}

			if(_ValueSeperators != null) {
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
			string lastArgName = "";

			foreach(string arg in args) {
				bool hasNameIndicator = false;
				string nameIndicator = "";

				foreach(string nameStart in NameIndicators) {
					if(arg.StartsWith(nameStart)) {
						//the argument starts with a name indicator
						hasNameIndicator = true;
						nameIndicator = nameStart;
					}
				}

				if(hasNameIndicator == false) {
					//argument does not start with with an indicator, so assume it is part of a list
					AppendArgumentValue(lastArgName, arg);
				} else {
					string name = arg.Remove(nameIndicator.Length);
					string[] values = { };
					char valueSeperator = ValueSeperators[0];

					foreach(char seperator in ValueSeperators) {
						if(name.Contains(seperator)) {
							values = name.Split(seperator);
							valueSeperator = seperator;
						}
					}

					if(values.Length > 0) {
						//Is an array
						foreach(string v in values) {
							AppendArgumentValue(values[0], v);
						}
					} else {
						//Is not an array
						AddArgument(name, null);
					}

				}

			}
		}

        /// <summary>
        /// Appends a value to a argument array/list
        /// </summary>
		private void AppendArgumentValue(string Name, string Value) {
			Name = Name.ToLower();

			if(ArgumentExists(Name)) {
				var o = GetValueList(Name).ToList();
				o.Add(Value);
				Arguments[Name] = o.ToArray();
			} else {
				AddArgument(Name, Value);
			}
		}

        /// <summary>
        /// Adds a argument to list list of parsed arguments
        /// </summary>
		private void AddArgument(string Name, string Value) {
			Name = Name.ToLower();

			if(ArgumentExists(Name)) {
				throw new ArgumentException("The argument: '" + Name + "' was already parsed");
			} else {
				string[] s = { Value };
				Arguments.Add(Name, s);
			}
		}

        /// <summary>
        /// Checks if a argument with this name exists
        /// </summary>
		public bool ArgumentExists(string Name) {
			Name = Name.ToLower();
			return (Arguments.ContainsKey(Name));
		}

        /// <summary>
        /// Checks if a list of arguments exists
        /// </summary>
        /// <returns>True if all arguments exist</returns>
		public bool ArgumentsExists(params string[] Names) {
			foreach(string name in Names) {
				if(ArgumentExists(name) == false) {
					return false;
				}
			}

			return true;
		}

        /// <summary>
        /// Checks of a argument has more than one value
        /// </summary>
		public bool ArgumentValueIsArray(string Name) {
			Name = Name.ToLower();
			if(ArgumentExists(Name)) {
				return (Arguments[Name].Length > 1);
			} else {
				return false;
			}
		}

        /// <summary>
        /// Gets the first value of a argument
        /// </summary>
        /// <param name="DefaultValue">The return value if the argument is not found</param>
        /// <returns></returns>
		public string GetValue(string Name, string DefaultValue) {
			Name = Name.ToLower();
			if(ArgumentExists(Name)) {
				return Arguments[Name.ToLower()][1];
			} else {
				return DefaultValue;
			}
		}

        /// <summary>
        /// Gets the first value of a argument
        /// </summary>
        public string GetValue(string Name) {
			return GetValue(Name, null);
		}

        /// <summary>
        /// Gets all values of a argument
        /// </summary>
        public string[] GetValueList(string Name) {
			Name = Name.ToLower();

			if(ArgumentExists(Name) && ArgumentValueIsArray(Name)) {
				return Arguments[Name.ToLower()];
			} else {
				return null;
			}
		}

        /// <summary>
        /// Gets the first value of a argument as an integer
        /// </summary>
        /// <param name="DefaultValue">The return value if the argument is not found</param>
        public int GetIntValue(string Name, int DefaultValue) {
			int intValue;
			string stringValue = GetValue(Name);

			if((string.IsNullOrEmpty(stringValue) == false) &&
			             int.TryParse(stringValue, out intValue)) {
				return intValue;
			} else {
				return DefaultValue;
			}
		}

        /// <summary>
        /// Gets the first value of a argument as an integer
        /// </summary>
        /// <returns>Returns the value, or null if not found</returns>
		public Nullable<int> GetIntValue(string Name) {
			int intValue;
			string stringValue = GetValue(Name);

			if((string.IsNullOrEmpty(stringValue) == false) &&
			             int.TryParse(stringValue, out intValue)) {
				return intValue;
			} else {
				return null;
			}
		}

        /// <summary>
        /// Gets the first value of a argument as a decimal
        /// </summary>
        /// <param name="DefaultValue">The return value if the argument is not found</param>
        public decimal GetDecimalValue(string Name, decimal DefaultValue) {
			decimal decimalValue;
			string stringValue = GetValue(Name);

			if((string.IsNullOrEmpty(stringValue) == false) &&
			             decimal.TryParse(stringValue, out decimalValue)) {
				return decimalValue;
			} else {
				return DefaultValue;
			}
		}

        /// <summary>
        /// Gets the first value of a argument as a decimal
        /// </summary>
        /// <returns>Returns the value, or null if not found</returns>
		public Nullable<decimal> GetDecimalValue(string Name) {
			decimal decimalValue;
			string stringValue = GetValue(Name);

			if((string.IsNullOrEmpty(stringValue) == false) &&
			             decimal.TryParse(stringValue, out decimalValue)) {
				return decimalValue;
			} else {
				return null;
			}
		}

        /// <summary>
        /// Gets the first value of a argument as a long
        /// </summary>
        /// <param name="DefaultValue">The return value if the argument is not found</param>
        public long GetLongValue(string Name, long DefaultValue) {
			long lValue;
			string stringValue = GetValue(Name);

			if((string.IsNullOrEmpty(stringValue) == false) &&
			             long.TryParse(stringValue, out lValue)) {
				return lValue;
			} else {
				return DefaultValue;
			}
		}

        /// <summary>
        /// Gets the first value of a argument as a long
        /// </summary>
        /// <returns>Returns the value, or null if not found</returns>
		public Nullable<long> GetLongValue(string Name) {
			long lValue;
			string stringValue = GetValue(Name);

			if((string.IsNullOrEmpty(stringValue) == false) &&
			             long.TryParse(stringValue, out lValue)) {
				return lValue;
			} else {
				return null;
			}
		}

	}
    
}
