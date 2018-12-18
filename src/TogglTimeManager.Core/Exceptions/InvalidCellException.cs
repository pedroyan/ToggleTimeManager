using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TogglTimeManager.Core.Exceptions
{
    [Serializable]
    public class InvalidCellException : Exception
    {
        public string RawValue { get; }
        public int Line { get; }

        public InvalidCellException(string message, string rawValue, int line) : base($"{message}\nValue: {rawValue}, Line: {line}")
        {
            RawValue = rawValue;
            Line = line;
        }
    }
}
