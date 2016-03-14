using UnityEngine;
using UnityEditor;
using GDGeek;
using System.IO;
using System.Collections.Generic;


namespace GDGeek{
	public class JoinVoxel
	{
		public struct Packed
		{
			public VoxelStruct vs;
			public VectorInt3 offset;

		}
		private Dictionary<VectorInt3, VoxelData> dictionary_ = new Dictionary<VectorInt3, VoxelData>();
		private List<Packed> list_ = new List<Packed>();

		public void addVoxel(VoxelStruct vs, VectorInt3 offset){
//			Debug.Log (offset.x +","+ offset.y+","+ offset.z);
			Packed packed = new Packed ();
			packed.vs = vs;
			packed.offset = offset;
			list_.Add (packed);
		}
		public void clear(){
			dictionary_.Clear ();
		}
		public void readIt(Packed packed){
			for (int i = 0; i < packed.vs.datas.Count; ++i) {
				dictionary_ [packed.vs.datas [i].pos +packed.offset ] = packed.vs.datas [i];

			}


		}
		public List<VoxelData> getDatas(){
			List<VoxelData> datas = new List<VoxelData>();
			int i = 0;
			foreach(KeyValuePair<VectorInt3, VoxelData> item in dictionary_){
				VoxelData data = new VoxelData ();
				data.color = item.Value.color;
				data.pos.x = item.Key.x;
				data.pos.y = item.Key.y;
				data.pos.z = item.Key.z;

				data.id = i;
				datas.Add (data);
				++i;
			}
			return datas;
		}
		/*
		public VectorInt4[] getPalette(){
			int size = Mathf.Max (palette_.Count, 256);
			VectorInt4[] palette = new VectorInt4[size];
			int i = 0;
			foreach (Color c in palette_)
			{
				palette [i] = VoxelFormater.Color2Bytes (c);
				++i;
			}
			return palette;
		}
*/
		//public 
		public VoxelStruct doIt(){

			this.clear ();
			for (int i = 0; i < list_.Count; ++i) {
				Packed p = this.list_ [i];
				this.readIt(p);
			}

			VoxelStruct vs = new VoxelStruct();
			/*
			vs.main = new VoxelStruct.Main ();
			vs.main.name = "MAIN";
			vs.main.size = 0;


			vs.size = new VoxelStruct.Size ();
			vs.size.name = "SIZE";
			vs.size.size = 12;
			vs.size.chunks = 0;

			vs.size.box = new VectorInt3 ();


			vs.size.box.x = this.max_.x - this.min_.x +1;
			vs.size.box.y = this.max_.y - this.min_.y +1;
			vs.size.box.z = this.max_.z - this.min_.z +1;


			vs.rgba = new VoxelStruct.Rgba ();//list_ [0].vs.rgba;
			vs.rgba.palette = this.getPalette ();

			vs.rgba.size = vs.rgba.palette.Length * 4;
			vs.rgba.name = "RGBA";
			vs.rgba.chunks = 0;

			/**/
			vs.datas = this.getDatas ();
			vs.arrange (true);
			/*
			Debug.Log (vs.datas.Count);
			vs.version = 150;


			vs.main.chunks = 52 + vs.rgba.palette.Length *4 + vs.datas.Count *4;
			Debug.Log (vs.main.chunks);
			*/
			return vs;

		}
	}
}

