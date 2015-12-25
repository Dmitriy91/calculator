using System;
using System.Collections.Generic;
using System.Linq;
namespace Calculator.Model
{
    public class Calculator
    {
        private List<IOperation> _operations = new List<IOperation>();
        private double _currentValue = 0;

        public double Calculate()
        {
            _operations.ForEach(i => { i.Execute(); });
            double result = _currentValue;
            _currentValue = 0;

            return result;
        }
        public void AddOperation(char @operator, double operand)
        {
            _operations.Add(new CalculatorOperation(CalculateOperation, @operator, operand));
        }
        public void RemoveLastOperation()
        {
            if (_operations.Count > 0)
                _operations.RemoveAt(_operations.Count - 1);
        }
        public bool UndoLastOperation()
        {
            if (_operations.Count == 0)
                return false;

            _operations.Last().Unexecute();
            _operations.RemoveAt(_operations.Count - 1);
            return true;
        }
        public bool RedoLastOperation()
        {
            if (_operations.Count == 0)
                return false;

            _operations.Last().Unexecute();
            return true;
        }
        public void Reset()
        {
            _currentValue = 0;
            _operations.Clear();
        }

        private void CalculateOperation(char @operator, double operand)
        {
            switch (@operator)
            {
                case '+': _currentValue += operand; break;
                case '-': _currentValue -= operand; break;
                case '*': _currentValue *= operand; break;
                case '/': _currentValue /= operand; break;
                default:
                    throw new ArgumentException("Invalid operator value");
            }
        }
    }
}
