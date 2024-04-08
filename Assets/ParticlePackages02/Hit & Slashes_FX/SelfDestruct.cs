using UnityEngine;
using System.Collections;

namespace Effect2
{
	public class SelfDestruct : MonoBehaviour
	{
		public float selfdestruct_in = 4;

		void Start()
		{
			Destroy(gameObject, selfdestruct_in);
		}
	}
}
