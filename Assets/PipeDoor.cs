using UnityEngine;
using UniRx;
using System.Collections;

public class PipeDoor : MonoBehaviour
{
    HWaypoint m_hwayParent;
    MeshRenderer m_meshRThis;
    
    void Start()
    {
        m_hwayParent = transform.parent.GetComponent<HWaypoint>();
        m_meshRThis = GetComponent<MeshRenderer>();
        m_hwayParent.p_Connected.Subscribe(State =>
        {
            m_meshRThis.material.color = State ? Color.green : Color.red;
        })
        .AddTo(this.gameObject);
    }
}
