using UnityEngine;
using System.Collections;

public class OneBitSoundEngine : MonoBehaviour {

	public int sum = 0;


	public float Delay = 0f;
	public float DelayOne = 1f;
	public float DelayZero = 0.25f;


	// Update is called once per frame
	void Update () {

		if(Delay <= 0f)
		{
			if((sum & 1) == 1)
			{
				System.Media.SystemSounds.Beep.Play();
				Delay += DelayOne;
			}
			else
			{
				Delay += DelayZero;
			}
			sum >>= 1;
		}
		Delay -= Time.deltaTime;


		for(int i=1;i<=9;++i)
		{
			KeyCode cc = (KeyCode)(KeyCode.Alpha0 + i);
			if(Input.GetKeyDown(cc))
			{
				sum += i;
			}
		}
	}
}
