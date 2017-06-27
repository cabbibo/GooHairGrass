using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HumanBuffer : MonoBehaviour {

	public GameObject player;

   
    public ComputeBuffer _buffer;
    private float[] inValues;
    public int numberHumans = 1;

    // Use this for initialization
    void Start() {
        RebuildHumans();
    }


    void RebuildHumans() {

        inValues = new float[1 * Structs.HumanStructSize];

        // Rebuild buffers
        createBuffers();
    }

    private void createBuffers() {

        if (_buffer != null)
            _buffer.Release();

        _buffer = new ComputeBuffer(1, Structs.HumanStructSize * sizeof(float));
    }


    void FixedUpdate()
    {
    	int index = 0;
        Structs.AssignHumanStruct(inValues, index, out index, player.GetComponent<HumanInfo>().human);
        _buffer.SetData(inValues);
    }


}