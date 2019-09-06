using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject playerSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadPlayerSelect() {
        playerSelect.SetActive(true);
        GameObject.Find("MainMenu").gameObject.SetActive(false);
    }
    //loads the game scene with the correct number of players and map
    public void LoadGame(Color[] playerColors, int mapID) {

    }
    public void Exit() {
        Application.Quit();
    }
    public void ControlPanelOn() {
        controlsPanel.SetActive(true);
    }

    public void ControlPanelOff() {
        controlsPanel.SetActive(false);
    }

}
