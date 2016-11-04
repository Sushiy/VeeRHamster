using UnityEngine;
using System.Collections;

public class HWaypoint : MonoBehaviour {

	public float Drag = 0.0f;
	public bool Connected = true;
	public HWaypoint PreviousWaypoint;
	public HWaypoint NextWaypoint;

	public void OnDrawGizmos()
	{

		if(NextWaypoint!=null)
		{
			if(Connected)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(this.transform.position, NextWaypoint.transform.position);
			}
			else
			{
				Gizmos.color = Color.red;
				var to = (this.transform.position + (NextWaypoint.transform.position - this.transform.position) * 0.5f);

				Gizmos.DrawLine(this.transform.position, to);
			}
		}
	}
}
