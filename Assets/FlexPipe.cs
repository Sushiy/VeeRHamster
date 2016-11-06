using UnityEngine;
using UniRx;
using System.Collections;

public class FlexPipe : MonoBehaviour
{
    HWaypoint m_hwayParent;
    MeshRenderer m_meshRThis;
    public float m_fTurnDurationS = 0.25f;

    void Start()
    {
        m_hwayParent = transform.parent.GetComponent<HWaypoint>();
        m_meshRThis = GetComponent<MeshRenderer>();
        m_meshRThis.materials[1].color = Color.green;
        m_hwayParent.p_NextWaypoint.Subscribe(nextWP =>
        {
            Vector3 nextWPpos = nextWP.transform.position;

            var vA = transform.up;
            var vB = (nextWPpos - transform.position).normalized;
            float zAngles = Vector3.Angle(vA, vB);

            Debug.Log(Vector3.Cross(vA, vB));
            if(Vector3.Cross(vA, vB).z < 0f)
            {
                zAngles = -zAngles;
            }
            var targetRotQ = Quaternion.Euler(0f, 0f, zAngles);


            StartCoroutine(LerpTurn(transform.rotation * targetRotQ));
        })
        .AddTo(this.gameObject);
    }

    IEnumerator LerpTurn(Quaternion _qTo)
    {
        float Alpha = 0f;
        Quaternion startQ = transform.rotation;
        m_hwayParent.Connected = false;
        m_meshRThis.materials[1].color = Color.red;

        while (Alpha <= 1f)
        {
            transform.rotation = Quaternion.Slerp(startQ, _qTo, Alpha);
            yield return null;
            Alpha += Time.deltaTime / m_fTurnDurationS;
        }
        m_hwayParent.Connected = true;
        m_meshRThis.materials[1].color = Color.green;
        transform.rotation = _qTo;
    }
}
