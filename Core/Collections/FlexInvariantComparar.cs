using System;
using System.Collections;
using System.Globalization;

namespace Core.Collections
{
[Serializable]
	public class FlexInvariantComparar : IComparer
	{
		private readonly CompareInfo m_compareInfo;
		public static readonly FlexInvariantComparar Default = new FlexInvariantComparar();

		public FlexInvariantComparar()
		{
			m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		public int Compare(object a, object b)
		{
			var sa = a as string;
			var sb = b as string;
			if (sa != null && sb != null)
				return m_compareInfo.Compare(sa, sb);
			return Comparer.Default.Compare(a, b);
		}
	}
}
