using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class InteractionSoundDecorator : MonoBehaviour, IInteractionDecorator {
	public float MAX_VALUE = 1f;
	public float MIN_VALUE = 0f;

	public AudioClip Audio_On;
	public AudioClip Audio_Off;

	public AudioClip Audio_Change;

	AudioSource aSource;

	void Start()
	{
		aSource = GetComponent<AudioSource>();
	}

	public void OnValueChange(float alpha)
	{
		if(alpha >= MAX_VALUE)
		{
			if(Audio_On)
				aSource.PlayOneShot(Audio_On);

		}
		else if (alpha == MIN_VALUE)
		{
			if (Audio_Off)
				aSource.PlayOneShot(Audio_Off);
		}
		else
		{
			if(Audio_Change)
				aSource.PlayOneShot(Audio_Change);
		}
	}
}
