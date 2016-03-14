using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelGeometry
	{
		public MeshFilter _mesh = null;

		public BoxCollider _collider = null;

		private Mesh createMesh(VoxelProduct product)
		{
			Mesh m = new Mesh();
			m.name = "ScriptedMesh";


//			Debug.Log (product.draw.vertex [0].position);
			//			Debug.Log (vertices[0]);
			product.draw.refresh();
			m.vertices =  product.draw.postions;
			m.colors =  product.draw.colors;
			m.uv =  product.draw.uv1s;
			m.uv2 =  product.draw.uv2s;

			m.triangles =  product.draw.triangles.ToArray ();
			m.RecalculateNormals();

			return m;
		}
		public MeshFilter crateMeshFilter(VoxelProduct product, string name, Material material){
			GameObject go = new GameObject(name);
			MeshFilter meshFilter = go.AddComponent<MeshFilter>();
			meshFilter.mesh = createMesh(product);
			MeshRenderer renderer = go.AddComponent<MeshRenderer>();
			renderer.material = material;


			return meshFilter;
		}

		public void draw(VoxelProduct product, GameObject gameObject, Material material){

			this._mesh = this.crateMeshFilter (product, "VoxelMesh", material);

			//_mesh.gameObject.transform.SetParent (transform);		

			this._mesh.gameObject.transform.SetParent (gameObject.transform);	
			this._mesh.gameObject.transform.localPosition = Vector3.zero;
			this._mesh.gameObject.transform.localScale = Vector3.one;
			this._mesh.gameObject.transform.localRotation = Quaternion.Euler (Vector3.zero);

			this._mesh.gameObject.SetActive (true);

			Renderer renderer = this._mesh.GetComponent<Renderer> ();
			renderer.material = material;
			refresh (product, gameObject);





		}
		public void refresh(VoxelProduct product, GameObject gameObject){
			Vector3 offset = Vector3.zero;
			Vector3 size =  new Vector3 (product.max.x - product.min.x, product.max.z - product.min.z, product.max.y - product.min.y);
			offset = size / -2.0f -new Vector3 ( product.min.x, product.min.z,  product.min.y);
//			Debug.Log ("offset" + product.max);

			this._mesh.transform.localPosition = offset;

			if (_collider == null) {
				_collider = gameObject.GetComponent <BoxCollider>();
			}

			if (_collider == null) {
				_collider = gameObject.AddComponent <BoxCollider>();
			}
			_collider.size = size + Vector3.one;

		}

	}


}