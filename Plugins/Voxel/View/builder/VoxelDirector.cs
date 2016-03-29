using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek{
	public class VoxelDirector : MonoBehaviour {



		public Material _material = null;
		private VoxelData2Point begin_ = new VoxelData2Point();
		public List<IVoxelBuilder> list_ = new List<IVoxelBuilder> ();

	
		public void init(){
			list_.Add (new VoxelMeshBuild());
			list_.Add (new VoxelRemoveSameVertices());
			list_.Add (new VoxelRemoveFace());
		}
		public void Awake(){

			init ();

		}
		public delegate void GeometryResult(VoxelGeometry geometry);

		private Task task_(string name, VoxelData[] datas, GeometryResult cb){
			if (list_.Count == 0) {
				init ();
			}

			VoxelProduct product = new VoxelProduct ();

			begin_.setup (datas);

			TaskList tl = new TaskList ();


			tl.push(begin_.task (product));
			for (int i = 0; i < list_.Count; ++i) {

				tl.push(list_[i].task (product));
			}

			TaskManager.PushBack (tl, delegate {
				VoxelGeometry geometry = new VoxelGeometry ();
				geometry.draw (name, product, this.gameObject, this._material);;
				cb(geometry);
			});
			return tl;



		}


		//public List
		public Task task(string name, VoxelData[] datas, GeometryResult cb){

			Task task = new TaskPack (delegate {
				return 	task_(name, datas, cb);
			});

			return task;
		}

		public VoxelGeometry build (VoxelData[] datas, GameObject obj = null)
		{

			if (obj == null) {
				obj = this.gameObject;
			}

			VoxelProduct product = new VoxelProduct();
			begin_.init ();
			begin_.setup (datas);
			begin_.build(product);

		
			for (int i = 0; i < list_.Count; ++i) {
				list_ [i].init ();
				list_[i].build (product);
			}




			VoxelGeometry geometry = new VoxelGeometry();
			geometry.draw ("Mesh", product, obj, this._material);
			return geometry;

		}

	

	}

}