using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class HandInfo : MonoBehaviour{

  public Structs.Hand hand;

	public Vector3 debug;

	public Vector3 velocity;
	public float trigger;



  SteamVR_TrackedObject trackedObj;

  void Awake(){
    trackedObj = GetComponent<SteamVR_TrackedObject>();

  }

  void FixedUpdate(){

    var device = SteamVR_Controller.Input((int)trackedObj.index);

    var axis = device.GetState().rAxis1;

    hand.localToWorld = transform.localToWorldMatrix;
	  hand.worldToLocal = transform.worldToLocalMatrix;
	  hand.vel = device.velocity;
	  hand.pos = transform.position;
	  hand.trigger = axis.x;
	  hand.debug = debug;

  }


}