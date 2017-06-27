using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grassify : MonoBehaviour {


	public int totalHairs;
	public float hairLength;
	public int vertsPerHair;

	public int tubeWidth;
	public int tubeLength;

	public HumanBuffer humanBuffer;


	public ComputeShader basePhysicsShader;
	public ComputeShader hairCollisionShader;
	public ComputeShader hairConstraintShader;
	public ComputeShader hairGatherShader;


	public Material baseLineRenderer;
	public Material baseRenderer;
	public Material hairLineRenderer;
	public Material tubeRenderer;

	private Hairify hairify;
	private RenderTube renderTube;

	// Use this for initialization
	void OnEnable() {

		hairify = gameObject.AddComponent<Hairify>() as Hairify;

		hairify.totalHairs = totalHairs;
		hairify.hairLength = hairLength;
		hairify.vertsPerHair = vertsPerHair;
		hairify.basePhysicsShader = basePhysicsShader;
		hairify.hairCollisionShader = hairCollisionShader;
		hairify.hairConstraintShader = hairConstraintShader;
		hairify.hairGatherShader = hairGatherShader;
		hairify.baseLineRenderer = baseLineRenderer;
		hairify.baseRenderer = baseRenderer;
		hairify.hairLineRenderer = hairLineRenderer;

		hairify.humanBuffer = humanBuffer;

		hairify.selfStart = false;

		hairify.Live();


		renderTube = gameObject.AddComponent<RenderTube>() as RenderTube;
		renderTube.tubeWidth = tubeWidth;
		renderTube.tubeLength = tubeLength;
		renderTube.m = new Material( tubeRenderer );

		renderTube.Live();

		
	}

	void OnDisable(){
		hairify.Die();
		renderTube.Die();
	}

}
