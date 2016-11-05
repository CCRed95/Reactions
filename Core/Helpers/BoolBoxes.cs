namespace Core.Helpers
{
	internal static class BoolBoxes
	{
		internal static object TrueBox = true;
		internal static object FalseBox = false;

		internal static object Box(bool value)
		{		
			if (value)
				return TrueBox;
			return FalseBox;
		}
	}
}
