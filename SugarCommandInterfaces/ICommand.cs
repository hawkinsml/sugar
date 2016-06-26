

namespace Sugar
{
    public interface ICommand
    {
        string Name
        {
            get;
        }

        string[] ParamList
        {
            get;
        }

        string Help
        {
            get;
        }

        bool Execute(string[] args);
    }
}
