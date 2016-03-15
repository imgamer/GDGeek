using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelRemoveSameVertices : IVoxelBuilder
	{
		private VoxelDrawData draw_ = null;
		private struct Pack
		{
			public int index;
			public VoxelDrawData.Vertice vertice;
			public Pack(int i, VoxelDrawData.Vertice v){
				index = i;
				vertice = v;
			}
		
		}

		public Task task(VoxelProduct product){
			Task task = new Task ();
			task.init = delegate {
				build(product);
			};
			return task;
		}


		public void build(VoxelProduct product){
			draw_ = product.draw;

			List<VoxelDrawData.Vertice> vertices = draw_.vertices;

			List<int> triangles = draw_.triangles;

			List<VoxelDrawData.Vertice> tVertices = new List<VoxelDrawData.Vertice>();


			Dictionary<Vector3, Pack> board = new Dictionary<Vector3, Pack>(); 
			List<Pack> temp = new List<Pack> ();

			List<int> tTriangles = new List<int>();
			Dictionary<int, int> ht = new Dictionary<int, int>(); 


			List<Pack> all = new List<Pack> ();
			for(int i =0; i<vertices.Count; ++i){
				all.Add (new Pack (i, vertices [i]));
			}

			while (all.Count != 0) {
				for (int i = 0; i < all.Count; ++i) {
					if (board.ContainsKey (all [i].vertice.position)) {
						Pack ver = board [all[i].vertice.position];

						if (ver.vertice.color != all [i].vertice.color || ver.vertice.normal != all [i].vertice.normal) {
							temp.Add (all [i]);
						} else {
							ht [all [i].index] = ht[ver.index];
						}

					} else {
						board [all [i].vertice.position] = all[i];
						tVertices.Add (all[i].vertice);
						ht [all [i].index] = tVertices.Count - 1;
					}
				}

				board.Clear ();
			
				all = temp;


				temp = new List<Pack> ();

			}

			for(int i = 0; i<triangles.Count; ++i){

				int oldIndex = triangles[i];
				int newIndex = ht[oldIndex];
				tTriangles.Add(newIndex);
			}

			product.draw.triangles = tTriangles;
			product.draw.vertices = tVertices;
		}
		public void build2(VoxelProduct product){
			draw_ = product.draw;

			List<VoxelDrawData.Vertice> vertices = draw_.vertices;

			//Debug.Log ("!@@" + vertices.Count);
			List<int> triangles = draw_.triangles;

			List<VoxelDrawData.Vertice> tVertices = new List<VoxelDrawData.Vertice>();
			List<int> tTriangles = new List<int>();
			Dictionary<int, int> ht = new Dictionary<int, int>(); 

			for(int i=0; i<vertices.Count; ++i){
				int index = tVertices.FindIndex(delegate(VoxelDrawData.Vertice v) {
					if(v.position == vertices[i].position &&
						v.color == vertices[i].color &&
					   v.normal == vertices[i].normal
					   ){
						return true;
					}
					else{ 
						return false;
					}
				});

				int newIndex = -1;
				int oldIndex = i;
				if(index == -1){

					newIndex = tVertices.Count;
					tVertices.Add(vertices[i]);

				}else{
					newIndex = index;
				}
				ht[oldIndex] = newIndex;
			}

			for(int i = 0; i<triangles.Count; ++i){
				int oldIndex = triangles[i];
				int newIndex = ht[oldIndex];
				tTriangles.Add(newIndex);
			}

		//	Debug.Log ("!@@" + tVertices.Count);
			product.draw.triangles = tTriangles;
			product.draw.vertices = tVertices;
		}
	}


}