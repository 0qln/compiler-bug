using System.Collections;
using System.Runtime.CompilerServices;

namespace Application.UI.MVVM.ObservedProperty
{
    internal interface IImmutable
    {
        /// <summary>
        /// This event will be invoked whenever the value of this property changes.
        /// </summary>
        public event EventHandler? OnChanged;
    }


    /// <summary>
    /// A property whose value or value source/binding cannot be changed after instanciation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Immutable<T> : Const<T>, IImmutable
    {
        /// <summary>
        /// This event will be invoked whenever the value of this property changes.
        /// </summary>
        public event EventHandler? OnChanged;


        protected virtual T SetValue
        {
            set
            {
                // TODO: only invoke, if the value actually changed.

                _value = value;
                OnChanged?.Invoke();
            }
        }

        private readonly List<(IImmutable source, EventHandler handler)> _bindings = new();


        /// <summary>
        /// Create a new property that is bound to a specific value. This will be effectively the same as a Const<T>.
        /// </summary>
        /// <param name="value"></param>
        public Immutable(T value) : base(value)
        {
        }

        /// <summary>
        /// Create a new property, whose value is bound to another property.
        /// </summary>
        /// <param name="bindingSource"></param>
        public Immutable(Const<T> bindingSource) : base(bindingSource.GetValue)
        {
            BindTo(bindingSource);
        }

        /// <summary>
        /// Create a new property, whose value is bound to another property using the specified value converter.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="bindingSource"></param>
        /// <returns></returns>
        public static Immutable<T> CreateBinding(Const<T> bindingSource)
        {
            return Immutable<T>.CreateBinding(bindingSource: bindingSource, valueConverter: x => x);
        }

        /// <summary>
        /// Create a new property, whose value is bound to another property using the specified value converter.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="bindingSource"></param>
        /// <param name="valueConverter"></param>
        /// <returns></returns>
        public static Immutable<T> CreateBinding<TIn>(Const<TIn> bindingSource, ValueConverter<TIn, T> valueConverter)
        {
            Immutable<T> result = new(valueConverter(bindingSource.GetValue));
            result.BindTo(bindingSource, valueConverter);
            return result;
        }

        /// <summary>
        /// Create a new property, whose value is bound to another property using the specified value converter.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="bindingSource"></param>
        /// <param name="valueConverter"></param>
        /// <returns></returns>
        public static Immutable<T> CreateBinding(ITuple bindingSources, ValueConverter<ITuple, T> valueConverter)
        {
            Immutable<T> result = new(valueConverter(bindingSources));
            result.BindTo(bindingSources, valueConverter);
            return result;
        }

        //protected void BindTo<T1>(EventAccessor<T1> @event, Func<T> func)
        //{
        //    SetValue = func();
        //    EventHandler handler = () => SetValue = func();
        //    _bindings.Add((@event, handler));
        //    @event.OnChanged += handler;
        //}

        protected void BindTo(Const<T> bindingSource)
        {
            SetValue = bindingSource.GetValue;

            if (bindingSource is IImmutable immutable)
            {
                EventHandler handler = () => SetValue = bindingSource.GetValue;
                _bindings.Add((immutable, handler));
                immutable.OnChanged += handler;
            }
        }

        protected void BindTo<TIn>(Const<TIn> bindingSource, ValueConverter<TIn, T> valueConverter)
        {
            SetValue = valueConverter(bindingSource.GetValue);

            if (bindingSource is IImmutable immutable)
            {
                EventHandler handler = () => SetValue = valueConverter(bindingSource.GetValue);
                _bindings.Add((immutable, handler));
                immutable.OnChanged += handler;
            }
        }

        protected void BindTo(ITuple bindingSources, ValueConverter<ITuple, T> valueConverter)
        {
            SetValue = valueConverter(bindingSources);

            for (int i = 0; i < bindingSources.Length; i++)
            {
                if (bindingSources[i] is IImmutable immutable)
                {
                    EventHandler handler = () => SetValue = valueConverter(bindingSources);
                    _bindings.Add((immutable, handler));
                    immutable.OnChanged += handler;
                }
            }
        }

        protected void ClearBindings()
        {
            while (_bindings.Any())
            {
                var binding = _bindings.First();
                binding.source.OnChanged -= binding.handler;
                _bindings.Remove(binding);
            }
        }


        /// <summary>
        /// Using an implicit operator allows for nicer syntax when accessing this value.
        /// </summary>
        public static implicit operator Immutable<T>(T value) => value;

        /// <summary>
        /// Using an implicit operator allows for nicer syntax when accessing this value.
        /// </summary>
        public static implicit operator T(Immutable<T> property) => property.GetValue;
    }


}
