using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectPanel : MonoBehaviour
{
    public GameObject[] playerPanels;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0;i<playerPanels.Length;i++) {
            if (Input.GetButtonDown("Jump " + (i+1))) {
                Debug.Log((i + 1)+" panel");
                playerPanels[i].transform.Find("SelectPlayerDetails").gameObject.SetActive(true);
            }
        }
    }
}
