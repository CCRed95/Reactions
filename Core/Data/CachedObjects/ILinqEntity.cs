namespace Core.Data.CachedObjects
{
	public interface ILinqEntity
	{
		void OnLoaded();

		void SendPropertyChanged(string propertyName);

		void OnPropertyChanged(string propertyName);

		void InvalidateAllDependentProperties();

		object GetValueBase(LinqPropertyBase linqProperty);
	}
}
