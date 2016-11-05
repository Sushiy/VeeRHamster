﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class InteractionMultigateDecorator : MonoBehaviour, IInteractionDecorator {

    public float MAX_VALUE = 1f;


    public HWaypoint WaypointToChange;
    public int Index = 0;
    public List<HWaypoint> NextConnectors; 

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
            var old = WaypointToChange.NextWaypoint;
            Index = (Index + 1) % NextConnectors.Count;
            WaypointToChange.NextWaypoint = NextConnectors[Index];
            NextConnectors[Index].PreviousWaypoint = WaypointToChange;
            // WaypointToChange.ChangeNextWaypointRipple(old, WaypointToChange.NextWaypoint);
        }
    }
}
