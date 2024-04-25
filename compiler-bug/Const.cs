namespace Application.UI.MVVM.ObservedProperty
{
    /// <summary>
    /// A property with a constant value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Const<T>(T value)
    {
        /// <summary>
        /// Retrieve the value of this property
        /// </summary>
        public T GetValue
        {
            get
            {
                return _value;
            }
        }
        protected T _value = value;

        /// <summary>
        /// `ToString()` override.
        /// </summary>
        /// <returns></returns>
        public override string? ToString() => GetValue?.ToString();

        /// <summary>
        /// Using an implicit operator allows for nicer syntax when accessing this value.
        /// </summary>
        public static implicit operator Const<T>(T value) => new(value);

        /// <summary>
        /// Using an implicit operator allows for nicer syntax when accessing this value.
        /// </summary>
        public static implicit operator T(Const<T> property) => property.GetValue;
    }

}
