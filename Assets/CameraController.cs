using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject[] targets;
    public Vector3 offset;
    public float cameraDist;
    Vector3 targetAverage;
    //this float is the minimum distance the camera must be from the average position of the players to get all of them is the scene.
    float minCamDist;
    // Use this for initialization
    void Start () {
        offset = offset.normalized;
	}

    void Update() {
        
        CalculateAveragePosition();
        //the pos of the player that is farthest from the average position
        Vector3 farthestPlayerPos = Vector3.zero;
        float farthestPlayerDist = 0;
        int farthestPlayerIndex = -1;
        for (int i = 0; i < targets.Length; i++) {
            //Debug.Log("Player" + (i + 1) + " is " + (targets[i].transform.position - targetAverage).magnitude + " from the average.");
            if ((targets[i].transform.position - targetAverage).magnitude > farthestPlayerDist) {
                farthestPlayerDist = (targets[i].transform.position - targetAverage).magnitude;
                farthestPlayerPos = targets[i].transform.position;
                farthestPlayerIndex = i;
            }
        }
        //Debug.Log("Player: " + (farthestPlayerIndex + 1) + " is the farthest from the average");
        cameraDist = Mathf.Max(10, farthestPlayerDist * 2);

        transform.position = targetAverage + offset*cameraDist;
        transform.LookAt(targetAverage);
    }
    //looks at the positions of the targets (players) and calculates the targetAverage variable
    void CalculateAveragePosition() {
        targetAverage = Vector3.zero;
        
        for (int i = 0; i < targets.Length; i++) {
            targetAverage += targets[i].transform.position;
        }
        targetAverage /= targets.Length;
    }

    private void OnDrawGizmos() {
        CalculateAveragePosition();
        Gizmos.DrawSphere(targetAverage, 0.5f);
    }
}
