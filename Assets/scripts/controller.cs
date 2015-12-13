using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class controller : MonoBehaviour {

    public GameObject gcamera;
    public GameObject mapPart;
    private Rigidbody rigidBody;
    private bool hitLevel = false;
    private float ballSize = 1;
    private static float grow = .1f;

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
        Debug.logger.Log(ballSize + " " + transform.localScale.x + " " + (transform.localScale.x == ballSize));
        if (!Mathf.Approximately(transform.localScale.x, ballSize))
        {
            if (transform.localScale.x < ballSize)
            {
                rigidBody.mass += grow;
                transform.localScale += (new Vector3(grow, grow, grow));
            }
            else if (transform.localScale.x > ballSize && transform.localScale.x > (1 + grow))
            {
                rigidBody.mass -= grow;
                transform.localScale -= (new Vector3(grow, grow, grow));
            }
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
            if(col.transform.localScale.x > transform.localScale.x)
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex);

            Destroy(col.gameObject);
            ballSize += col.transform.localScale.x;
        }
        else if(col.tag == "Ball_shrink")
        {
            Destroy(col.gameObject);
            ballSize -= col.transform.localScale.x;
            if (ballSize < 1)
                ballSize = 1;
        }
    }
}
