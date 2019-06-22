using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float ballSpeed;
    public float boost;
    //an int that represents the player number [1-4]
    public int playerID;
    Rigidbody body;
	// Use this for initialization
	void Start () {
		if (ballSpeed==0) Debug.Log("ballSpeed is 0!!!!");
        if (playerID < 1 || playerID > 4) Debug.Log("invalid playerID. got: " + playerID + ". expected a value from 1-4");
        body = GetComponent<Rigidbody>();
        boost = 1f;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(isOnGround());
        if (isOnGround()) {
            float xSpeed = Input.GetAxis("Horizontal "+playerID);
            float ySpeed = Input.GetAxis("Vertical " +playerID);
            //check if player is pressing the boost button, if it is, add boost, otherwise no boost
            if (Input.GetButton("Boost " + playerID)) {
                Debug.Log("Boosting");
                boost = 1.5f;
            }
            else {
                boost = 1f;
            }
            //Debug.Log(boost);
            //body.AddTorque(new Vector3(xSpeed, 0, ySpeed) * ballSpeed * Time.deltaTime);
            body.AddForce(new Vector3(xSpeed, 0, ySpeed) * ballSpeed * boost);
            //check if player is pressing the jump button
            if (Input.GetButtonDown("Jump " + playerID)) {
                jump();
            }
        }
        
    }

    bool isOnGround() {
        bool result = false;
        Collider[] touchingObjects = Physics.OverlapSphere(transform.position, 0.6f);
        for (int i=0;i<touchingObjects.Length;i++) {
            if (touchingObjects[i].CompareTag("Ground")) {
                result = true;
                break;
            }
        }
        return result;
    }

    //Player jumping mechanic. Has to be on the ground to be ran from update
    void jump() {
        float ySpeed = 350f;
        body.AddForce(new Vector3(0, ySpeed, 0));
    }
}
