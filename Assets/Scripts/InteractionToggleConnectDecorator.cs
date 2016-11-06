using UnityEngine;
using System.Collections;
using System;
using UniRx;


public class InteractionToggleConnectDecorator : MonoBehaviour, IInteractionDecorator
{
    public float MAX_VALUE = 1f;
    public float MIN_VALUE = 0f;

    public HWaypoint WaypointToToggle;

    void Start()
    {
    }

    public void OnValueChange(float alpha)
    {
        if(alpha >= MAX_VALUE)
        {
            if (WaypointToToggle)
                WaypointToToggle.Connected = !WaypointToToggle.Connected;
        }
        else if (alpha == MIN_VALUE)
        {
        }
    }

    public void OnDrawGizmos()
    {
        if(WaypointToToggle)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, WaypointToToggle.transform.position);
        }

    }
}
