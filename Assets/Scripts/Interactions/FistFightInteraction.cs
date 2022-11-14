using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hands
{
    LEFT, RIGHT, NULL
}

public class FistFightInteraction : Interaction
{
    private Hands lastHand = Hands.NULL;
    private Hands currentHand = Hands.NULL;

    public Hands CurrentHand { get => currentHand; }

    protected override void SetMatchingInteractable()
    {
        matchingInteractable = typeof(Enemy);
    }

    protected override void ExecuteInteraction()
    {
        if (isInteractionRunning == false)
        {
            Punch();
        }

        base.ExecuteInteraction();
    }

    protected override void ResetInteraction()
    {
        StartCoroutine(WaitAndReset());
    }

    protected override void Update()
    {
        if (CheckTrigger() == true)
        {
            if (CheckMatchingInteractable() == true)
            {
                if (CheckOtherInteractionsRunning() == true)
                {
                    if (CheckConditions() == true)
                    {
                        ExecuteInteraction();
                        ResetInteraction();
                    }
                }
            }
        }
    }

    private void SetLastHand()
    {
        lastHand = currentHand;
    }

    // TODO Karo Gegner entsprechend treffen --> nicht nur in Luft schlagen, sondern Gegner anvisieren --> TICKET: FAUSTKAMPF
    private void Punch()
    {
        interactionManager.IsFighting = true;
        ChoosePunchHand();
        ExecutePunchAnimation();
        animationManager.EnableHeadLayer();
    }

    private void ChoosePunchHand()
    {
        if (lastHand == Hands.NULL)
        {
            int random = Random.Range(1, 10);
            if (random % 2 == 0)
            {
                currentHand = Hands.LEFT;
            }
            else
            {
                currentHand = Hands.RIGHT;
            }
        }
        else if (lastHand == Hands.LEFT)
        {
            currentHand = Hands.RIGHT;
        }
        else if (lastHand == Hands.RIGHT)
        {
            currentHand = Hands.LEFT;
        }

        SetLastHand();
    }

    private void ExecutePunchAnimation()
    {
        int random = Random.Range(1, 4);
        // TODO Karo dafür sorgen, dass keine Animation zweimal hintereinander abgespielt wird --> siehe Randomisierung  --> TICKET: FAUSTKAMPF

        if (currentHand == Hands.LEFT)
        {
            switch(random)
            {
                case 1:
                    animationManager.ExecuteBasicHipPunchLeft();
                    break;

                case 2:
                    animationManager.ExecuteBasicPunchLeft();
                    break;

                case 3:
                    animationManager.ExecuteCrossPunchLeft();
                    break;
            }
        }
        else if (currentHand == Hands.RIGHT)
        {
            switch (random)
            {
                case 1:
                    animationManager.ExecuteBasicHipPunchRight();
                    break;

                case 2:
                    animationManager.ExecuteBasicPunchRight();
                    break;

                case 3:
                    animationManager.ExecuteCrossPunchRight();
                    break;
            }
        }
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1f);
        interactionManager.IsFighting = false;
        base.ResetInteraction();
    }
}


