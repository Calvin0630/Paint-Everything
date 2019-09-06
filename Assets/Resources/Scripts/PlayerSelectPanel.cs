using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectPanel : MonoBehaviour
{
    public int playerID;
    bool playerActive;
    // Start is called before the first frame update
    void Start() {
        playerActive = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump "+playerID)) {
            playerActive = true;
            Debug.Log("Player " + playerID + " active");
        }
    }
}
