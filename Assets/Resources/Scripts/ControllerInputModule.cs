using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputModule : UnityEngine.EventSystems.StandaloneInputModule{
    protected override void Start() {
        base.Start();
    }
    //variable to hold current settings
    public bool isMouseInputActive = false;
    //type interface to get actual mouse input status
    public bool GetMouseState {
        get { return isMouseInputActive; }
    }
    //mouse switcher interface 
    public void MouseSwitcher() {
        isMouseInputActive = isMouseInputActive == false ? true : false;
    }
    //inherited event processing interface (called every frame)
    public override void Process() {
        bool usedEvent = SendUpdateEventToSelectedObject();

        if (eventSystem.sendNavigationEvents) {
            if (!usedEvent)
                usedEvent |= SendMoveEventToSelectedObject();
            if (!usedEvent)
                SendSubmitEventToSelectedObject();
        }
        if (isMouseInputActive)
            ProcessMouseEvent();
    }
}
