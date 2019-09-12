using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGround : MonoBehaviour {
    [SerializeField]
    GameObject Ground;

	// Use this for initialization
	void Start () {
        Instantiate(Ground);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
