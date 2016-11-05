using System;
using System.Globalization;

namespace Core.Data.CachedObjects.Collections
{
	internal class NamedObject
	{
		private string _name;

		public NamedObject(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(name);
			_name = name;
		}

		public override string ToString()
		{
			if (_name[0] != 123)
				_name = string.Format(CultureInfo.InvariantCulture, "{{{0}}}", _name);
			return _name;
		}
	}
}
