using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Collections
{
	public class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
	{
		TKey key;
		IEnumerable<TElement> elements;

		public Grouping(TKey key, IEnumerable<TElement> elements)
		{
			this.key = key;
			this.elements = elements;
		}

		public TKey Key { get { return key; } }

		public IEnumerator<TElement> GetEnumerator()
		{
			return elements.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return elements.GetEnumerator();
		}
	}
}
