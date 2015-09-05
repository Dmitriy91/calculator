using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Model
{
    interface IOperation
    {
        void Execute();
        void Unexecute();
        char Operator { get; }
    }
}
