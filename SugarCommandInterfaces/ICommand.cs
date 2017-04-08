

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

        string[] ParamDescriptionList
        {
            get;
        }

        bool[] ParamRequired
        {
            get;
        }

        string Description
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
