using System;
using System.Windows.Input;

namespace InteractiveMusicScales
{
    internal class CommandParametrized : ICommand
    {
        private Action<object> actionToExecute;

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