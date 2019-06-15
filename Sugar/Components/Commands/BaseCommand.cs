using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{
    abstract public class BaseCommand : ICommand
    {
        public string Name
        {
            get;
            protected set;
        }

        public string[] ParamList
        {
            get;
            protected set;
        }

        public string[] ParamDescriptionList
        {
            get;
            protected set;
        }

        public bool[] ParamRequired
        {
            get;
            protected set;
        }

        public string Description
        {
            get;
            protected set;
        }

        public string Help
        {
            get;
            protected set;
        }

        abstract public bool Execute(string[] args);
    }
}
