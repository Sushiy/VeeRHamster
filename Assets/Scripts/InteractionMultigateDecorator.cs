using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class InteractionMultigateDecorator : MonoBehaviour, IInteractionDecorator {

    public float MAX_VALUE = 1f;


    public HWaypoint WaypointToChange;
    public int Index = 0;
    public List<HWaypoint> NextConnectors = new List<HWaypoint>(); 

    public void Start()
    {
        var old = WaypointToChange.NextWaypoint;
        WaypointToChange.NextWaypoint = NextConnectors[Index];
        // WaypointToChange.ChangeNextWaypointRipple(old, WaypointToChange.NextWaypoint);

        NextConnectors[Index].PreviousWaypoint = WaypointToChange;
    }

    public void OnValueChange(float alpha)
    {
        if(alpha >= MAX_VALUE)
        {
            if(WaypointToChange && WaypointToChange.Connected)
            {
                var old = WaypointToChange.NextWaypoint;
                Index = (Index + 1) % NextConnectors.Count;
                if(NextConnectors[Index] != null)
                {
                    WaypointToChange.NextWaypoint = NextConnectors[Index];
                    NextConnectors[Index].PreviousWaypoint = WaypointToChange;
                }
                else
                {
                    WaypointToChange.NextWaypoint = null;
                }
                // WaypointToChange.ChangeNextWaypointRipple(old, WaypointToChange.NextWaypoint);
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (WaypointToChange)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, WaypointToChange.transform.position);

            Gizmos.color = Color.cyan;
            NextConnectors.ForEach(wp =>
            {
                if(wp)
                    Gizmos.DrawLine(WaypointToChange.transform.position, wp.transform.position);
            });
        }

    }
}
