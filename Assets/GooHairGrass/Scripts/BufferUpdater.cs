using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferUpdater : MonoBehaviour {

	public vBuffUpdater updater;

			
	// Use this for initialization
	public void Live() {
		if( updater == null){
			updater = GetComponent<vBuffUpdater>();
		}

	 	ExtraEnable();

		updater.OnBeforeDispatch += BeforeUpdate;
		updater.OnAfterDispatch += AfterUpdate;
	}

	public void Die(){
		updater.OnBeforeDispatch -= BeforeUpdate;
		updater.OnAfterDispatch -= AfterUpdate;
	}


	public virtual void ExtraEnable(){}
	public virtual void BeforeUpdate(ComputeShader computeShader , int _kernel){}
	public virtual void AfterUpdate(ComputeShader computeShader , int _kernel){}
	


}
