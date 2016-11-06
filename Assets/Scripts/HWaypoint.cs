using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;


public class HWaypoint : MonoBehaviour {

	public float Drag = 0.0f;

	public BoolReactiveProperty p_Connected = new BoolReactiveProperty(true);
	public bool Connected { get { return p_Connected.Value; } set { p_Connected.Value = value; } }

	public HWaypointReactiveProperty p_PreviousWaypoint = new HWaypointReactiveProperty();
	public HWaypoint PreviousWaypoint { get { return p_PreviousWaypoint.Value; } set { p_PreviousWaypoint.Value = value; } }


	public HWaypointReactiveProperty p_NextWaypoint = new HWaypointReactiveProperty();
	public HWaypoint NextWaypoint { get { return p_NextWaypoint.Value; } set { p_NextWaypoint.Value = value; } }

	private List<IHWaypointDecorator> DecoratorList = new List<IHWaypointDecorator>();
	public List<IHWaypointDecorator> Decorators
	{
		get
		{
			GetComponents(DecoratorList);
			return DecoratorList;
		}
	}

	public void OnDrawGizmos()
	{
		if(NextWaypoint!=null)
		{
			if(Connected)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(this.transform.position, NextWaypoint.transform.position);

				var from = (this.transform.position + (NextWaypoint.transform.position - this.transform.position) * 0.3f);
				Vector3[] arrows = new Vector3[]
				{
					(this.transform.position + (NextWaypoint.transform.position - this.transform.position) * 0.15f + transform.right * 0.15f)
					, (this.transform.position + (NextWaypoint.transform.position - this.transform.position) * 0.15f - transform.right * 0.15f)
					, (this.transform.position + (NextWaypoint.transform.position - this.transform.position) * 0.15f + transform.forward * 0.15f)
					, (this.transform.position + (NextWaypoint.transform.position - this.transform.position) * 0.15f - transform.forward * 0.15f)
					, (this.transform.position + (NextWaypoint.transform.position - this.transform.position) * 0.15f + transform.up * 0.15f)
					, (this.transform.position + (NextWaypoint.transform.position - this.transform.position) * 0.15f - transform.up * 0.15f)
				};

				foreach(var arrow in arrows)
				{
					Gizmos.DrawLine(from, arrow);
				}

			}
			else
			{
				Gizmos.color = Color.red;
				var to = (this.transform.position + (NextWaypoint.transform.position - this.transform.position) * 0.5f);

				Gizmos.DrawLine(this.transform.position, to);
			}
		}
	}

	public void ChangeNextWaypointRipple(HWaypoint old, HWaypoint _new)
	{
		if (old != null)
		{
			old.PreviousWaypoint = null;

			if (old.NextWaypoint != null)
				old.NextWaypoint.ChangePrevWaypointRipple(old.NextWaypoint, null);

		}

		if (_new != null)
		{
			_new.PreviousWaypoint = this;
			this.Connected = true;

			_new.ChangeNextWaypointRipple(null, _new.NextWaypoint);
		}
		else
		{
			this.Connected = false;
		}

	}

	public void ChangePrevWaypointRipple(HWaypoint old, HWaypoint _new)
	{
		if (old != null)
		{
			old.NextWaypoint = null;
			old.Connected = false;
		}

		if (_new != null)
			_new.NextWaypoint = this;
	}

}
