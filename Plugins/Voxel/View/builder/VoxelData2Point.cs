using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelData2Point : VoxelBuilder
	{
		private VoxelData[] data_ = null;
		public VoxelData2Point(VoxelData[] data){
			data_ = data;
		}


		private VoxelHandler data2Handler (VoxelData data)
		{

			VoxelHandler handler = new VoxelHandler();

			handler.position = new VectorInt3(data.pos.x, data.pos.y, data.pos.z);
			handler.color = data.color;
			handler.id = data.id;

			return handler;

		}
		public override void build(VoxelProduct product){
			
			product.min = new Vector3(999, 999, 999);
			product.max = new Vector3(-999, -999, -999);
			product.voxels = new Dictionary<VectorInt3, VoxelHandler>();
			for (int i=0; i<data_.Length; ++i) {
				VoxelData d = data_ [i];
				product.min.x = Mathf.Min (product.min.x, d.pos.x);
				product.min.y = Mathf.Min (product.min.y, d.pos.y);
				product.min.z = Mathf.Min (product.min.z, d.pos.z);
				product.max.x = Mathf.Max (product.max.x, d.pos.x);
				product.max.y = Mathf.Max (product.max.y, d.pos.y);
				product.max.z = Mathf.Max (product.max.z, d.pos.z);

			}
			for (int i=0; i<data_.Length; ++i) {

				VoxelHandler handler = data2Handler(data_[i]);
				product.voxels.Add (handler.position, handler);	

			}




		}
	}


}