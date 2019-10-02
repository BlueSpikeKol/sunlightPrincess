using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : MonoBehaviour {
    public GameObject Player;
    public GameObject destination;


	// Use this for initialization


	void OnEnable () {

        Player.transform.position = destination.transform.position;
        this.enabled = false;
    }
	
	
}
