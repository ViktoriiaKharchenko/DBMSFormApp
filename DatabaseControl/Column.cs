using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseControl
{
    public class Column
    {
        public string Name { get; protected set; }
        public string TypeFullName { get; private set; }
        public Column (){}
        public Column(string name, Type type ) 
        {
            Name = name;
            TypeFullName = type.FullName;
        }
        public Column(string name, string type)
        {
            Name = name;
            TypeFullName = type;
        }
        public bool CheckCast<T>(T value)
        {
            try
            {
                var resultVal = Convert.ChangeType(value, Type.GetType(TypeFullName));
                if (!resultVal.ToString().Equals(value.ToString()))
                    throw new InvalidCastException();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckValue(string value, Invl invl, char from, char to)
        {
            if (invl == Invl.charInvl)
            {
                if (value.Length > 1 ) return false;
            }
            var charList = new List<char>();
            charList.AddRange(value);
            var invalidChars = charList.FindAll(c => c < from || c > to);
            if (invalidChars.Count != 0) return false;
            return true;
        }
    }
}
