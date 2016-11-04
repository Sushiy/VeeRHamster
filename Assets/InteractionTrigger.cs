using UnityEngine;
using System.Collections;

public class InteractionTrigger : MonoBehaviour
{
    public InteractionHand m_handThis;

    void Awake()
    {
        if (m_handThis == null)
            m_handThis = transform.parent.GetComponent<InteractionHand>();
    }
    
    public void OnTriggerEnter(Collider _coll)
    {
        m_handThis.TriggerEntered(_coll);
    }

    public void OnTriggerExit(Collider _coll)
    {
        m_handThis.TriggerExited(_coll);
    }
}
