using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vBuff_Human : BufferUpdater {

	public HumanBuffer human;

	public override void BeforeUpdate(ComputeShader computeShader , int _kernel){

		computeShader.SetInt( "_HumanLength" , human.numberHumans );
		computeShader.SetBuffer( _kernel , "humanBuffer" ,human._buffer );

	}
	

}