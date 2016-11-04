using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionHand : MonoBehaviour
{
    public Grabbable m_grabActiveObject = null;
    public bool m_bIsGrabbing = false;
    public Joint m_jointThis;

    public Text m_textDebug;

    private int m_iDeviceIndexThis = -1;
    
    // Update is called once per frame
    void Update()
    {
        if (m_iDeviceIndexThis == -1)
            m_iDeviceIndexThis = (int)GetComponent<SteamVR_TrackedObject>().index;
        var device = SteamVR_Controller.Input(m_iDeviceIndexThis);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            m_textDebug.text = "TryGrab";
            if (m_grabActiveObject != null)
            {
                m_textDebug.text = "Grab";
                m_bIsGrabbing = true;
                //m_grabActiveObject.Grab(this.transform);
                Rigidbody m_rigidObject = m_grabActiveObject.GetComponent<Rigidbody>();
                m_jointThis.connectedBody = m_rigidObject;
            }
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            m_textDebug.text = "TryRelease";
            if (m_grabActiveObject != null && m_bIsGrabbing == true)
            {
                m_textDebug.text = "Release";
                m_bIsGrabbing = false;
                //m_grabActiveObject.Release(this.transform);
                m_jointThis.connectedBody = null;
            }
                
        }
    }

    public void TriggerExited(Collider _coll)
    {
        m_textDebug.text = "empty";
        if (m_grabActiveObject != null)
            m_grabActiveObject = null;
    }

    public void TriggerEntered(Collider _coll)
    {
        if (_coll.CompareTag("Grabbable"))
        {
            m_textDebug.text = "grabbable";
            m_grabActiveObject = _coll.GetComponent<Grabbable>();
        }
        else
            m_textDebug.text = _coll.tag;
    }
}
