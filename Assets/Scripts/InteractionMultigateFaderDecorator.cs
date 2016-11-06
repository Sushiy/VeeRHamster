using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using FloatToWaypoint = InteractionFaderDecorator.FloatToWaypoint;

public class InteractionMultigateFaderDecorator : MonoBehaviour, IInteractionDecorator {
    public HWaypoint WaypointToChange;
    public int Index = 0;
    public List<FloatToWaypoint> RangesPerWaypoint; 

    public void Start()
    {
        var old = WaypointToChange.NextWaypoint;
        OnValueChange(0f);
    }

    public void OnValueChange(float alpha)
    {
        RangesPerWaypoint.ForEach(ftw => {
            if (ftw.WaypointToToggle && (ftw.lowerBorder <= alpha && alpha < ftw.upperBorder) && WaypointToChange.Connected)
            {
                var old = WaypointToChange.NextWaypoint;
                old.PreviousWaypoint = null;
                if (ftw.WaypointToToggle != null)
                {
                    WaypointToChange.NextWaypoint = ftw.WaypointToToggle;
                    ftw.WaypointToToggle.PreviousWaypoint = WaypointToChange;
                }
                else
                {
                    WaypointToChange.NextWaypoint = null;
                }
            }
        });
    }

    public void OnDrawGizmos()
    {
        if (WaypointToChange)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, WaypointToChange.transform.position);

            Gizmos.color = Color.cyan;
            RangesPerWaypoint.ForEach(wp =>
            {
                if (wp.WaypointToToggle)
                    Gizmos.DrawLine(WaypointToChange.transform.position, wp.WaypointToToggle.transform.position);
            });
        }

    }
}
