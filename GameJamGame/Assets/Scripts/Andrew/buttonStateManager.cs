using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonStateManager : MonoBehaviour
{
    public enum buttonState { IDLE, HOVER, PRESS, HOLD, RELEASE };
    public buttonState state = buttonState.IDLE;

    // Update is called once per frame
    void Update ()
    {
		switch(state)
        {
            case buttonState.IDLE:
                SendMessage("onIdle");
                break;
            case buttonState.HOVER:
                SendMessage("onHover");
                break;
            case buttonState.PRESS:
                SendMessage("onPress");
                break;
            case buttonState.HOLD:
                SendMessage("onHold");
                break;
            case buttonState.RELEASE:
                SendMessage("onRelease");
                break;
        }
        state = buttonState.IDLE;
	}
}
