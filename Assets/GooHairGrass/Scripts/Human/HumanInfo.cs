
using UnityEngine;
using System.Collections;

public class HumanInfo : MonoBehaviour {

	public GameObject Head;
	public GameObject Hand1;
	public GameObject Hand2;

	public Structs.Human human;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		human.head = Head.GetComponent<HeadInfo>().head;
		human.hand1 = Hand1.GetComponent<HandInfo>().hand;
		human.hand2 = Hand2.GetComponent<HandInfo>().hand;
	
	}
}