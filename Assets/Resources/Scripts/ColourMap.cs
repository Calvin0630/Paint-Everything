using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourMap : MonoBehaviour {
    GameObject[] players;
    Color[] playerColors;
    Mesh mapMesh;
    Vector3[] vertices;
    //vertColors has the same size as vertices. colors map onto verts by index.
    Color[] vertColors;
    // Use this for initialization
    void Start () {
        mapMesh = gameObject.GetComponent<MeshFilter>().mesh;
        players = Camera.main.GetComponent<CameraController>().targets;
        playerColors = new Color[players.Length];
        for (int i=0;i<players.Length;i++) {
            playerColors[i] = players[i].GetComponent<MeshRenderer>().materials[0].color;
        }
        vertices = mapMesh.vertices;
        vertColors = new Color[vertices.Length];
        //set the default color of the map
        for (int i=0;i<vertColors.Length;i++) {
            vertColors[i] = Color.white;
        }
        mapMesh.colors = vertColors;
        
        float max = 0;
        float min = 1024;
        for (int i = 0; i < vertices.Length; i++) {
            //vertices[i] is in model space. so it must be converted to world space
            Vector3 worldSpaceVertPos = gameObject.transform.TransformPoint(vertices[i]);
            float mag = (players[0].transform.position - worldSpaceVertPos).magnitude;
            //float mag = worldSpaceVertPos.x;
            if (mag > max) max = mag;
            if (mag < min) min = mag;
        }
        Debug.Log("Max: " + max);
        Debug.Log("Min: " + min);

    }

    // Update is called once per frame
    void Update () {
        vertices = mapMesh.vertices;
        //for each of vertices in the mesh
        for (int i=0;i<vertices.Length;i++) {
            //check if any of the players are within a distance
            for (int j=0;j<players.Length;j++) {
                //the distance between vertice i, and player j
                float distance = (players[j].transform.position - gameObject.transform.TransformPoint(vertices[i])).magnitude;
                if (distance<0.75f) {
                    vertColors[i] = playerColors[j];
                }
            }
        }
        mapMesh.colors = vertColors;

    }
}
