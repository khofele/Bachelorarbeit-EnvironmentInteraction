using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyCondition : Condition
{
    [SerializeField] private KeyCode keyCode = KeyCode.None;
    private bool isButtonPressed = false;

    private void Update()
    {
        // TODO Tastenbelegung anpassen
        if(Input.GetKeyDown(keyCode))
        {
            isButtonPressed = true;
        }
        else
        {
            isButtonPressed = false;
        }
    }

    public override bool CheckCondition()
    {
        return isButtonPressed;
    }

    public override void ResetCondition()
    {
        isButtonPressed = false;
    }
}
