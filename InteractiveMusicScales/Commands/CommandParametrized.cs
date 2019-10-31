using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InteractiveMusicScales
{
    class CommandParametrized : ICommand
    {
        Action<object> actionToExecute;

        public CommandParametrized(Action<object> actionToExecute)
        {
            this.actionToExecute = actionToExecute;
        }

        public void Execute(object parameter)
        {
            actionToExecute.Invoke(parameter);
        }

        //==============================================================
        //Not used
        public event EventHandler CanExecuteChanged { add {/*Do nothing*/} remove {/*Do nothing*/} }

        public bool CanExecute(object parameter) => true;
    }
}
