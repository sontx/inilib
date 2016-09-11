using System;
using System.Collections.Generic;

namespace INILib
{
    public class Section
    {
        public string Name { get; set; } = null;

        public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

        public string GetString(string keyName, string defaultValue = null)
        {
            string value;
            if (Keys.TryGetValue(keyName, out value))
                return value;
            return defaultValue;
        }

        public byte GetInt16(string keyName, byte defaultValue = 0)
        {
            string stringValue = GetString(keyName, defaultValue.ToString());
            byte byteValue;
            if (byte.TryParse(stringValue, out byteValue))
                return byteValue;
            return defaultValue;
        }

        public Int16 GetInt16(string keyName, Int16 defaultValue = 0)
        {
            string stringValue = GetString(keyName, defaultValue.ToString());
            Int16 int16Value;
            if (Int16.TryParse(stringValue, out int16Value))
                return int16Value;
            return defaultValue;
        }

        public Int32 GetInt32(string keyName, Int32 defaultValue = 0)
        {
            string stringValue = GetString(keyName, defaultValue.ToString());
            Int32 int32Value;
            if (Int32.TryParse(stringValue, out int32Value))
                return int32Value;
            return defaultValue;
        }

        public Int64 GetInt32(string keyName, Int64 defaultValue = 0)
        {
            string stringValue = GetString(keyName, defaultValue.ToString());
            Int64 int64Value;
            if (Int64.TryParse(stringValue, out int64Value))
                return int64Value;
            return defaultValue;
        }

        public float GetFloat(string keyName, float defaultValue = 0)
        {
            string stringValue = GetString(keyName, defaultValue.ToString());
            float floatValue;
            if (float.TryParse(stringValue, out floatValue))
                return floatValue;
            return defaultValue;
        }

        public double GetDouble(string keyName, double defaultValue = 0)
        {
            string stringValue = GetString(keyName, defaultValue.ToString());
            double doubleValue;
            if (double.TryParse(stringValue, out doubleValue))
                return doubleValue;
            return defaultValue;
        }
    }
}
