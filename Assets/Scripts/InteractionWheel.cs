using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionWheel : InteractionGrabbable
{
    float m_fValue;

    public Text m_textDebug;
    private Quaternion m_qStartRotation;

    public float[] m_arrfSnapValues = { 0, 90, 180, 270, 360 };
    public float m_fErrorRange = 1.5f;
	// Use this for initialization
	void Start ()
    {
        m_qStartRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_fValue = Quaternion.Angle(m_qStartRotation, transform.rotation);
        float fUntransfAngle = m_fValue;
        if(transform.right.z < 0)
        {
            m_fValue = 360.0f - m_fValue;
        }

        if (m_textDebug != null)
            m_textDebug.text = m_fValue.ToString("F0");
	}
}
