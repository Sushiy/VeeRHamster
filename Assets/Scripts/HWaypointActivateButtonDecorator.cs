using UnityEngine;
using System.Collections;
using System;

public class HWaypointActivateButtonDecorator : MonoBehaviour, IHWaypointDecorator {
	public GameObject ButtonToActivate;

	public void OnWaypointReached(Hamster hamster)
	{
		System.Media.SystemSounds.Asterisk.Play();

	}
}
