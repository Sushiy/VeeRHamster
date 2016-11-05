using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public virtual void Press(InteractionHand _hand)
    {
        //Methodbody will be implemented in childclasses
    }

    public virtual void Release(InteractionHand _hand)
    {
        //Methodbody will be implemented in childclasses
    }
}
