using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Model
{
    public class CalculatorOperation : IOperation
    {
        private char _operator;
        private double _operand;
        private Action<char, double> _calculate;

        public char Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
        public int Operand
        {
            set { _operand = value; }
        }
        
        public CalculatorOperation(Action<char, double> calculate, char @operator, double operand)
        {
          _calculate = calculate;
          _operator = @operator;
          _operand = operand;
        }
        public void Execute()
        {
            _calculate(_operator, _operand);
        }
        //Unexecute last command
        public void Unexecute()
        {
          _calculate(GetReversedOperator(_operator), _operand);
        }
        //Returns an opposite operator for given one
        private char GetReversedOperator(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default: 
                    throw new ArgumentException("@operator");
            }
        }
    }
}
