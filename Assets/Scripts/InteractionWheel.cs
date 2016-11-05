using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionWheel : InteractionGrabbable
{
    float m_fValue;

    public Text m_textDebug;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_fValue = transform.rotation.y;
        if (m_textDebug != null)
            m_textDebug.text = m_fValue.ToString("F0");
	}
}
