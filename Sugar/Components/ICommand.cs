using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    public interface ICommand
    {
        string Name
        {
            get;
            //set;
        }

        string[] ParamList
        {
            get;
            //set;
        }

        string Help
        {
            get;
            //set;
        }

        bool Execute(string[] args);
    }
}
