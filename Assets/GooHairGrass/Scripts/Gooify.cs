using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gooify : MonoBehaviour {

	public bool selfStart;

	public HumanBuffer humanBuffer;

	public ComputeShader physicsShader;
	public ComputeShader gatherShader;

	public Material renderer;

	private vBuffer vBuf;
	private vBuffUpdater updater;

	private vBuff_Transform vbt;
	private vBuff_Human vbh;
	private vBuff_DataOut vdo;

	private tBuffer tBuf;
	
	private DisplayVertBufferWithTriangles  dvbwt;

	

	public void OnEnable(){
		if( selfStart == true ){ Live(); }
	}

	public void OnDisable(){
		if( selfStart == true ){ Die(); }
	}

	// Use this for initialization
	public void Live() {

		vBuf = gameObject.AddComponent<vBuffer>() as vBuffer;

		updater = gameObject.AddComponent<vBuffUpdater>() as vBuffUpdater; 
		updater.computeShader = physicsShader;
		updater.update = true;
		updater.resetOnStart = true;

		
		vbt = gameObject.AddComponent<vBuff_Transform>() as vBuff_Transform;
		tBuf = gameObject.AddComponent<tBuffer>() as tBuffer;


		dvbwt = gameObject.AddComponent<DisplayVertBufferWithTriangles>() as DisplayVertBufferWithTriangles;
		dvbwt.m = new Material(renderer);
		//dvbwt.enabled = false;

		vbh = gameObject.AddComponent<vBuff_Human>() as vBuff_Human;
		vbh.human = humanBuffer;

		vdo = gameObject.AddComponent<vBuff_DataOut>() as vBuff_DataOut;
		vdo.gatherShader = gatherShader;

		tBuf.Live();
		vBuf.Live();
		updater.Live();
		vbt.Live();
		vbh.Live();
		vdo.Live();
		dvbwt.Live();

		GetComponent<MeshRenderer>().enabled = false;
		
	}

	public void Die(){

		tBuf.Die();
		vBuf.Die();
		
		updater.Die();
		
		vbt.Die();
		vbh.Die();

		vdo.Die();

		dvbwt.Die();

		Destroy( tBuf );
		Destroy( vBuf );
		Destroy( updater );
		Destroy( vbt );
		Destroy( vbh );
		Destroy( vdo );
		Destroy( dvbwt );

		GetComponent<MeshRenderer>().enabled = true;




	}


}
