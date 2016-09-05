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

		float GetPosx()
		{
			return posx;
		}

		float GetPosy()
		{
			return posy;
		}
	}
}

