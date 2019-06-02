using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject[] targets;
    public Vector3 offset;
    // Use this for initialization
    void Start () {
		
	}

    void Update() {
        Vector3 targetAverage = Vector3.zero;
        for (int i = 0; i < targets.Length; i++) {
            targetAverage += targets[i].transform.position;
        }
        targetAverage /= targets.Length;
        transform.position = targetAverage + offset;
        transform.LookAt(targetAverage);
    }
}
