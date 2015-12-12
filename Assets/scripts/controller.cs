using UnityEngine;
using System.Collections;
using System;

public class controller : MonoBehaviour {

    public GameObject gcamera;
    private Rigidbody rb;
    private bool hitLevel = false;
    private int counter = 1;

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
        gcamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (transform.localScale.x * 5));

        if(transform.localScale.x < counter)
        {
            rb.mass += .01f;
            transform.localScale += (new Vector3(.01f, .01f, .01f));
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Level")
            hitLevel = true;
    }
    void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "Level")
            hitLevel = false;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ball")
        {
            Destroy(col.gameObject);
            counter++;
        }
    }
}
