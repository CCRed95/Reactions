using System;

namespace Material.Design.Providers
{
	public class ProviderContext
	{
		public int CycleLength { get; }
		public Guid AccessKey { get; }
		public int ProviderIndex { get; set; }

		public ProviderContext(int cycleLength)
		{
			CycleLength = cycleLength;
			AccessKey = Guid.NewGuid();
		}
		public static ProviderContext Default => new ProviderContext(0);
	}
}
