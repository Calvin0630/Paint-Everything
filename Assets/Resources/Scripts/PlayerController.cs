using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float ballSpeed;
    //an int that represents the player number [1-4]
    public int playerID;
    Rigidbody body;
	// Use this for initialization
	void Start () {
		if (ballSpeed==0) Debug.Log("ballSpeed is 0!!!!");
        if (playerID < 1 || playerID > 4) Debug.Log("invalid playerID. got: " + playerID + ". expected a value from 1-4");
        body = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD:Assets/Resources/Scripts/PlayerController.cs
        //Debug.Log(isOnGround());
=======
>>>>>>> origin:Assets/PlayerController.cs
        if (isOnGround()) {
            float xSpeed = Input.GetAxis("Horizontal "+playerID);
            float ySpeed = Input.GetAxis("Vertical " +playerID);
            //body.AddTorque(new Vector3(xSpeed, 0, ySpeed) * ballSpeed * Time.deltaTime);
            body.AddForce(new Vector3(xSpeed, 0, ySpeed) * ballSpeed);
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
}
