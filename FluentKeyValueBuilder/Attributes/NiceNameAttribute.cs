using System;

namespace FluentKeyValueBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NiceNameAttribute : Attribute
    {
        public string NiceName { get; set; }

        public NiceNameAttribute(string niceName)
        {
            NiceName = niceName;
        }
    }
}
