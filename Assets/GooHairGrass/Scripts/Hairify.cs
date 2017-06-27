using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hairify : MonoBehaviour {

	public bool selfStart;

	public int totalHairs;
	public float hairLength;
	public int vertsPerHair;

	public HumanBuffer humanBuffer;

	public ComputeShader basePhysicsShader;
	public ComputeShader hairCollisionShader;
	public ComputeShader hairConstraintShader;
	public ComputeShader hairGatherShader;


	public Material baseLineRenderer;
	public Material baseRenderer;
	public Material hairLineRenderer;


	private vBuffer vBuf;
	private vBuffUpdater baseUpdater;

	private vBuff_Transform vbt;
	private tBuffer tBuf;
	private hBuffer hBuf;

	private hBuffUpdater hairUpdater;
	private hBuff_Transform hbt;
	private hBuff_DataOut hdo;

	private DisplayHairBufferWithLines dhbwl;
	private DisplayVertBufferWithLines dvbwl;
	private DisplayVertBufferWithTriangles  dvbwt;

	private vBuff_Human vbh;
	private hBuff_Human hbh;

	public void OnEnable(){
		if( selfStart == true ){ Live(); }
	}

	public void OnDisable(){
		if( selfStart == true ){ Die(); }
	}

	// Use this for initialization
	public void Live() {

		vBuf = gameObject.AddComponent<vBuffer>() as vBuffer;

		baseUpdater = gameObject.AddComponent<vBuffUpdater>() as vBuffUpdater; 
		baseUpdater.computeShader = basePhysicsShader;
		baseUpdater.update = true;

		
		vbt = gameObject.AddComponent<vBuff_Transform>() as vBuff_Transform;

		tBuf = gameObject.AddComponent<tBuffer>() as tBuffer;

		hBuf = gameObject.AddComponent<hBuffer>() as hBuffer;

		hBuf.tBuf = tBuf;
		hBuf.vBuf = vBuf;

		hBuf.numVertsPerHair = vertsPerHair;
		hBuf.hairLength = hairLength;
		hBuf.totalHairs = totalHairs;


//		print("HAHWS");

		hairUpdater = gameObject.AddComponent<hBuffUpdater>() as hBuffUpdater;

		hairUpdater.collisionShader = hairCollisionShader;
		hairUpdater.constraintShader = hairConstraintShader;
		hairUpdater.update = true;

		

		hbt = gameObject.AddComponent<hBuff_Transform>() as hBuff_Transform;
		hdo = gameObject.AddComponent<hBuff_DataOut>() as hBuff_DataOut;
		hdo.gatherShader = hairGatherShader;
		

		dhbwl = gameObject.AddComponent<DisplayHairBufferWithLines>() as DisplayHairBufferWithLines;
		dhbwl.m = new Material(hairLineRenderer);


		dvbwl = gameObject.AddComponent<DisplayVertBufferWithLines>() as DisplayVertBufferWithLines;
		dvbwl.m = new Material(baseLineRenderer);
		dvbwl.enabled = false;
		

		dvbwt = gameObject.AddComponent<DisplayVertBufferWithTriangles>() as DisplayVertBufferWithTriangles;
		dvbwt.m = new Material(baseRenderer);
		//dvbwt.enabled = false;

		hbh = gameObject.AddComponent<hBuff_Human>() as hBuff_Human;
		vbh = gameObject.AddComponent<vBuff_Human>() as vBuff_Human;

		hbh.human = humanBuffer;
		vbh.human = humanBuffer;

		tBuf.Live();
		vBuf.Live();
		hBuf.Live();

		baseUpdater.Live();
		hairUpdater.Live();

		vbt.Live();
		hbt.Live();

		vbh.Live();
		hbh.Live();

		hdo.Live();

		dhbwl.Live();
		dvbwl.Live();
		dvbwt.Live();

		GetComponent<MeshRenderer>().enabled = false;
		
	}

	public void Die(){
GetComponent<MeshRenderer>().enabled = true;
		tBuf.Die();
		vBuf.Die();
		hBuf.Die();

		baseUpdater.Die();
		hairUpdater.Die();

		vbt.Die();
		hbt.Die();

		hdo.Die();

		vbh.Die();
		hbh.Die();

		dhbwl.Die();
		dvbwl.Die();
		dvbwt.Die();

		Destroy( tBuf );
		Destroy( vBuf );
		Destroy( hBuf );

		Destroy( baseUpdater );
		Destroy( hairUpdater );

		Destroy( vbt );
		Destroy( hbt );

		Destroy( vbh );
		Destroy( hbh );

		Destroy( dhbwl );
		Destroy( dvbwl );


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
