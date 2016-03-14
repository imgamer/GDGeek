using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek{
	public class VoxelDirector : MonoBehaviour {



		public Material _material = null;
		public VoxelGeometry _geometry;
		public VoxelProduct _product;





		public void setLayer (int layer)
		{
			this.gameObject.layer = layer;
			this._geometry._mesh.gameObject.layer = layer;

		}


		public Dictionary<VectorInt3, VoxelHandler> voxels{
			get{
				
				return _product.voxels;
			}
		}



		public bool empty{
			get{
				return this._geometry == null;
			}
		}

		public void build (VoxelData[] datas)
		{
			if (empty) {

				this._product = new VoxelProduct();

				VoxelData2Point vd2p = new VoxelData2Point(datas);
				vd2p.build(this._product);

				VoxelShadowBuild vsb = new VoxelShadowBuild ();
				vsb.build(this._product);


				VoxelMeshBuild vmb = new VoxelMeshBuild ();
				vmb.build(this._product);

				//VoxelRemoveSameVertices rsv = new VoxelRemoveSameVertices ();
				//rsv.build(this._product);

				//VoxelRemoveFace vrf = new VoxelRemoveFace ();
				//vrf.build(this._product);

				/*
				VoxelRemoveFace vrf = new VoxelRemoveFace ();
				vrf.build(this._product);
				*/

				_geometry = new VoxelGeometry();
				_geometry.draw (this._product, this.gameObject, this._material);
			}

		}


		public void clear ()
		{

			if (!empty) {

				if(_product.voxels != null){
					_product.voxels.Clear();
				}
				this.clearMesh ();
			}
		}



		public Vector3 min{
			get{
				return this._product.min;
			}
		}
		public Vector3 max{
			get{
				return this._product.max;
			}
		}

		public void showMesh ()
		{
			this._geometry._mesh.gameObject.SetActive (true);
			//refersh ();

		}
		/*private void refersh(){
			Vector3 offset = Vector3.zero;
			Vector3 size =  new Vector3 (this._product.max.x - this._product.min.x, this._product.max.z - this._product.min.z, this._product.max.y - this._product.min.y);

			Debug.Log ("offset" + offset);

			this._geometry._mesh.transform.localPosition = offset;

			if (_geometry._collider == null) {
				_geometry._collider = this.gameObject.GetComponent <BoxCollider>();
			}


			if (_geometry._collider == null) {
				_geometry._collider = this.gameObject.AddComponent <BoxCollider>();
			}
			_geometry._collider.size = size + Vector3.one;//new Vector3 (max_.x - min_.x + 1, max_.z - min_.z + 1, max_.y - min_.y + 1);
		}
	
*/
		public void setLightColor(Color color){
			Renderer renderer = this._geometry._mesh.GetComponent<Renderer> ();
			renderer.material.SetColor("_LightColor", color);
		}
		public void setMainColor(Color color){
			Renderer renderer = this._geometry._mesh.GetComponent<Renderer> ();
			renderer.material.SetColor("_MainColor", color);
		}
		public void clearMesh(){


			if (this._geometry._mesh) {
				GameObject.DestroyImmediate (this._geometry._mesh.gameObject);
			}
			this._geometry._mesh = null;

		}




	}

}