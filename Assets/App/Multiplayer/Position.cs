using System;

namespace Team8bITProject
{
	public class Position
	{
		private float posx;
		private float posy;

		public Position (float posx, float posy)
		{
			this.posx = posx;
			this.posy = posy;
		}

		public float GetPosx()
		{
			return posx;
		}

		public float GetPosy()
		{
			return posy;
		}
	}
}

