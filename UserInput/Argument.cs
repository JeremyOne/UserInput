using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput {
    public class Argument {

        ///<summary>The name/key of this argument, used for lookups</summary>
        public string Name { get; set; }

        private List<string> Values = new List<string>();

        public Argument(string _Name, bool _Exists) {
            Name = _Name;
            Exists = _Exists;
        }

        public Argument(string _Name, string _Value, bool _Exists) {
            Name = _Name;
            Exists = _Exists;
            Values = new List<string> { _Value };
        }

        public Argument(string _Name, string[] _Values, bool _Exists) {
            Name = _Name;
            Exists = _Exists;
            Values = _Values.ToList();
        }

        ///<summary>Access arguments values by index</summary>
        public Argument this[int index] {
            get {
                if (index < Values.Count()) {
                    return new Argument(Name, Values[index], true);
                } else {
                    throw new IndexOutOfRangeException();
                }
            }
        }
        
        ///<summary>Returns true if the key (case insensitive) exits</summary>
        public bool Exists { get; set; }

        ///<summary>Returns true if this argument has more than one value</summary>
        public bool IsArray() {
            return (Values.Count() > 1);
        }

        ///<summary>Returns true if this argument has any number of values</summary>
        public bool HasValue() {
            return (Values.Count() > 0);
        }

        ///<summary>Returns the first value in the arument, or null if it does not exist</summary>
        public string Value {
            get {
                if (HasValue()) {
                    return Values[0];
                } else {
                    return null;
                }
            }
        }

        ///<summary>Returns the first value or passed default vaue if it does not exist </summary>
        public string ValueOrDefault(string Default) {
            var _value = Value;

            if (_value == null) {
                return Default;
            } else {
                return _value;
            }
        }

        ///<summary>Returns the first value in the argument as an integer, or null if it does not exist or can't be parsed</summary>
        public int? ValueInt {
            get {
                int valueInt;

                if (HasValue() && int.TryParse(Values[0], out valueInt)) {
                    return valueInt;
                } else {
                    return null;
                }
            }            
        }
        
        ///<summary>Returns the first value in the argument as an integer, or the provided default if it does not exist or can't be parsed</summary>
        public int ValueIntOrDefault(int Default) {
            var _value = ValueInt;

            if (_value == null) {
                return Default;
            } else {
                return _value.Value;
            }
        }

        ///<summary>Returns the first value in the argument as an decimal, or null if it does not exist or can't be parsed</summary>
        public decimal? ValueDecimal {
            get {
                decimal valueDecimal;

                if (HasValue() && decimal.TryParse(Values[0], out valueDecimal)) {
                    return valueDecimal;
                } else {
                    return null;
                }
            }
        }

        ///<summary>Returns the first value in the argument as an decimal, or the provided default if it does not exist or can't be parsed</summary>
        public decimal ValueDecimalOrDefault(decimal Default) {
            var _value = ValueDecimal;

            if (_value == null) {
                return Default;
            } else {
                return _value.Value;
            }
        }

        ///<summary>Returns the first value in the argument as an long, or null if it does not exist or can't be parsed</summary>
        public long? ValueLong {
            get {
                long valueLong;

                if (HasValue() && long.TryParse(Values[0], out valueLong)) {
                    return valueLong;
                } else {
                    return null;
                }
            }
        }

        ///<summary>Returns the first value in the argument as a long, or the provided default if it does not exist or can't be parsed</summary>
        public long ValueLongOrDefault(long Default) {
            var _value = ValueLong;

            if (_value == null) {
                return Default;
            } else {
                return _value.Value;
            }
        }

        ///<summary>Returns the first value in the argument as a DateTime, or null if it does not exist or can't be parsed</summary>
        public DateTime? ValueDateTime {
            get {
                DateTime valueDateTime;

                if (HasValue() && DateTime.TryParse(Values[0], out valueDateTime)) {
                    return valueDateTime;
                } else {
                    return null;
                }
            }
        }

        ///<summary>Returns the first value in the argument as an DateTime, or the provided default if it does not exist or can't be parsed</summary>
        public DateTime ValueDateTimeOrDefault(DateTime Default) {
            var _value = ValueDateTime;

            if (_value == null) {
                return Default;
            } else {
                return _value.Value;
            }
        }

        ///<summary>Returns the first value in the argument as an integer, or the provided default if it does not exist or can't be parsed</summary>
        public void AddValue(string NewValue) {
            Values.Add(NewValue);
        }

    }
}
