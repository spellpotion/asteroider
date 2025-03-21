using System;

namespace Asteroider
{
    public class EventProxy<T>
    {
        private Action<T> listener;
        private readonly Action<T> invoker;

        public EventProxy(out Action<T> invoker)
        {
            this.invoker = x => listener?.Invoke(x);
            invoker = this.invoker;
        }

        public void AddListener(Action<T> listener)
        {
            this.listener += listener;
        }

        public void RemoveListener(Action<T> listener)
        {
            this.listener -= listener;
        }
    }

    public class EventProxy
    {
        private Action listener;
        private readonly Action invoker;

        public EventProxy(out Action invoker)
        {
            this.invoker = () => listener?.Invoke();
            invoker = this.invoker;
        }

        public void AddListener(Action listener)
        {
            this.listener += listener;
        }

        public void RemoveListener(Action listener)
        {
            this.listener -= listener;
        }
    }
}
