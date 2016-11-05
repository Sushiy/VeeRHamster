using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HWaypoint))]
public class HWaypointInspector : Editor {

	public override void OnInspectorGUI()
	{
		var waypoint = (HWaypoint)target;

		HWaypoint currentPrev = waypoint.PreviousWaypoint;
		HWaypoint currentNext = waypoint.NextWaypoint;

		DrawDefaultInspector();

		if(GUI.changed)
		{
			// Previous Waypoint changed
			// Go to previous and change next to null
			if(currentPrev != waypoint.PreviousWaypoint)
			{
				waypoint.ChangePrevWaypointRipple(currentPrev, waypoint.PreviousWaypoint);
			}

			if (currentNext != waypoint.NextWaypoint)
			{
				waypoint.ChangeNextWaypointRipple(currentNext, waypoint.NextWaypoint);
			}
		}
	}

}
