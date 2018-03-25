namespace Domain.Helper
{
	public static class StringHelper
	{
		public static int FromStringToInt(this string str)
		{
			int a;
			str = str.Trim('(',')',' ');
			int.TryParse(str, out a);
			return a;
		}
	}
}
