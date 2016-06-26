
namespace Sugar
{
    public interface ICommandManager
    {
        void AddCommandHandler(ICommand handler);
        bool ExecuteCommand(string commandName, string param);
    }
}
