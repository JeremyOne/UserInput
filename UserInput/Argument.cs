using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput {
    public class Argument {

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

        public Argument this[int index] {
            get {
                if (index < Values.Count()) {
                    return new Argument(Name, Values[index], true);
                } else {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public bool Exists { get; set; }

        public bool IsArray() {
            return (Values.Count() > 1);
        }

        public bool HasValue() {
            return (Values.Count() > 0);
        }

        public string Value {
            get {
                if (HasValue()) {
                    return Values[0];
                } else {
                    return null;
                }
            }
        }

        public string ValueOrDefault(string Default) {
            var _value = Value;

            if (_value == null) {
                return Default;
            } else {
                return _value;
            }
        }

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

        public int ValueIntOrDefault(int Default) {
            var _value = ValueInt;

            if (_value == null) {
                return Default;
            } else {
                return _value.Value;
            }
        }

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

        public decimal ValueDecimalOrDefault(decimal Default) {
            var _value = ValueDecimal;

            if (_value == null) {
                return Default;
            } else {
                return _value.Value;
            }
        }

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

        public long ValueLongOrDefault(long Default) {
            var _value = ValueLong;

            if (_value == null) {
                return Default;
            } else {
                return _value.Value;
            }
        }

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

        public DateTime ValueDateTimeOrDefault(DateTime Default) {
            var _value = ValueDateTime;

            if (_value == null) {
                return Default;
            } else {
                return _value.Value;
            }
        }

        public void AddValue(string NewValue) {
            Values.Add(NewValue);
        }

    }
}
