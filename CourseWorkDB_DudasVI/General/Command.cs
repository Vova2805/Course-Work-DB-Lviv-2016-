using System;
using System.Windows.Input;
using Apex.MVVM;

namespace CourseWorkDB_DudasVI.General
{
    public class Command : ICommand
    {
        protected Action action;

        private bool canExecute;
        protected Action<object> parameterizedAction;

        public Command(Action action, bool canExecute = true)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public Command(Action<object> parameterizedAction, bool canExecute = true)
        {
            this.parameterizedAction = parameterizedAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute
        {
            get { return canExecute; }
            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    var canExecuteChanged = CanExecuteChanged;
                    if (canExecuteChanged != null)
                        canExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return canExecute;
        }

        void ICommand.Execute(object parameter)
        {
            DoExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public event CancelCommandEventHandler Executing;

        public event CommandEventHandler Executed;

        protected void InvokeAction(object param)
        {
            var theAction = action;
            var theParameterizedAction = parameterizedAction;
            if (theAction != null)
                theAction();
            else if (theParameterizedAction != null)
                theParameterizedAction(param);
        }

        protected void InvokeExecuted(CommandEventArgs args)
        {
            var executed = Executed;

            if (executed != null)
                executed(this, args);
        }

        protected void InvokeExecuting(CancelCommandEventArgs args)
        {
            var executing = Executing;

            if (executing != null)
                executing(this, args);
        }

        public virtual void DoExecute(object param)
        {
            var args =
                new CancelCommandEventArgs {Parameter = param, Cancel = false};
            InvokeExecuting(args);

            if (args.Cancel)
                return;

            InvokeAction(param);
            InvokeExecuted(new CommandEventArgs {Parameter = param});
        }
    }
}