using UnityEngine;
using System.Collections;

public class Hamster : MonoBehaviour {

	public enum MovementDir {
		Forward,
		Backward
	};

	public MovementDir CurrentDir = MovementDir.Forward;

	public float MovementSpeed = 0.5f /* meter per second */;
	public HWaypoint CurrentWaypoint;
	public HWaypoint TargetWaypoint;

	public float lineProgress = 0f;

	public HWaypoint SelectNext
	{
		get { return CurrentDir == MovementDir.Forward ? CurrentWaypoint.NextWaypoint : CurrentWaypoint.PreviousWaypoint;  }
	}

	// Use this for initialization
	void Start()
	{
		this.transform.position = CurrentWaypoint.transform.position;
		if(!CurrentWaypoint.Connected)
		{
			ToggleMovementDir();
		}
		this.TargetWaypoint = SelectNext;
	}

	// Update is called once per frame
	void Update () {
		if (!TargetWaypoint) return;

		Vector3 Direction = (TargetWaypoint.transform.position - CurrentWaypoint.transform.position).normalized;

		this.transform.forward = Direction;

		float maxMag = (this.TargetWaypoint.transform.position - (this.CurrentWaypoint.transform.position)).magnitude;

		lineProgress = lineProgress + (1.0f / (1.0f + CurrentWaypoint.Drag)) * MovementSpeed * Time.deltaTime;

		this.transform.position = CurrentWaypoint.transform.position + lineProgress * Direction;

		if(lineProgress >= maxMag)
		{
			this.CurrentWaypoint = this.TargetWaypoint;
			if( ((CurrentDir == MovementDir.Forward) &&  !TargetWaypoint.Connected) || SelectNext == null || ( CurrentDir == MovementDir.Backward && !SelectNext.Connected ))
			{
				ToggleMovementDir();
			}
			this.TargetWaypoint = SelectNext;
			lineProgress = lineProgress - maxMag;
			this.transform.position = CurrentWaypoint.transform.position + lineProgress * Direction;

            if (TargetWaypoint)
                this.TargetWaypoint.Decorators.ForEach(dec => dec.OnWaypointReached(this));
		}
		
	}

	void ToggleMovementDir()
	{
		if(CurrentDir == MovementDir.Forward)
		{
			CurrentDir = MovementDir.Backward;
		}
		else
		{
			CurrentDir = MovementDir.Forward;
		}
	}
}
