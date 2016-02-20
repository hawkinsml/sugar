using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    interface ICommand
    {
        string Name
        {
            get;
            //set;
        }
        void Execute(string[] args);
    }
}
