using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace JeremyOne.UserInput
{
    public class Casting
    {

        public static Decimal GetDecimal(object Input) {
            var dec = new decimal();
            string value = Input.ToString();

            decimal.TryParse(value, out dec);

            return dec;
        }

        public static int GetInt(object Input) {
            int valueInt;
            string valueString = Input.ToString();

            int.TryParse(valueString, out valueInt);

            return valueInt;
        }

        public static Nullable<DateTime> GetDate(object Input) {
            if (Input is DateTime)
            {
                return (DateTime)Input;
            }

            if (Input != null)
            {
                string valueString = Input.ToString();
                DateTime dateValue = new DateTime();

                if (DateTime.TryParse(valueString, out dateValue))
                {
                    return dateValue;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Copies and Casts one object to another (clone). Takes a base object, and assigns any 1st level fields that exist in the ToObject to the value of the FromObject.
        /// </summary>
        /// <param name="FromObject">An object with fields to be copied from</param>
        /// <param name="ToObject">An object with fields to be copied to</param>
        /// <returns>The ToObject</returns>
        public static object CopyCast(object FromObject, object ToObject) {

            Type fromType = FromObject.GetType(); // Get type pointer
            Type toType = ToObject.GetType(); // Get type pointer

            FieldInfo[] fromFields = fromType.GetFields(); // Obtain all fields
            FieldInfo[] toFields = toType.GetFields(); // Obtain all fields

            foreach (var fromfield in fromFields)
            {
                var toField = (from f in toFields
                               where
                                   (f.FieldType == fromfield.FieldType) &&
                                   (f.Name == fromfield.Name)
                               select f).FirstOrDefault();

                if (toField != null)
                {
                    toField.SetValue(ToObject, fromfield.GetValue(FromObject));
                }
            }

            return ToObject;

        }

    }
}
