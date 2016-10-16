using UnityEngine;
using System.Collections.Generic;

namespace xyz._8bITProject.cooperace {
	public class RollingAverage {

		private int nmax;
		public float average { get; private set; }

		private Queue<long> data;

		public RollingAverage (int nmax) {
			
			if (nmax < 1) {
				this.nmax = 1;
			} else {
				this.nmax = nmax;
			}

			data = new Queue<long> ();

			average = 0;
		}

		public void Add (long num) {
			long popped;
			int n;

			data.Enqueue (num);
			n = data.Count;

			if (n > nmax) {
				
				popped = data.Dequeue ();

				average += (num - popped) / (n-1);

			} else {

				average = ((average * (n-1)) + num) / n;

			}
		}
	}
}
