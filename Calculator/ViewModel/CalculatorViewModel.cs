using MicroMvvm;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    public class CalculatorViewModel : BaseViewModel
    {
        #region Fields
        private string _inputStr = String.Empty;
        private string _resultStr = String.Empty;
        private char _lastOperator = '+';
        private readonly char[] _operators = { '/', '*', '+', '-' };
        private readonly Model.Calculator _calculator = new Model.Calculator();
        private readonly CultureInfo _cultureInfo = CultureInfo.CurrentUICulture;
        #endregion

        #region Properties
        public string InputStr
        {
            get
            {
                return _inputStr;
            }
            set
            {
                _inputStr = value;
                NotifyPropertyChanged("InputStr");
            }
        }
        public string ResultStr
        {
            get
            {
                return _resultStr;
            }
            set
            {
                _resultStr = value;
                NotifyPropertyChanged("ResultStr");
            }
        }
        #endregion

        #region Commands
        private RelayCommand _multiplyCommand;
        private RelayCommand _divideCommand;
        private RelayCommand _addCommand;
        private RelayCommand _substructCommand;
        private RelayCommand _inputNumberDecimalSeparator;
        private RelayCommand _inputDigitCommand;
        private RelayCommand _removeLastCharCommand;
        private RelayCommand _calculateCommand;
        private RelayCommand _resetCommand;
        private RelayCommand _closeWinCommand;
        private RelayCommand _minimizeWinCommand;

        private bool CanExecuteOperationCommand()
        {
            if (String.IsNullOrWhiteSpace(InputStr) || IsOperatorOrPoint(InputStr.Last()))
                return false;

            return true;
        }
        public ICommand DivideCommand
        {
            get
            {
                if (_divideCommand == null)
                    _divideCommand = new RelayCommand(param => { OperationHelper('/'); }, param => CanExecuteOperationCommand());

                return _divideCommand;
            }
        }
        public ICommand MultiplyCommand
        {
            get
            {
                if (_multiplyCommand == null)
                    _multiplyCommand = new RelayCommand(param => { OperationHelper('*'); }, param => CanExecuteOperationCommand());

                return _multiplyCommand;
            }
        }
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new RelayCommand(param => { OperationHelper('+'); }, param => CanExecuteOperationCommand());

                return _addCommand;
            }
        }
        public ICommand SubstructCommand
        {
            get
            {
                if (_substructCommand == null)
                    _substructCommand = new RelayCommand(param => { OperationHelper('-'); }, param => CanExecuteOperationCommand());

                return _substructCommand;
            }
        }

        private bool CanExecuteInputDigitCommand()
        {
            if (InputStr.Split(_operators).Last().Length < 15)
                return true;

            return false;
        }
        private void InputDigit(Object digit)
        {
            if(InputStr.Split(_operators).Last() == "0")
                InputStr = InputStr.Remove(InputStr.Length - 1) + digit as string;
            else
                InputStr += digit as string;

            ResultStr = String.Empty;
        }
        public ICommand InputDigitCommand
        {
            get
            {
                if (_inputDigitCommand == null)
                    _inputDigitCommand = new RelayCommand(InputDigit, param => CanExecuteInputDigitCommand());

                return _inputDigitCommand;
            }
        }

        private bool CanExecuteRemoveLastCharCommand()
        {
            if (String.IsNullOrWhiteSpace(InputStr))
                return false;

            return true;
        }
        private void RemoveLastChar()
        {
            if (_operators.Contains(InputStr.Last()) && !InputStr.Equals("-"))
            {
                char prevOperator = InputStr.Remove(InputStr.Length - 1)
                    .ToList()
                    .FindLast(character => _operators.Contains(character));

                _lastOperator = prevOperator == default(char)? '+': prevOperator;
                _calculator.RemoveLastOperation();

                if (InputStr[InputStr.Length - 2] == ')')
                {
                    InputStr = InputStr.Remove(0, 1);
                    InputStr = InputStr.Remove(InputStr.Length - 1);
                }
            }

            InputStr = InputStr.Remove(InputStr.Length - 1);

            if (this.CalculateCommand.CanExecute(null))
                this.CalculateCommand.Execute(null);

            if (String.IsNullOrEmpty(InputStr))
                ResultStr = String.Empty;
        }
        public ICommand RemoveLastCharCommand
        {
            get
            {
                if (_removeLastCharCommand == null)
                    _removeLastCharCommand = new RelayCommand(param => { RemoveLastChar(); }, param => CanExecuteRemoveLastCharCommand());

                return _removeLastCharCommand;
            }
        }

        private bool CanExecuteInputNumberDecimalSeparatorCommand()
        {
            if (InputStr.Split(_operators).Last().Contains(_cultureInfo.NumberFormat.NumberDecimalSeparator))
                return false;

            return true;
        }
        private void InputPoint()
        {
            if (InputStr.Split(_operators).Last() == String.Empty)
                InputStr += "0" + _cultureInfo.NumberFormat.NumberDecimalSeparator;
            else
                InputStr += _cultureInfo.NumberFormat.NumberDecimalSeparator;
        }
        public ICommand InputNumberDecimalSeparatorCommand
        {
            get
            {
                if (_inputNumberDecimalSeparator == null)
                    _inputNumberDecimalSeparator = new RelayCommand(param => { InputPoint(); }, param => CanExecuteInputNumberDecimalSeparatorCommand());

                return _inputNumberDecimalSeparator;
            }
        }

        private bool CanExecuteCalculateCommand()
        {
            if (String.IsNullOrWhiteSpace(InputStr) || IsOperatorOrPoint(InputStr.Last()))
                return false;

            return true;
        }
        public ICommand CalculateCommand
        {
            get
            {
                if (_calculateCommand == null)
                    _calculateCommand = new RelayCommand(param => { OperationHelper('='); }, param => CanExecuteCalculateCommand());

                return _calculateCommand;
            }
        }

        private bool CanExecuteResetCommand()
        {
            if (String.IsNullOrWhiteSpace(InputStr))
                return false;

            return true;
        }
        private void Reset()
        {
            InputStr = String.Empty;
            ResultStr = String.Empty;
            _calculator.Reset();
            _lastOperator = '+';
        }
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => { Reset(); }, param => CanExecuteResetCommand());

                return _resetCommand;
            }
        }

        private void CloseWin(Object win)
        {
            (win as Window).Close();
        }
        public ICommand CloseWinCommand
        {
            get
            {
                if (_closeWinCommand == null)
                    _closeWinCommand = new RelayCommand(CloseWin);

                return _closeWinCommand;
            }
        }

        private void MinimizeWin(Object win)
        {
            (win as Window).WindowState = WindowState.Minimized;
        }
        public ICommand MinimizeWinCommand
        {
            get
            {
                if (_minimizeWinCommand == null)
                    _minimizeWinCommand = new RelayCommand(MinimizeWin);

                return _minimizeWinCommand;
            }
        }
        #endregion

        #region Helper methods
        private bool IsOperatorOrPoint(char character)
        {
            if (_operators.Contains(character) || character == _cultureInfo.NumberFormat.NumberDecimalSeparator.First())
                return true;

            return false;
        }
        private void OperationHelper(char @operator)
        {
            if (@operator == '/' || @operator == '*')
            {
                if ((_lastOperator == '+' || _lastOperator == '-') && InputStr.TrimStart(new []{'-'}).Split(_operators).Length > 1)
                    InputStr = String.Format("({0})", InputStr);
            }

            double operand = ParseLastOperand();

            if (operand == 0.0D && _lastOperator == '/')
            {
                MessageBox.Show("Connot divide by zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _calculator.AddOperation(_lastOperator, operand);

            if (@operator != '=')
            {
                InputStr += @operator;
                _lastOperator = @operator;
                ResultStr = String.Empty;
            }
            else
            {
                string result = _calculator.Calculate().ToString("F15", _cultureInfo.NumberFormat).Remove(15);

                if (result.Contains(_cultureInfo.NumberFormat.NumberDecimalSeparator))
                    result = result.TrimEnd(new []{'0'});

                if (result.Last() == _cultureInfo.NumberFormat.NumberDecimalSeparator.First())
                    result = result.Remove(result.Length - 1);

                ResultStr = result;
                _calculator.RemoveLastOperation();
            }
        }
        private double ParseLastOperand()
        {
            double operand = Double.Parse(InputStr.Split(_operators).Last().TrimEnd(new []{')'}), _cultureInfo.NumberFormat);

            if (InputStr.TrimStart(new []{'('}).First() == '-' && InputStr.Split(_operators).Length == 2)
                return -operand;

            return operand;
        }
        #endregion
    }
}
