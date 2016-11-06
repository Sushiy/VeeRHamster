using UnityEngine;
using UniRx;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FlexPipe : MonoBehaviour
{
    HWaypoint m_hwayParent;
    MeshRenderer m_meshRThis;
    public float m_fTurnDurationS = 0.25f;

    [System.Serializable]
    public class WaypointToRotation
    {
        public HWaypoint point;
        public Vector3 EulerRotation;
    }

    public List<WaypointToRotation> RotationsPerWaypoint;

    [Range(0, 2)]
    public int PlaneIndex = 2;

    void Start()
    {
        m_hwayParent = transform.parent.GetComponent<HWaypoint>();
        m_meshRThis = GetComponent<MeshRenderer>();
        m_meshRThis.materials[1].color = Color.green;
        m_hwayParent.p_NextWaypoint.Subscribe(nextWP =>
        {
            WaypointToRotation rotateTowards = RotationsPerWaypoint.Where(wp => nextWP == wp.point).DefaultIfEmpty(null).FirstOrDefault();
            if(rotateTowards != null)
                StartCoroutine(LerpPress(rotateTowards.EulerRotation));
        })
        .AddTo(this.gameObject);
    }

    IEnumerator LerpPress(Vector3 m_v3TargetEuler)
    {
        m_hwayParent.Connected = false;
        m_meshRThis.materials[1].color = Color.red;
        float Alpha = 0f;
        Quaternion startQ = transform.localRotation;
        Quaternion targetQ =  Quaternion.Euler(m_v3TargetEuler);
        while (Alpha < 1f)
        {
            transform.localRotation = Quaternion.Slerp(startQ, targetQ, (Alpha));
            yield return null;
            Alpha += Time.deltaTime / m_fTurnDurationS;
        }
        transform.localRotation = targetQ;
        m_meshRThis.materials[1].color = Color.green;
        m_hwayParent.Connected = true;
    }
}
