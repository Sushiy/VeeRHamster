using UnityEngine;
using System.Collections;
using UniRx;

public class HWaypointKillDecorator : MonoBehaviour, IHWaypointDecorator
{
    public HWaypoint m_startPoint;
    private double DeathDelay = 1.5;

    public void OnWaypointReached(Hamster hamster)
    {
        var timer = Observable.Timer(System.TimeSpan.FromSeconds(DeathDelay)).Subscribe(l =>
        {
            hamster.CurrentWaypoint = m_startPoint;
            hamster.TargetWaypoint = m_startPoint.NextWaypoint;

            hamster.CurrentDir = Hamster.MovementDir.Forward;
        });
        
    }
}
