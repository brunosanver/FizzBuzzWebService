using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FizzBuzzClient.Models
{
    public class ArgumentNullOrWhiteSpaceException : ArgumentException
    {
        public ArgumentNullOrWhiteSpaceException() : base() { }
        public ArgumentNullOrWhiteSpaceException(string message) : base(message) { }
    }

    public class ArgumentNotAnIntegerException : ArgumentException
    {
        public ArgumentNotAnIntegerException() : base("Input value is not an integer.") { }
        public ArgumentNotAnIntegerException(string message) : base(message) { }
    }
}