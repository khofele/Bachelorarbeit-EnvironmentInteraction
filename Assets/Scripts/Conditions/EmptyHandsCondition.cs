using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyHandsCondition : Condition
{
    [SerializeField] private Transform rightHand = null;
    [SerializeField] private Transform leftHand = null;

    private bool isHandsEmpty = false;

    private void Update()
    {
        if (rightHand.childCount == 0 && leftHand.childCount == 0)
        {
            isHandsEmpty = true;
        }
        else
        {
            isHandsEmpty = false;
        }
    }

    public override bool CheckCondition()
    {
        return isHandsEmpty ;
    }
}
