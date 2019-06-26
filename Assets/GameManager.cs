using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //hom long the round will be in seconds
    public int roundDuration;
    public Text clock;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(UpdateClock());
    }

    // Update is called once per frame
    void Update() {
        
    }
    IEnumerator UpdateClock() {
        while (roundDuration>0) {
            //formating the time in second into a digital clock string
            int minutesInt = roundDuration / 60;
            int secondsInt = roundDuration - 60 * minutesInt;
            string minutesString = "";
            if (minutesInt < 10) minutesString = "0" + minutesInt;
            else minutesString = ""+minutesInt;
            string secondsString = "";
            if (secondsInt < 10) secondsString = "0" + secondsInt;
            else secondsString = "" + secondsInt;
            clock.text = minutesString + ":" + secondsString;
            roundDuration--;
            yield return new WaitForSeconds(1);
        }
        clock.text = "GAMEOVER";
        UpdateClock();
    }
}
