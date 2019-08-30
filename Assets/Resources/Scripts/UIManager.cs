using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject controlsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play() {
        Debug.Log("GameObject.Find(PlayerSelect).gameObject == null: " + (GameObject.Find("PlayerSelect") == null));
        GameObject.Find("PlayerSelect").gameObject.SetActive(true);
        GameObject.Find("MainMenu").gameObject.SetActive(false);
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
