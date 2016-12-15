using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput {
    public class Argument : List<string> {

        public string Name { get; set; }

        public Argument(string _Name, bool _Exists) {
            Name = _Name;
            Exists = _Exists;
        }

        public Argument(string _Name, string _Value, bool _Exists) {
            Name = _Name;
            Exists = _Exists;
            this.Add(_Value);
        }

        public Argument(string _Name, string[] _Values, bool _Exists) {
            Name = _Name;
            Exists = _Exists;
            this.AddRange(_Values);
        }

        public Argument GetItem(int index) {
            if (index < this.Count()) {
                return new Argument(Name, this[index], true);
            } else {
                throw new IndexOutOfRangeException();
            }
        }

        new public bool Exists { get; set; }

        public bool IsArray() {
            return (this.Count() > 1);
        }

        public bool HasValue() {
            return (this.Count() > 0);
        }

        public string Value {
            get {
                if (HasValue()) {
                    return this[0];
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

                if (HasValue() && int.TryParse(this[0], out valueInt)) {
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

                if (HasValue() && decimal.TryParse(this[0], out valueDecimal)) {
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

                if (HasValue() && long.TryParse(this[0], out valueLong)) {
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

                if (HasValue() && DateTime.TryParse(this[0], out valueDateTime)) {
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

        /// <summary>
        /// Gets an list of values from this argument as an array of nullable decimals
        /// </summary>
        public List<decimal?> ToDecimalList() {
            var l = new List<decimal?>();

            if (this.IsArray()) {
                foreach (string thisString in this) {
                    decimal thisDecimal;

                    if (decimal.TryParse(thisString, out thisDecimal)) {
                        l.Add(thisDecimal);
                    } else {
                        l.Add(null);
                    }
                }
            }

            return l;
        }


        /// <summary>
        /// Gets an list of values from this argument as an array of nullable ints
        /// </summary>
        public List<int?> ToIntList() {
            var l = new List<int?>();

            if (this.IsArray()) {
                foreach (string thisString in this) {
                    int thisInt;

                    if (int.TryParse(thisString, out thisInt)) {
                        l.Add(thisInt);
                    } else {
                        l.Add(null);
                    }
                }
            }

            return l;
        }

        /// <summary>
        /// Gets an list of values from this argument as an array of nullable longs
        /// </summary>
        public List<long?> ToLongList() {
            var l = new List<long?>();

            if (this.IsArray()) {
                foreach (string thisString in this) {
                    long thisLong;

                    if (long.TryParse(thisString, out thisLong)) {
                        l.Add(thisLong);
                    } else {
                        l.Add(null);
                    }
                }
            }

            return l;
        }

    }
}
