namespace Calculator.Model
{
    interface IOperation
    {
        void Execute();
        void Unexecute();
        char Operator { get; }
    }
}
