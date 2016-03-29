using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelMeshBuild : IVoxelBuilder
	{

		public void init (){

		}
		private VoxelProduct product_ = null;
		private void addVertix (Vector3 p, Color c, Vector3 normal){
			VoxelDrawData.Vertice v = new VoxelDrawData.Vertice ();
			v.position = p;
			v.normal = normal;
			v.color = c;
			v.uv1 =Vector2.zero;
			v.uv2 = Vector2.zero;
			product_.draw.vertices.Add (v);
//			Debug.Log (product_.draw.vertices[0]);

		}
		private void addRect(Vector3 direction, VectorInt3 position, Color color){

			Vector3 offset = new Vector3(position.x, position.z, position.y);

			product_.draw.triangles.Add (product_.draw.vertices.Count + 0);
			product_.draw.triangles.Add (product_.draw.vertices.Count + 1);
			product_.draw.triangles.Add (product_.draw.vertices.Count + 2);
			product_.draw.triangles.Add (product_.draw.vertices.Count + 1);
			product_.draw.triangles.Add (product_.draw.vertices.Count + 3);
			product_.draw.triangles.Add (product_.draw.vertices.Count + 2);

		

			Vector3 p0 = new Vector3 ();//
			Vector3 p1 = new Vector3 ();//
			Vector3 p2 = new Vector3 ();//
			Vector3 p3 = new Vector3 ();//
			if (direction == Vector3.up) {
				
				p0 =  (new Vector3 (-0.5f, 0.5f, 0.5f) + offset);
				p1 =  (new Vector3 (0.5f, 0.5f, 0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, 0.5f, -0.5f) + offset);
				p3 =  (new Vector3 (0.5f, 0.5f, -0.5f) + offset);

			}if (direction == Vector3.down) {
				p0 =  (new Vector3 (-0.5f, -0.5f, -0.5f) + offset);
				p1 =  (new Vector3 (0.5f, -0.5f, -0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, -0.5f, 0.5f) + offset);
				p3 = (new Vector3 (0.5f, -0.5f, 0.5f) + offset);

			}else if (direction == Vector3.back) {
				p0 =  (new Vector3 (-0.5f, 0.5f, -0.5f) + offset);
				p1 =  (new Vector3 (0.5f, 0.5f, -0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, -0.5f, -0.5f) + offset);
				p3 =  (new Vector3 (0.5f, -0.5f, -0.5f) + offset);


			}else if (direction == Vector3.forward) {
				p0 =  (new Vector3 (-0.5f, -0.5f, 0.5f) + offset);
				p1 =  (new Vector3 (0.5f, -0.5f, 0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, 0.5f, 0.5f) + offset);
				p3 =  (new Vector3 (0.5f, 0.5f, 0.5f) + offset);


			}else if (direction == Vector3.left) {
				p0 =  (new Vector3 (0.5f, -0.5f, 0.5f) + offset);
				p1 =  (new Vector3 (0.5f, -0.5f, -0.5f) + offset);
				p2 =  (new Vector3 (0.5f, 0.5f, 0.5f) + offset);
				p3 =  (new Vector3 (0.5f, 0.5f, -0.5f) + offset);
			}else if (direction == Vector3.right) {

				p0 =  (new Vector3 (-0.5f, -0.5f, -0.5f) + offset);
				p1 =  (new Vector3 (-0.5f, -0.5f, 0.5f) + offset);
				p2 =  (new Vector3 (-0.5f, 0.5f, -0.5f) + offset);
				p3 =  (new Vector3 (-0.5f, 0.5f, 0.5f) + offset);
			}
		
			this.addVertix (p0, color, direction.normalized);
			this.addVertix (p1, color, direction.normalized);
			this.addVertix (p2, color, direction.normalized);
			this.addVertix (p3, color, direction.normalized);
		}
		public Task task(VoxelProduct product){
			return new TaskPack (delegate{
				return _task(product);
			});
		}
		private Task _task(VoxelProduct product){
//			Debug.Log ("****************");

			TaskList tl = new TaskList ();
			TaskManager.PushFront (tl, delegate {
				//Debug.Log("!!!!!!");
				this.product_ = product;
				product_.draw = new VoxelDrawData ();
			});


			for (int i = 0; i < product.voxels.Count; i+=1000) {
				tl.push(buildTask (i, Mathf.Min(i + 1000, product.voxels.Count), product.voxels));
			}

			return tl;
		}
		private Task buildTask(int from, int to, Dictionary<VectorInt3, VoxelHandler> voxs){
			Task task = new Task ();
			task.init = delegate{
				build(from, to, voxs);
			};


			return task;
		}
		private void build(int from, int to, Dictionary<VectorInt3, VoxelHandler> voxs){
//			Debug.Log ("from:" + from + ", to" + to);
			List<VectorInt3> keys = new List<VectorInt3> (voxs.Keys); 
			for (int i = from; i < to; ++i) {
				VectorInt3 key = keys [i];

				VoxelHandler value = voxs [key];
				if(!voxs.ContainsKey(key + new VectorInt3(0,  -1, 0))){
					addRect (Vector3.back, key, value.color);

				}

				if(!voxs.ContainsKey(key + new VectorInt3(0, 1, 0))){
					addRect (Vector3.forward, key,  value.color);

				}

				if(!voxs.ContainsKey(key + new VectorInt3(0, 0, 1))){
					addRect (Vector3.up, key, value.color);

				}


				if(!voxs.ContainsKey(key + new VectorInt3(0, 0, -1))){
					addRect (Vector3.down, key, value.color);

				}


				if(!voxs.ContainsKey(key + new VectorInt3(1, 0, 0))){
					addRect (Vector3.left, key, value.color);

				}


				if(!voxs.ContainsKey(key + new VectorInt3(-1, 0, 0))){
					addRect (Vector3.right, key,  value.color);

				}
			}
		
		}
		public void build(VoxelProduct product){
			this.product_ = product;
			product_.draw = new VoxelDrawData ();

			for (int i = 0; i < product.voxels.Count; i+=1000) {
				build (i, Mathf.Min(i + 1000, product.voxels.Count), product.voxels);
			}
		}
	}


}