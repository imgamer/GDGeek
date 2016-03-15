using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	class VoxelRemoveFace: IVoxelBuilder
	{

		public Task task(VoxelProduct product){
			Task task = new Task ();
			task.init = delegate {
				build(product);
			};
			return task;
		}
		public void build(VoxelProduct product){


		}
	}


}