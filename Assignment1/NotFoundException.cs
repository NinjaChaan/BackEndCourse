using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1
{
    class NotFoundException : Exception
    {
		public NotFoundException(string Message) : base(Message) {}
    }
}
