using System;

namespace VersionKeeper.Contracts
{
    public interface IMessenger
    {
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
        void Subscribe<TMessage>(object target, Action<TMessage> callback);

        /// <exception cref="ArgumentNullException">Thrown if <paramref name="message"/> is null.</exception>
        void Publish<TMessage>(TMessage message);

        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is null.</exception>
        void Unsubscribe(object target);
    }
}