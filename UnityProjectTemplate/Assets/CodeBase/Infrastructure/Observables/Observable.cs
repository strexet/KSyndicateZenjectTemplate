using System;

namespace CodeBase.Infrastructure.Observables
{
    /// <summary>
    ///     Wraps a value in order to allow observing its value change
    /// </summary>
    /// <example>
    ///     var obs = new Observable<int>(123);
    ///     obs.OnChanged += (o, oldVal, newVal) => Log("changed from " + oldVal + " to " + newVal);
    ///     obs.Value = 456; // dispatches OnChanged
    /// </example>
    /// <author>Jackson Dunstan, http://JacksonDunstan.com/articles/3547</author>
    /// <license>MIT</license>
    [Serializable]
	public class Observable<TValue> : IEquatable<Observable<TValue>>
	{

		public Action<Observable<TValue>, TValue, TValue> OnChanged;

		TValue value;

		public Observable() { }

		public Observable(TValue value) => this.value = value;

		public TValue Value {
			get => value;
			set {
				var oldValue = this.value;
				this.value = value;
				OnChanged?.Invoke(this, oldValue, value);
			}
		}

		public bool Equals(Observable<TValue> other) => other.value.Equals(value);

		public static implicit operator Observable<TValue>(TValue observable) => new(observable);

		public static explicit operator TValue(Observable<TValue> observable) => observable.value;

		public override string ToString() => value.ToString();

		public override bool Equals(object other)
		{
			var observable = other as Observable<TValue>;
			return observable != null
				&& observable.value.Equals(value);
		}

		public override int GetHashCode() => value.GetHashCode();
	}
}