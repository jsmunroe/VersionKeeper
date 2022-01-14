using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace VersionKeeper.WPF.Utilities
{
    public class PropertyChangeRelay
    {
        private readonly INotifyPropertyChanged _source;
        private readonly string _propertyName;
        private readonly Action<string> _handler;

        private PropertyChangeRelay(INotifyPropertyChanged source, string propertyName, Action<string> handler)
        {
            _source = source;
            _propertyName = propertyName;
            _handler = handler;

            _source.PropertyChanged += OnPropertyChanged;
        }

        public void Unbind()
        {
            _source.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == _propertyName)
            {
                _handler.Invoke(_propertyName);
            }
        }

        public static PropertyChangeRelay Bind<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> selector, Action<string> handler)
            where TSource : INotifyPropertyChanged
        {
            #region Argument Validation

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            #endregion

            var memberName = GetMemberName(selector.Body);

            return new PropertyChangeRelay(source, memberName, handler);
        }

        public static PropertyChangeRelay Bind<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> selector, Action handler)
            where TSource : INotifyPropertyChanged
        {
            #region Argument Validation

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            #endregion

            var memberName = GetMemberName(selector.Body);

            return new PropertyChangeRelay(source, memberName, i => handler());
        }

        private static string GetMemberName(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)expression).Member.Name;
                case ExpressionType.Convert:
                    return GetMemberName(((UnaryExpression)expression).Operand);
                default:
                    throw new NotSupportedException(expression.NodeType.ToString());
            }
        }
    }
}