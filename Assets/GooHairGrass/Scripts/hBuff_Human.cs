using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBuff_Human : HairBufferUpdater {

	public HumanBuffer human;

	public override void BeforeCollisionDispatch(ComputeShader computeShader , int _kernel){

		computeShader.SetInt( "_HumanLength" , human.numberHumans );
		computeShader.SetBuffer( _kernel , "humanBuffer" , human._buffer );

	}
	

}
