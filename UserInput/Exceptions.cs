using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput
{
    class UserInputExceptions
    {

        class ArgumentExistsException : Exception {
            public ArgumentExistsException() { }

            public ArgumentExistsException(string message) : base(message) { }

            public ArgumentExistsException(string message, Exception inner) : base(message, inner) { }
        }

    }
}
