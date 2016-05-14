namespace Core.Helpers.CLREventHelpers
{
	public delegate void ParameterizedEventHandler<in T>(T p1);
	public delegate void ParameterizedEventHandler<in T1, in T2>(T1 p1, T2 p2);
}
