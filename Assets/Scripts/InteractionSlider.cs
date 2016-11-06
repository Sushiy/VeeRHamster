using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionSlider : InteractionGrabbable
{
    Vector3 m_v3Origin;
    public Vector3 m_v3MoveAxis = Vector3.forward;
    public float m_fMaxPull = 0.5f;
    Vector3 m_v3MaxPosition;

    public float m_fRetractTime = 5.0f;
    float m_fSpeed;

    float m_fCurrentValue; //The current value of the slider

    public Text m_textDebug;

    // Use this for initialization
    void Start ()
    {
        m_rigidThis = GetComponent<Rigidbody>();
        m_v3Origin = m_rigidThis.position;
        m_fSpeed = m_fMaxPull / m_fRetractTime;
        m_v3MaxPosition = m_v3Origin + m_v3MoveAxis * m_fMaxPull;
	}
    void Update()
    {
        float fDist = Vector3.Distance(m_v3Origin, m_rigidThis.position);
        m_fCurrentValue = fDist/m_fMaxPull;
		ExecuteDecorators(m_fCurrentValue);

        m_textDebug.text = m_fCurrentValue.ToString("F1");
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {       
        Vector3 v3Pos = m_rigidThis.position;
        if(m_v3MoveAxis.x != 0)
        {
            float minX = Mathf.Min(m_v3Origin.x, m_v3MaxPosition.x);
            float maxX = Mathf.Max(m_v3Origin.x, m_v3MaxPosition.x);
            v3Pos.x = Mathf.Clamp(v3Pos.x, minX, maxX);
        }
        else if (m_v3MoveAxis.z != 0)
        {
            float minZ = Mathf.Min(m_v3Origin.z, m_v3MaxPosition.z);
            float maxZ = Mathf.Max(m_v3Origin.z, m_v3MaxPosition.z);
            v3Pos.z = Mathf.Clamp(v3Pos.z, minZ, maxZ);
        }

        m_rigidThis.MovePosition(v3Pos);

    }

    public override void Press(InteractionHand _hand)
    {
        base.Press(_hand);
        StopAllCoroutines();
    }
    public override void Release(InteractionHand _hand)
    {
        base.Release(_hand);
        StartCoroutine(LerpRetract());
    }

    IEnumerator LerpRetract()
    {
        yield return new WaitForSeconds(0.2f);
        float Alpha = 0f;
        Vector3 startP = m_v3Origin;

        while (Alpha < 1f)
        {
            transform.position = Vector3.Lerp(m_rigidThis.position, m_v3Origin, Alpha);
            yield return null;
            Alpha += Time.deltaTime * m_fSpeed;
        }

        yield return null;
    }

    public float GetValue()
    {
        return m_fCurrentValue;
    }



}
