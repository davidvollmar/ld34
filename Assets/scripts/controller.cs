using UnityEngine;
using System.Collections;
using System;

public class controller : MonoBehaviour {

    public GameObject gcamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var value = Input.GetAxis("Horizontal");
        Debug.logger.Log("game", "horizontal" + value);
        Vector3 trans = new Vector3(value * 0.2f * (float)Math.PI, 0, 0);
        transform.Translate(trans);
        transform.Rotate(0, 0, value * -5);
        gcamera.transform.Translate(trans);
    }
}
