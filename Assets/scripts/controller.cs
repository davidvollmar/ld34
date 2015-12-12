using UnityEngine;
using System.Collections;
using System;

public class controller : MonoBehaviour {

    public GameObject gcamera;
    public GameObject mapPart;
    private Rigidbody rigidBody;
    private bool hitLevel = false;
    private int ballSize = 1;

	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        WorldGen();
	}

    void WorldGen()
    {
        var random = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            var gameobj = Instantiate(mapPart, new Vector3(i * 2.0F + 60, 2f + (i * 0.2f), 0.5f), Quaternion.identity);
            ((GameObject)gameobj).SetActive(true);
        }
    }

    void Update () {         
        float xVel = Input.GetAxis("Horizontal");
        if (!hitLevel)
            xVel *= .4f;
        var force = new Vector3((xVel *.5f), 0, 0);

        rigidBody.AddForce(force, ForceMode.Impulse);
        
        gcamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (transform.localScale.x * 5));

        if(transform.localScale.x < ballSize)
        {
            rigidBody.mass += .01f;
            transform.localScale += (new Vector3(.01f, .01f, .01f));
        }
        else if (transform.localScale.x > ballSize && transform.localScale.x > 1.01f)
        {
            rigidBody.mass -= .01f;
            transform.localScale -= (new Vector3(.01f, .01f, .01f));
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
            ballSize++;
        }
        else if(col.tag == "Ball_shrink")
        {
            Destroy(col.gameObject);
            ballSize -= 2;
            if (ballSize < 1)
                ballSize = 1;
        }
    }
}
