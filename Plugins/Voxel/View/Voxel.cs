using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class Voxel_ : MonoBehaviour {

		public VoxelProperty property {
			get{
				VoxelProperty prop = new VoxelProperty();
				prop.color = this.color;
				prop.scale = this.transform.localScale;
				prop.position = this.transform.position;
				return prop;
			}
			set{
				VoxelProperty prop = value;
				this.color = prop.color;
				this.transform.position = prop.position;
				this.transform.localScale = prop.scale;
			}
		}

		public int _id = 0;
		public Color _color;
		public Color color {
			get{
				return _color;
			}
			set{
				
				this._color = value;
				if(Application.isPlaying)
				{
					this.GetComponent<Renderer>().material.color = this._color;
				}
			}
		}
		public void Awake(){
			this.GetComponent<Renderer>().material.color = this._color;
		}


		public void setup (VectorInt3 position, Color color, int id)
		{
			this.gameObject.SetActive (true);
			this.transform.localPosition = new Vector3(position.x, position.y, -position.z);
			this.color = color;
			this._id = id;
		}



	}
}