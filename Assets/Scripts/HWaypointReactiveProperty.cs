using UnityEngine;
using System.Collections;
using UniRx;

[System.Serializable]
public class HWaypointReactiveProperty : ReactiveProperty<HWaypoint> {
	public HWaypointReactiveProperty()
		: base()
	{
	}

	public HWaypointReactiveProperty(HWaypoint waypoint)
		: base(waypoint)
	{
	}
}
