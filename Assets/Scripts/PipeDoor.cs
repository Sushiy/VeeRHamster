using UnityEngine;
using UniRx;
using System.Collections;

public class PipeDoor : MonoBehaviour
{
    HWaypoint m_hwayParent;
    MeshRenderer m_meshRThis;

    private Quaternion m_qOrigin;
    public Vector3 m_v3TargetEuler = new Vector3(0, 0, 90.0f);
    public float m_fPressDistance = 0.05f;
    public float m_fSpeed = 2f;

    public AnimationCurve m_ButtonCurve;

    private Vector3 m_v3Target;

    void Start()
    {
        m_qOrigin = this.transform.localRotation;
        m_hwayParent = transform.parent.GetComponent<HWaypoint>();
        m_meshRThis = GetComponent<MeshRenderer>();
        m_hwayParent.p_Connected.Subscribe(State =>
        {
            if(State && m_hwayParent.NextWaypoint != null)
            {
                m_meshRThis.material.color = Color.green;
                StopAllCoroutines();
                StartCoroutine(LerpOpen());
            }

            else
            {
                m_meshRThis.material.color = Color.red;
                StopAllCoroutines();
                StartCoroutine(LerpClose());
            }
        })
        .AddTo(this.gameObject);
    }

    IEnumerator LerpOpen()
    {
        float Alpha = 0f;
        while (Alpha < 1f)
        {
            transform.localRotation = Quaternion.Slerp(m_qOrigin, m_qOrigin * Quaternion.Euler(m_v3TargetEuler), m_ButtonCurve.Evaluate(Alpha));
            yield return null;
            Alpha += Time.deltaTime * m_fSpeed;
        }
        transform.localRotation = m_qOrigin * Quaternion.Euler(m_v3TargetEuler);
    }

    IEnumerator LerpClose()
    {
        float Alpha = 0f;
        while (Alpha < 1f)
        {
            transform.localRotation = Quaternion.Slerp(m_qOrigin * Quaternion.Euler(m_v3TargetEuler), m_qOrigin, m_ButtonCurve.Evaluate(Alpha));
            yield return null;
            Alpha += Time.deltaTime * m_fSpeed;
        }
        transform.localRotation = m_qOrigin;
    }
}
