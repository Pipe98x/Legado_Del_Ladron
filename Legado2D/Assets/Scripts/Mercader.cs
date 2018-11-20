using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercader : MonoBehaviour {

    [SerializeField]
    private NivelManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (manager.iniciar)
        {
            gameObject.transform.Translate(Vector3.right * 5 * Time.deltaTime);
        }
    }
}
