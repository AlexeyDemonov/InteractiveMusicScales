using System;
using System.Windows.Input;

namespace InteractiveMusicScales
{
    internal class Command : ICommand
    {
        private Action actionToExecute;

        public Command(Action actionToExecute)
        {
            this.actionToExecute = actionToExecute;
        }

        public void Execute(object parameter)
        {
            actionToExecute.Invoke();
        }

        //==============================================================
        //Not used
        public event EventHandler CanExecuteChanged { add {/*Do nothing*/} remove {/*Do nothing*/} }

        public bool CanExecute(object parameter) => true;
    }
}