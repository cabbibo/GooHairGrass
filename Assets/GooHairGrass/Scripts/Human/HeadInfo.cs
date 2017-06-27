
using UnityEngine;
using System.Collections;

public class HeadInfo : MonoBehaviour {

	public Structs.Head head;

	public Vector3 debug;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		head.localToWorld = transform.localToWorldMatrix;
		head.worldToLocal = transform.worldToLocalMatrix;
		head.debug = debug;

	}


}