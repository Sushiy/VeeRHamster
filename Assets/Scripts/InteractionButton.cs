using UnityEngine;
using System.Collections;

public class InteractionButton : Interactable
{
    public Transform m_transButtonMesh;
    public bool m_bIsBeingPressed = false;
    private Vector3 m_v3Origin = Vector3.zero;
    public Vector3 m_v3TargetEuler= new Vector3(0,0,15.0f);
    public float m_fPressDistance = 0.05f;
    public float m_fSpeed = 2f;

    public AnimationCurve m_ButtonCurve;

    private Vector3 m_v3Target;

    // Use this for initialization
    void Start()
    {
        m_v3Origin = this.transform.position;
    }

    public override void Press(InteractionHand _hand)
    {
        if(!m_bIsBeingPressed)
            StartCoroutine(LerpPress());
    }

    public override void Release(InteractionHand _hand)
    {
        //No release function
    }

    IEnumerator LerpPress()
    {
        m_bIsBeingPressed = true;
        float Alpha = 0f;
        Quaternion startQ = m_transButtonMesh.localRotation;
        while (Alpha < 1f)
        {
            m_transButtonMesh.localRotation = Quaternion.Slerp(startQ, startQ * Quaternion.Euler(m_v3TargetEuler), m_ButtonCurve.Evaluate(Alpha));
            yield return null;
            Alpha += Time.deltaTime * m_fSpeed;
        }
		ExecuteDecorators(1f);
        yield return new WaitForSeconds(0.1f);
        ExecuteDecorators(0f);
        while (Alpha < 2f)
        {
            m_transButtonMesh.localRotation = Quaternion.Slerp(startQ, startQ * Quaternion.Euler(m_v3TargetEuler), m_ButtonCurve.Evaluate(Alpha));
            yield return null;
            Alpha += Time.deltaTime * m_fSpeed;
        }
        m_bIsBeingPressed = false;
        m_transButtonMesh.localRotation = startQ;
    }
}
