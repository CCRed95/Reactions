using System;

namespace Material.Design.Providers
{
	public class MaterialProviderKey
	{
		public Guid Key { get; }
		public MaterialProviderKey()
		{
			Key = Guid.NewGuid();
		}
	}
}
