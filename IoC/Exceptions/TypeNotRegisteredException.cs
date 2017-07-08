using System;
namespace IoC.Exceptions
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException() : base()
        {
        }

        public TypeNotRegisteredException(string sourceTypeName) : base(string.Format("Could not resolve {0}", sourceTypeName))
		{
		}
    }
}
