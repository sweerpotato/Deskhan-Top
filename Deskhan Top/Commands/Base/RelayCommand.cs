using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DeskhanTop.Commands
{
    public class RelayCommand<T> : ICommand where T : INotifyPropertyChanged
    {
        #region Properties and Fields

        private Action _Action = null;
        private Action<object> _ParamAction = null;
        private Func<bool> _CanExecute = null;
        private readonly List<string> _Properties = new List<string>();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of this class with a non-parameterized action
        /// </summary>
        /// <param name="action">The action to perform when this command is executed</param>
        /// <param name="canExecute">Encapsulated method indicating if the command can be executed or not</param>
        public RelayCommand(Action action, Func<bool> canExecute)
        {
            _Action = action;
            _CanExecute = canExecute;
        }

        /// <summary>
        /// Creates an instance of this class with a parameterized action
        /// </summary>
        /// <param name="paramAction">The parameterized action to perform when this command is executed</param>
        /// <param name="canExecute">Encapsulated method indicating if the command can be executed or not</param>
        public RelayCommand(Action<object> paramAction, Func<bool> canExecute)
        {
            _ParamAction = paramAction;
            _CanExecute = canExecute;
        }

        /// <summary>
        /// Creates an instance of this class with a non-parameterized action and binds the executability to the passed property expression
        /// </summary>
        /// <param name="viewModel">The ViewModel to listen to changed properties on</param>
        /// <param name="action">The action to perform when this command is executed</param>
        /// <param name="canExecute">Encapsulated method indicating if the command can be executed or not</param>
        /// <param name="propertySelector">An expression indicating which properties to watch on the passed ViewModel</param>
        public RelayCommand(T viewModel, Action action, Func<bool> canExecute, Expression<Func<T, object>> propertySelector)
            : this(action, canExecute)
        {
            _Properties = RegisterProperties(propertySelector);
            viewModel.PropertyChanged += PropertyChanged;
        }

        #endregion

        #region Private Methods

        private List<string> RegisterProperties(Expression<Func<T, object>> propertySelector)
        {
            List<string> properties = new List<string>();
            MemberExpression memberExpression = propertySelector.Body as MemberExpression;

            if (memberExpression != null)
            {
                properties.Add(memberExpression.Member.Name);
            }
            else
            {
                UnaryExpression unaryExpression = propertySelector.Body as UnaryExpression;

                if (unaryExpression != null)
                {
                    properties.Add(((MemberExpression)unaryExpression.Operand).Member.Name);
                }
                else
                {
                    if (propertySelector.Body.NodeType == ExpressionType.New)
                    {
                        foreach (Expression expression in ((NewExpression)propertySelector.Body).Arguments)
                        {
                            MemberExpression argMemberExpression = expression as MemberExpression;

                            if (argMemberExpression != null)
                            {
                                properties.Add(argMemberExpression.Member.Name);
                            }
                            else
                            {
                                throw new SyntaxErrorException(
                                    "The property selector has to be an expression which returns a new object containing a list of properties");
                            }
                        }
                    }
                    else
                    {
                        throw new SyntaxErrorException(
                            "The property selector has to be an expression which returns a new object containing a list of properties");
                    }
                }
            }

            return properties;
        }

        #endregion

        #region Public Methods

        public bool CanExecute(object param = null)
        {
            return _CanExecute();
        }

        public void Execute(object param = null)
        {
            if (_ParamAction == null)
            {
                _Action();
            }
            else
            {
                _ParamAction(param);
            }
        }

        #endregion

        #region Event Handlers

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_Properties.Contains(e.PropertyName))
            {
                _CanExecute();
            }
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion
    }
}
