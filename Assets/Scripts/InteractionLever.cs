using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionLever : InteractionGrabbable
{
    float m_fCurrentValue; //Value of the Slider from 0 to 1
    public HingeJoint m_hingeJThis;

    public Text m_textDebug;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float min = m_hingeJThis.limits.min;
        float max = m_hingeJThis.limits.max;

        m_fCurrentValue = 1.0f - ((m_hingeJThis.angle - min)/(max - min));
		ExecuteDecorators(m_fCurrentValue);

        if (m_textDebug != null)
            m_textDebug.text = m_fCurrentValue.ToString("F2");

    }

    public float GetValue()
    {
        return m_fCurrentValue;
    }
}
