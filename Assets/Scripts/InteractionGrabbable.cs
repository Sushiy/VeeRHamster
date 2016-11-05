using UnityEngine;
using System.Collections;

public class InteractionGrabbable : Interactable
{
    protected Rigidbody m_rigidThis;
    public bool m_bBecomeKinematic = true;
    protected bool m_bIsGrabbed = false;

    void Awake()
    {
        m_rigidThis = GetComponent<Rigidbody>();
    }

    public override void Press(InteractionHand _hand)
    {
        if(m_bBecomeKinematic)
            m_rigidThis.isKinematic = false;
        m_bIsGrabbed = true;
        _hand.SetJointConnectedBody(m_rigidThis);
    }

    public override void Release(InteractionHand _hand)
    {
        if (m_bBecomeKinematic)
        {
            m_rigidThis.isKinematic = true;
            StartCoroutine(ResetKinematic());
        }
        
        m_bIsGrabbed = false;
        _hand.ReleaseJointConnectedBody();
    }

    IEnumerator ResetKinematic()
    {
        yield return null;
        m_rigidThis.isKinematic = false;
    }
}
