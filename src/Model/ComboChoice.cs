namespace UnitTestBoilerplate.Model
{
	public class ComboChoice<T>
	{
		public ComboChoice(T value, string display)
		{
			this.Value = value;
			this.Display = display;
		}

		public T Value { get; set; }
		public string Display { get; set; }

		public override string ToString()
		{
			return this.Display;
		}
	}
}
