using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {

    [SerializeField]
    private GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(player.gameObject.transform.position.x > -3)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x + 3, transform.position.y, transform.position.z);
        }
	}
}
