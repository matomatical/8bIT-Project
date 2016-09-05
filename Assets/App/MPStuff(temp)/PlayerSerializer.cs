using System;

namespace Team8bITProject
{
	public class PlayerSerializer
	{
		public static readonly char[] DELIMINATOR_ARRAY = new[] {','};
		public static readonly String DELIMINATOR_STRING = ",";

		public static Position StringToPosition(String update)
		{
			String[] data = update.Split (DELIMINATOR_ARRAY);
			return new Position (Single.Parse(data [0]), Single.Parse(data [1]));
		}

		public static String PositionToString(Position pos)
		{
			float posx = pos.GetPosx ();
			float posy = pos.GetPosy ();

			return posx.ToString ()+ DELIMINATOR_STRING + posy.ToString ();
		}
	}
}

