namespace Material.Design.Providers
{
	//TODO generate a MaterialProviderContext to autoreset
	public interface IMaterialProvider
	{
		MaterialSet ProvideNext(ProviderContext context);
		void Reset(ProviderContext context);
	}
	public interface IBETAMaterialProvider
	{
		MaterialSet ProvideNext(ref ProviderContext context);
		void Reset(ref ProviderContext context);
	}
}
