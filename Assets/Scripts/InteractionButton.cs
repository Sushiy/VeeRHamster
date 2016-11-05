using UnityEngine;
using System.Collections;

public class InteractionButton : Interactable
{
    public bool m_bActive = false;
    public bool m_bIsBeingPressed = false;
    private Vector3 m_v3Origin = Vector3.zero;
    public Vector3 m_v3PressAxis = Vector3.forward;
    public float m_fPressDistance = 0.05f;
    public float m_fSpeed = 2f;

    private Vector3 m_v3Target;

    // Use this for initialization
    void Start()
    {
        m_v3Origin = this.transform.position;
    }

    public override void Press(InteractionHand _hand)
    {
        m_v3Target = m_v3Origin + m_v3PressAxis* m_fPressDistance;
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
        Vector3 startP = m_v3Origin;
        while (Alpha < 1f)
        {
            transform.position = Vector3.Lerp(startP, m_v3Target, Alpha);
            yield return null;
            Alpha += Time.deltaTime * m_fSpeed;
        }

        yield return new WaitForSeconds(0.1f);
        startP = m_v3Target;
        Alpha = 0f;
        while (Alpha < 1f)
        {
            transform.position = Vector3.Lerp(startP, m_v3Origin, Alpha);
            yield return null;
            Alpha += Time.deltaTime * m_fSpeed;
        }
        m_bIsBeingPressed = false;
    }
}
