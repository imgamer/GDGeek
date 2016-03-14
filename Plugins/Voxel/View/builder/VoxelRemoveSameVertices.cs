using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GDGeek
{
	public class VoxelRemoveSameVertices : VoxelBuilder
	{
		private VoxelDrawData draw_ = null;
		public override void build(VoxelProduct product){
			draw_ = product.draw;
			List<VoxelDrawData.Vertice> vertices = draw_.vertices;
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
			product.draw.triangles = tTriangles;
			product.draw.vertices = tVertices;
		}
	}


}