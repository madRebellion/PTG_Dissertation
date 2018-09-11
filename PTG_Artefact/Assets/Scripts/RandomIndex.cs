using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIndex : MonoBehaviour {

    public int index;

	// Use this for initialization
	void Start () {

        index = Random.Range(0, 4);
		
	}
	
}
