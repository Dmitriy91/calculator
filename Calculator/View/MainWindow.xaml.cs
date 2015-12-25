using System.Windows;
using System.Windows.Input;
using Calculator.ViewModel;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CalculatorViewModel _calculatorViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _calculatorViewModel = new CalculatorViewModel();
            DataContext = _calculatorViewModel;
        }

        private void WindowHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MainWin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D8 && (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))) 
                if (this.MultiplicationBtn.Command.CanExecute(null))
                    this.MultiplicationBtn.Command.Execute(null);
        }
    }
}

 