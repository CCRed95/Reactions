using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace Core.Data.CachedObjects
{
	public class IndependentLinqEntity : LinqEntity, INotifyPropertyChanged
	{
		public virtual bool IsNotifying { get; set; } = true;

		public virtual event PropertyChangedEventHandler PropertyChanged;


		protected void SendPropertyChanged(string propertyName)
		{
			NotifyOfPropertyChangeImpl(propertyName);
		}


		public virtual void NotifyOfPropertyChange([CallerMemberName] string propertyName = null)
		{
			NotifyOfPropertyChangeImpl(propertyName);
			//if (!IsNotifying || PropertyChanged == null)
			//  return;
			//Execute.OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
		}
		private void NotifyOfPropertyChangeImpl(string propertyName)
		{
			if (!IsNotifying || PropertyChanged == null)
				return;
			Execute.OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
		}

		public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
		{
			NotifyOfPropertyChangeImpl(ExpressionExtensions.GetMemberInfo(property).Name);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, e);
		}
	}
}
