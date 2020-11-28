namespace MZTATest.Commands
{
    public interface ICommand
    {
        bool CanExecute();
        void Execute();
    }

    public interface ICommand<TParam> : ICommand
    {
        bool CanExecute(TParam parameter);
        void Execute(TParam parameter);
    }
}
