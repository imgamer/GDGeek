using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;

public class VoxelResource : MonoBehaviour {

	public List<VoxelGeometry> _list = new List<VoxelGeometry>();

	public void add(VoxelGeometry geometry){
		_list.Add (geometry);
	}
	public void show(int n){
		for (int i = 0; i < _list.Count; ++i) {
			_list [i]._mesh.gameObject.SetActive (n == i);
		}
	}

}
