﻿using UnityEngine;
using System.Collections;
using System;

public class controller : MonoBehaviour {

    public GameObject gcamera;
    private Rigidbody rb;
    private bool hitLevel = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float value = 0f;
            value = Input.GetAxis("Horizontal");
        if (!hitLevel)
            value *= .4f;
        var force = new Vector3((value *.5f), 0, 0);
        rb.AddForce(force, ForceMode.Impulse);
        gcamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.name == "level")
            hitLevel = true;
    }
    void OnCollisionExit(Collision col)
    {
        if (col.collider.name == "level")
            hitLevel = false;
    }
}
