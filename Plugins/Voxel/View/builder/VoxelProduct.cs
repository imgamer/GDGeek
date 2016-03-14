using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	public class VoxelProduct{

		//private VoxelLattice lattice_;
		//private VoxelPolygon polygon_;


		public Vector3 min = new Vector3(999, 999, 999);
		public Vector3 max = new Vector3(-999, -999, -999);
		public Dictionary<VectorInt3, VoxelHandler> voxels = null;

		public VoxelDrawData draw = null;
		//public VoxelHandler[] datas = null;

	}

}