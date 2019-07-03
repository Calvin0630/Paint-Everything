using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ColourMap : MonoBehaviour {
    GameObject[] players;
    Color[] playerColors;
    //scores counts the number of vertices that each of the players have paintes. the size of scores is the number of players in the game.
    public int[] scores;
    public Image[] playerScoreBars;
    Mesh mapMesh;
    Vector3[] vertices;
    //vertColors has the same size as vertices. colors map onto verts by index.
    Color[] vertColors;
    public Color defaultColor;
    float dimX;
    float dimZ;
    Vector3 mapCenter;
    //the number of zones in the X direction
    int zoneCountX;
    //the difference in position between zones in the X direction
    float zoneDeltaX;
    //the number of zones in the Z direction
    int zoneCountZ;
    //the difference in position between zones in the Z direction
    float zoneDeltaZ;
    public float zoneSize;
    //zones is a map of positions to an array of indies of local vertices from the map object
    //the indices corelate to vertColors and vertices
    Dictionary<Vector3, int[]> zones;

    // Use this for initialization
    void Start() {
        mapMesh = gameObject.GetComponent<MeshFilter>().mesh;
        players = Camera.main.GetComponent<CameraController>().targets;
        scores = new int[players.Length];
        //set all the scores to 0
        for (int i = 0; i < scores.Length; i++) scores[i] = 0;
        playerColors = new Color[players.Length];
        for (int i = 0; i < players.Length; i++) {
            playerColors[i] = players[i].GetComponent<MeshRenderer>().materials[0].color;
        }
        vertices = mapMesh.vertices;
        vertColors = new Color[vertices.Length];
        //set the default color of the map
        for (int i = 0; i < vertColors.Length; i++) {
            vertColors[i] = defaultColor;
        }
        mapMesh.colors = vertColors;
        //get the dimensions of the map
        float maxX = Mathf.NegativeInfinity;
        float minX = Mathf.Infinity;
        float maxZ = Mathf.NegativeInfinity;
        float minZ = Mathf.Infinity;
        for (int i = 0; i < vertices.Length; i++) {
            //vertices[i] is in model space. so it must be converted to world space
            Vector3 worldSpaceVertPos = gameObject.transform.TransformPoint(vertices[i]);
            float x = worldSpaceVertPos.x;
            float z = worldSpaceVertPos.z;
            if (x > maxX) maxX = x;
            if (x < minX) minX = x;
            if (z > maxZ) maxZ = z;
            if (z < minZ) minZ = z;
        }
        Debug.Log("The bound of the map are: (" + minX + ", " + minZ + ") to (" + maxX + ", " + maxZ + ")");
        mapCenter = new Vector3((maxX + minX)/2, 0, (maxZ + minZ)/2);
        //center the mesh in world space
        gameObject.transform.parent.transform.position = -mapCenter;
        dimX = maxX - minX;
        dimZ = maxZ - minZ;
        Debug.Log("The center of the map is at: " + mapCenter);
        Debug.Log("the dimensions are " + dimX + " by " + dimZ);
        //initialize the zones
        zoneCountX = (int)(Mathf.Round(dimX) / (zoneSize/2))+1;
        zoneDeltaX = dimX / (zoneCountX - 1);
        zoneDeltaX = Mathf.Round(zoneDeltaX);
        zoneCountZ = (int)(Mathf.Round(dimZ) / (zoneSize / 2))+1;
        zoneDeltaZ = dimZ / (zoneCountZ - 1);
        zoneDeltaZ = Mathf.Round(zoneDeltaZ);
        Debug.Log("There will be " + zoneCountX + " zones in the X direction, and " + zoneCountZ + " zones in the Z direction");
        Debug.Log("the zones are spaced out by x: " + zoneDeltaX + " and z: " + zoneDeltaZ);
        //zonePositions start in -x, -z corner and move along the Z axis in rows
        Vector3[] zonePositions = new Vector3[zoneCountX * zoneCountZ];
        for (int i = 0; i < zoneCountX; i++) {
            for (int j = 0; j < zoneCountZ; j++) {
                zonePositions[(i * zoneCountX) + j] = new Vector3((i * zoneDeltaX) - (zoneDeltaX*((zoneCountX-1)/2)), 0, (j * zoneDeltaZ) - (zoneDeltaZ * ((zoneCountZ - 1) / 2)));
                Debug.Log("zonePosition[" + ((i * zoneCountZ) + j) + "]: " + zonePositions[(i * zoneCountX) + j]);
            }
        }
        //initialize zones
        zones = new Dictionary<Vector3, int[]>();
        //for each of the zone positions
        for (int i = 0; i < zonePositions.Length; i++) {
            //this list is to keep track of the indices of the verts within range of zone i
            List<int> vertIndices = new List<int>();
            //for each of the vertices
            for (int j = 0; j < vertices.Length; j++) {
                if (Mathf.Abs(gameObject.transform.TransformPoint(vertices[j]).x - zonePositions[i].x) <= zoneSize && Mathf.Abs(gameObject.transform.TransformPoint(vertices[j]).z - zonePositions[i].z) <= zoneSize) {
                    vertIndices.Add(j);
                }
            }
            //assign the zone position to the entry in the dictionary
            zones[zonePositions[i]] = vertIndices.ToArray();
            Debug.Log("zone: " + i + " size: " + vertIndices.Count);
        }
        EditorApplication.isPaused = true;
    }

    // Update is called once per frame
    void Update() {
        vertices = mapMesh.vertices;
        /*
        //the old way of coloring the map
        //im keeping it in for the sake of nostalgia
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
        */
        //for each of the players
        for (int i = 0; i < players.Length; i++) {
            //get the position of the nearest zone
            Vector3 closestZonePos = GetZonePosition(players[i].transform.position);
            //get the indices of the vertices in that zone
            int[] zoneVertIndices = zones[closestZonePos];
            for (int j=0;j<zoneVertIndices.Length;j++) {
                //the distance between player i and vert j
                float distance = (players[i].transform.position - gameObject.transform.TransformPoint(vertices[zoneVertIndices[j]])).magnitude;
                if (distance < 0.75f) {
                    //SetVertColor(zoneVertIndices[j], i);
                    for (int k=0;k<players.Length;k++) {
                        if (vertColors[zoneVertIndices[j]]==playerColors[k]) {
                            scores[k]--;
                        }
                    }
                    scores[i]++;
                    vertColors[zoneVertIndices[j]] = playerColors[i];
                }
            }
            
        }
        mapMesh.colors = vertColors;
        //update UI
        int totalScore = 0;
        for (int i = 0; i < players.Length; i++) {
            totalScore += scores[i];
        }
        for (int i = 0; i < players.Length; i++) {
            float scorePercent = (float)scores[i] / totalScore;
            playerScoreBars[i].fillAmount = scorePercent;
        }

    }
    //takes a vector and will return the nearest zone position
    Vector3 GetZonePosition(Vector3 v) {
        //check if its in the bounds of the map
        if (Mathf.Abs(v.x - gameObject.transform.position.x) > dimX / 2 || Mathf.Abs(v.z - gameObject.transform.position.z) > dimZ / 2) {
            Debug.Log("v is out of the bounds of the map");
            return Vector3.negativeInfinity;
        }
        //round v to the nearest 
        v = new Vector3(Mathf.Round(v.x / zoneDeltaX) * zoneDeltaX, 0, Mathf.Round(v.z / zoneDeltaZ) * zoneDeltaZ);
        Debug.Log("zonePos: " + v);
        return v;
    }
    /*
    //this is a function to count the scores of the players
    //vertIndex is the index of the vertex being painted
    //playerIndex is the number of the player ie player1 = 0
    void SetVertColor(int vertIndex, int playerIndex) {
        vertColors = mapMesh.colors;
        //Debug.Log(vertColors[vertIndex] == defaultColor);
        //if the vert hasnt been painted...
        if (vertColors[vertIndex]== defaultColor) {
            scores[playerIndex]++;
        }
    }
    */
}
