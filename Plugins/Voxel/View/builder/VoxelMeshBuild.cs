using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelMeshBuild : VoxelBuilder
	{
		private VoxelProduct product_ = null;
		private void addVertix (Vector3 p, Color c, Vector3 normal, Vector2 uv1, Vector2 uv2){
			VoxelDrawData.Vertice v = new VoxelDrawData.Vertice ();
			v.position = p;
			v.normal = normal;
			v.color = c;
			v.uv1 = uv1;
			v.uv2 = uv2;
			product_.draw.vertices.Add (v);
//			Debug.Log (product_.draw.vertices[0]);

		}
		private void addRect(Vector3 direction, VectorInt3 position, Vector2 shadow, Vector2 light, Color color){


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
			float magic = 0.03125f;//
		
			Vector2 uv01 = (new Vector2 (light.x +magic, light.y +magic));
			Vector2 uv11 =(new Vector2 (light.x, light.y +magic));
			Vector2 uv21 =(new Vector2 (light.x +magic, light.y+0.0625f));
			Vector2 uv31 = (new Vector2 (light.x, light.y+0.0625f));

			Vector2 uv02 =(new Vector2 (shadow.x +magic, shadow.y +magic));
			Vector2 uv12 = (new Vector2 (shadow.x, shadow.y +magic));
			Vector2 uv22 =(new Vector2 (shadow.x +magic, shadow.y+0.0625f));
			Vector2 uv32 = (new Vector2 (shadow.x, shadow.y+0.0625f));


			this.addVertix (p0, color, direction.normalized, uv01, uv02);
			this.addVertix (p1, color, direction.normalized, uv11, uv12);
			this.addVertix (p2, color, direction.normalized, uv21, uv22);
			this.addVertix (p3, color, direction.normalized, uv31, uv32);
		}
		public override void build(VoxelProduct product){
			this.product_ = product;
			product_.draw = new VoxelDrawData ();
			Dictionary<VectorInt3, VoxelHandler> voxs = product.voxels; 

			foreach (KeyValuePair<VectorInt3, VoxelHandler> kv in voxs) {
				if(!voxs.ContainsKey(kv.Key + new VectorInt3(0,  -1, 0))){
					addRect (Vector3.back, kv.Key, VoxelHandler.GetUV(kv.Value.back), VoxelHandler.GetUV(kv.Value.lback), kv.Value.color);

				
				}

				if(!voxs.ContainsKey(kv.Key + new VectorInt3(0, 1, 0))){
					addRect (Vector3.forward, kv.Key,  VoxelHandler.GetUV(kv.Value.front), VoxelHandler.GetUV(kv.Value.lfront), kv.Value.color);

				}

				if(!voxs.ContainsKey(kv.Key + new VectorInt3(0, 0, 1))){
					addRect (Vector3.up, kv.Key, VoxelHandler.GetUV(kv.Value.up), VoxelHandler.GetUV(kv.Value.lup), kv.Value.color);

				}


				if(!voxs.ContainsKey(kv.Key + new VectorInt3(0, 0, -1))){
					addRect (Vector3.down, kv.Key, VoxelHandler.GetUV(kv.Value.down), VoxelHandler.GetUV(kv.Value.ldown), kv.Value.color);

				}


				if(!voxs.ContainsKey(kv.Key + new VectorInt3(1, 0, 0))){
					addRect (Vector3.left, kv.Key, VoxelHandler.GetUV(kv.Value.left), VoxelHandler.GetUV(kv.Value.lleft), kv.Value.color);

				}


				if(!voxs.ContainsKey(kv.Key + new VectorInt3(-1, 0, 0))){
					addRect (Vector3.right, kv.Key, VoxelHandler.GetUV(kv.Value.right), VoxelHandler.GetUV(kv.Value.lright), kv.Value.color);
				
				}
			}
		}
	}


}