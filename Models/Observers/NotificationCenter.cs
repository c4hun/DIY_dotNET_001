using Todo.Models.Interfaces;

namespace Todo.Models.Observers
{
    public class NotificationCenter : ISubject
    {
        private readonly List<IObserver> _observers = new();

        public void Attach(IObserver observer) => _observers.Add(observer);
        public void Detach(IObserver observer) => _observers.Remove(observer);

        public void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
                observer.Update(message);
        }
    }
}
