using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UI.MVVM.ObservedProperty
{
    /// <summary>
    /// Expose the `OnChanged` event of a property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class EventAccessor<T>(Immutable<T> value) : IImmutable
    {
        /// <summary>
        /// This event will be invoked whenever the value of the property of this accessor changes.
        /// </summary>
        public event EventHandler? OnChanged
        {
            add
            {
                _property.OnChanged += value;
            }
            remove
            {
                _property.OnChanged -= value;
            }
        }

        protected readonly Immutable<T> _property = value;
    }
}
