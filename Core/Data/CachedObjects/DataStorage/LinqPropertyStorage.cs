using System.Windows;

namespace Core.Data.CachedObjects.DataStorage
{
	public abstract class LinqPropertyStorage
	{
		protected LinqEntity ownerEntity;
		internal abstract DependencyProperty GetPropertyInternal();
		internal abstract object GetEffectiveValueInternal();
	}
}
