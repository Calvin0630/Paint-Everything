using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourMap : MonoBehaviour {
    public GameObject map;
    Mesh mapMesh;
	// Use this for initialization
	void Start () {
        mapMesh = map.GetComponentInChildren<MeshFilter>().mesh;
        
        Vector3[] vertices = mapMesh.vertices;
        /*
        // create new colors array where the colors will be created.
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
            colors[i] = Color.red;

        // assign the array of colors to the mapMesh.
        mapMesh.colors = colors;
        */ 
        float max = 0;
        float min = 1024;
        for (int i = 0; i < vertices.Length; i++) {
            //vertices[i] is in model space. so it must be converted to world space
            Vector3 worldSpaceVertPos = map.transform.TransformPoint(vertices[i]);
            float mag = (gameObject.transform.position - worldSpaceVertPos).magnitude;
            if (mag > max) max = mag;
            if (mag < min) min = mag;
        }
        Debug.Log("Max: " + max);
        Debug.Log("Min: " + min);

    }

    // Update is called once per frame
    void Update () {
        Vector3[] vertices = mapMesh.vertices;

        // create new colors array where the colors will be created.
        Color[] colors = mapMesh.colors;
        float maxDist = 0;
        float minDist = 1024;
        Debug.Log("colors.Length: "+ colors.Length);
        for (int i=0;i<colors.Length;i++) {
            Vector3 worldSpaceVertPos = map.transform.TransformPoint(vertices[i]);
            float distanceFromPlayer = (worldSpaceVertPos - gameObject.transform.position).magnitude;
            colors[i] = Color.red;
            if (distanceFromPlayer<10) {
                colors[i] = Color.red;
                Debug.Log("new color");
            }
        }
        //Debug.Log("Max: " + maxDist);
        //Debug.Log("Min: " + minDist);
        mapMesh.colors = colors;

    }
}
