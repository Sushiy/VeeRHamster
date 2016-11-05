using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
	private List<IInteractionDecorator> DecoratorList = new List<IInteractionDecorator>();
	public List<IInteractionDecorator> Decorators
	{
		get
		{
			GetComponents(DecoratorList);
			return DecoratorList;
		}
	}

	public virtual void Press(InteractionHand _hand)
    {
		//Methodbody will be implemented in childclasses
    }

    public virtual void Release(InteractionHand _hand)
    {
        //Methodbody will be implemented in childclasses
    }

	public void ExecuteDecorators(float pressValue)
	{
		Decorators.ForEach(dec => dec.OnValueChange(pressValue));
	}
}
