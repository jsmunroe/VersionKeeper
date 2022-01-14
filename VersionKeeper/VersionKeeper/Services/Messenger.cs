using System;
using System.Collections.Generic;
using System.Linq;
using VersionKeeper.Contracts;

namespace VersionKeeper.Services
{
    public class Messenger : IMessenger
    {
        private List<Subscription> _subscriptions = new List<Subscription>();


        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
       public void Subscribe<TMessage>(object target, Action<TMessage> callback) 
        {
            #region Argument Validation

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            #endregion

            var subscription = new Subscription
            {
                MessageType = typeof(TMessage),
                Target = target,
                Callback = message => callback((TMessage)message),
            };

            _subscriptions.Add(subscription);
        }

        /// <exception cref="ArgumentNullException">Thrown if <paramref name="message"/> is null.</exception>
        public void Publish<TMessage>(TMessage message)
        {
            #region Argument Validation

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            #endregion

            var subscriptions = _subscriptions.Where(p => p.MessageType.IsAssignableFrom(typeof(TMessage)));

            foreach (var subscription in subscriptions)
            {
                subscription.Callback(message);
            }
        }

        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is null.</exception>
        public void Unsubscribe(object target)
        {
            #region Argument Validation

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            #endregion

            _subscriptions.RemoveAll(p => p.Target == target);
        }
        private class Subscription
        {
            public Type MessageType { get; set; }
            public object Target { get; set; }
            public Action<object> Callback { get; set; }
        }
    }
}
