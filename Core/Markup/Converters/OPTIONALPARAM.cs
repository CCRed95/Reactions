namespace Core.Markup.Converters
{
	public class OPTIONALPARAM<T> : OPTIONALPARAM
	{
		private T _value;
		public T Value
		{
			get { return _value; }
			set
			{
				_value = value;
				IsProvided = true;
			}
		}

		public OPTIONALPARAM()
		{
			IsProvided = false;
		}
		public OPTIONALPARAM(T value)
		{
			IsProvided = true;
			Value = value;
		}
	}
	public abstract class OPTIONALPARAM
	{
		public bool IsProvided { get; protected set; }
	}
}