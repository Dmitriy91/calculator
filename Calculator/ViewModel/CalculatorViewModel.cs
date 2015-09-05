using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    public class CalculatorViewModel: BaseViewModel
    {
        #region Fields
        private string _inputStr = String.Empty;
        private string _resultStr = String.Empty;
        private char _lastOperator = '+';
        private readonly Model.Calculator _calculator = new Model.Calculator();
        private static readonly char [] _operators = { '/', '*', '+', '-' };
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
        private RelayCommand _inputPointCommand;
        private RelayCommand _inputNullCommand;
        private RelayCommand _inputOneCommand;
        private RelayCommand _inputTwoCommand;
        private RelayCommand _inputThreeCommand;
        private RelayCommand _inputFourCommand;
        private RelayCommand _inputFiveCommand;
        private RelayCommand _inputSixCommand;
        private RelayCommand _inputSevenCommand;
        private RelayCommand _inputEightCommand;
        private RelayCommand _inputNineCommand;
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

        private void Divide()
        {
            if ((_lastOperator == '+' || _lastOperator == '-') && _inputStr.TrimStart(new char[] { '-' }).Split(_operators).Length >= 2)
                _inputStr = String.Format("({0})", _inputStr);

            OperationHelper('/');
        }
        public ICommand DivideCommand
        {
            get
            {
                if (_divideCommand == null)
                    _divideCommand = new RelayCommand((param) => { Divide(); }, (param) => { return CanExecuteOperationCommand(); });

                return _divideCommand;
            }
        }

        private void Multiply()
        {
            if ((_lastOperator == '+' || _lastOperator == '-') && _inputStr.TrimStart(new char[] { '-' }).Split(_operators).Length >= 2)
                _inputStr = String.Format("({0})", _inputStr);

            OperationHelper('*');
        }
        public ICommand MultiplyCommand
        {
            get
            {
                if (_multiplyCommand == null)
                    _multiplyCommand = new RelayCommand((param) => { Multiply(); }, (param) => { return CanExecuteOperationCommand(); });

                return _multiplyCommand;
            }
        }

        public void Add()
        {
            OperationHelper('+');
        }
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new RelayCommand((param) => { Add(); }, (param) => { return CanExecuteOperationCommand(); });

                return _addCommand;
            }
        }

        private void Substruct()
        {
            OperationHelper('-');
        }
        public ICommand SubstructCommand
        {
            get
            {
                if (_substructCommand == null)
                    _substructCommand = new RelayCommand((param) => { Substruct(); }, (param) => { return CanExecuteOperationCommand(); });

                return _substructCommand;
            }
        }

        private bool CanExecuteInputDigitCommand()
        {
            if (InputStr.Split(_operators).Last().Length >= 15)
                return false;

            return true;
        }

        public ICommand InputNullCommand
        {
            get
            {
                if (_inputNullCommand == null)
                    _inputNullCommand = new RelayCommand((param) => { InputStr += "0"; }, (param) => CanExecuteInputDigitCommand());

                return _inputNullCommand;
            }
        }
        public ICommand InputOneCommand
        {
            get
            {
                if (_inputOneCommand == null)
                    _inputOneCommand = new RelayCommand((param) => { InputStr += "1"; }, (param) => CanExecuteInputDigitCommand());

                return _inputOneCommand;
            }
        }
        public ICommand InputTwoCommand
        {
            get
            {
                if (_inputTwoCommand == null)
                    _inputTwoCommand = new RelayCommand((param) => { InputStr += "2"; }, (param) => CanExecuteInputDigitCommand());

                return _inputTwoCommand;
            }
        }
        public ICommand InputThreeCommand
        {
            get
            {
                if (_inputThreeCommand == null)
                    _inputThreeCommand = new RelayCommand((param) => { InputStr += "3"; }, (param) => CanExecuteInputDigitCommand());

                return _inputThreeCommand;
            }
        }
        public ICommand InputFourCommand
        {
            get
            {
                if (_inputFourCommand == null)
                    _inputFourCommand = new RelayCommand((param) => { InputStr += "4"; }, (param) => CanExecuteInputDigitCommand());

                return _inputFourCommand;
            }
        }
        public ICommand InputFiveCommand
        {
            get
            {
                if (_inputFiveCommand == null)
                    _inputFiveCommand = new RelayCommand((param) => { InputStr += "5"; }, (param) => CanExecuteInputDigitCommand());

                return _inputFiveCommand;
            }
        }
        public ICommand InputSixCommand
        {
            get
            {
                if (_inputSixCommand == null)
                    _inputSixCommand = new RelayCommand((param) => { InputStr += "6"; }, (param) => CanExecuteInputDigitCommand());

                return _inputSixCommand;
            }
        }
        public ICommand InputSevenCommand
        {
            get
            {
                if (_inputSevenCommand == null)
                    _inputSevenCommand = new RelayCommand((param) => { InputStr += "7"; }, (param) => CanExecuteInputDigitCommand());

                return _inputSevenCommand;
            }
        }
        public ICommand InputEightCommand
        {
            get
            {
                if (_inputEightCommand == null)
                    _inputEightCommand = new RelayCommand((param) => { InputStr += "8"; }, (param) => CanExecuteInputDigitCommand());

                return _inputEightCommand;
            }
        }
        public ICommand InputNineCommand
        {
            get
            {
                if (_inputNineCommand == null)
                    _inputNineCommand = new RelayCommand((param) => { InputStr += "9"; }, (param) => CanExecuteInputDigitCommand());

                return _inputNineCommand;
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
            if (_operators.Contains(InputStr.Last()) && (!InputStr.Equals("-")))
            {
                _calculator.RemoveLastOperation();
                int prevOperatorIndex = InputStr.Remove(InputStr.Length - 1)
                    .ToList()
                    .FindLastIndex((character) => _operators.Contains(character));

                if (prevOperatorIndex != -1)
                    _lastOperator = InputStr.ToCharArray()[prevOperatorIndex];
                else
                    _lastOperator = '+';
            }

            if (InputStr.Last() == ')')
                InputStr = InputStr.Remove(0, 1);

            InputStr = InputStr.Remove(InputStr.Length - 1);
            ResultStr = String.Empty;
        }
        public ICommand RemoveLastCharCommand
        {
            get
            {
                if (_removeLastCharCommand == null)
                    _removeLastCharCommand = new RelayCommand((param) => { RemoveLastChar(); }, (param) => { return CanExecuteRemoveLastCharCommand(); });

                return _removeLastCharCommand;
            }
        }

        private bool CanExecuteInputPointCommand()
        {
            if (InputStr.Split(_operators).Last().Contains(','))
                return false;

            return true;
        }
        public ICommand InputPointCommand
        {
            get
            {
                if (_inputPointCommand == null)
                    _inputPointCommand = new RelayCommand((param) => { InputStr += ","; }, (param) => { return CanExecuteInputPointCommand(); });

                return _inputPointCommand;
            }
        }

        private bool CanExecuteCalculateCommand()
        {
            if (String.IsNullOrWhiteSpace(InputStr) || _operators.Contains(InputStr.Last()))
                return false;

            return true;
        }
        private void Calculate()
        {
            double operand = ParseLastOperand();
            
            if (operand == 0.0D && _lastOperator == '/')
            {
                MessageBox.Show("Connot divide by zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _calculator.AddOperation(_lastOperator, operand);

            string result = _calculator.Calculate().ToString("F15").Remove(15);

            if(result.Contains(','))
                result = result.TrimEnd(new char[]{'0'});

            if (result.Last() == ',')
                result = result.Remove(result.Length - 1);

            InputStr = result;
            ResultStr = result;

            _calculator.Reset();
            _lastOperator = '+';
        }
        public ICommand CalculateCommand
        {
            get
            {
                if (_calculateCommand == null)
                    _calculateCommand = new RelayCommand((param) => { Calculate(); }, (param) => { return CanExecuteCalculateCommand(); });

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
            InputStr = string.Empty;
            ResultStr = string.Empty;
            _calculator.Reset();
            _lastOperator = '+';
        }
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand((param) => { Reset(); }, (param) => { return CanExecuteResetCommand(); });

                return _resetCommand;
            }
        }

        private void CloseWin(Object param)
        {
            Window win = param as Window;
            win.Close();
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

        private void MinimizeWin(Object param)
        {
            Window win = param as Window;
            win.WindowState = WindowState.Minimized;
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
            if (character == '/' || character == '*' || character == '+' || character == '-' || character == '.')
                return true;

            return false;
        }
        private void OperationHelper(char @operator)
        {
            double operand = ParseLastOperand();
            if (operand == 0.0D && _lastOperator =='/')
            {
                MessageBox.Show("Connot divide by zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _calculator.AddOperation(_lastOperator, operand);
            InputStr += @operator;
            _lastOperator = @operator;
            ResultStr = String.Empty;
        }
        private double ParseLastOperand()
        {
            double operand = Double.Parse(InputStr.Split(_operators).Last().TrimEnd(new char[] { ')' }));

            if (InputStr.TrimStart(new char[]{'('}).First() == '-' && InputStr.Split(_operators).Length == 2)
                return -operand;

            return operand;
        }
        #endregion
    }
}
