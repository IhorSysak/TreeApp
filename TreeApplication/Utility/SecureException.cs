using System.Text.Json;
using TreeApplication.Context;

namespace TreeApplication.Utility
{
    public class SecureException : Exception
    {
        public SecureException(string message) : base(message) { }
    }
}
