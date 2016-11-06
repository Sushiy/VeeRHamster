using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class InteractionFaderDecorator : MonoBehaviour, IInteractionDecorator {
    [System.Serializable]
    public class FloatToWaypoint
    {
        public float lowerBorder;
        public float upperBorder;

        public HWaypoint WaypointToToggle;
    }

    public List<FloatToWaypoint> RangesPerWaypoint;

    public void OnValueChange(float alpha)
    {
        //Debug.Log(alpha);
        RangesPerWaypoint.ForEach(ftw => {
            if(ftw.WaypointToToggle)
                ftw.WaypointToToggle.Connected = (ftw.lowerBorder <= alpha && alpha < ftw.upperBorder);
        });
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        RangesPerWaypoint.ForEach(wp =>
        {
            if(wp.WaypointToToggle)
                Gizmos.DrawLine(this.transform.position, wp.WaypointToToggle.transform.position);
        });
    }
}
