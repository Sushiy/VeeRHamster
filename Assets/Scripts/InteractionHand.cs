using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class InteractionHand : MonoBehaviour
{
    public Interactable m_interactableHover = null;
    public bool m_bIsGrabbing = false;
    public Joint m_jointThis;
    public Rigidbody m_rigidDummy;

    public Text m_textDebug;
    public Text m_textDebug2;

    private int m_iDeviceIndexThis = -1;
    
    // Update is called once per frame
    void Update()
    {
        if (m_iDeviceIndexThis == -1)
            m_iDeviceIndexThis = (int)GetComponent<SteamVR_TrackedObject>().index;
        var device = SteamVR_Controller.Input(m_iDeviceIndexThis);
        if(m_interactableHover != null)
            m_textDebug2.text = m_interactableHover.ToString();
        else
        {
            m_textDebug2.text = "null";
            if(m_jointThis.connectedBody == m_rigidDummy)
                ReleaseJointConnectedBody();
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            m_textDebug.text = "TryPress";
            if (m_interactableHover != null)
            {
                m_textDebug.text = "Press";
                m_bIsGrabbing = true;
                m_interactableHover.Press(this);
            }
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            m_textDebug.text = "TryRelease";
            if (m_interactableHover != null)
            {
                m_textDebug.text = "Release";
                m_bIsGrabbing = false;
                m_interactableHover.Release(this);
            }
                
        }
    }

    public void TriggerExited(Collider _coll)
    {
        m_textDebug.text = "empty";
        if (m_interactableHover != null)
        {
            m_textDebug.text = "Exit";
            m_bIsGrabbing = false;
            m_interactableHover.Release(this);
            m_interactableHover = null;

        }
    }

    public void TriggerStay(Collider _coll)
    {
        if (_coll.CompareTag("Interactable"))
        {
            m_textDebug.text = "interactable";
            m_interactableHover = _coll.GetComponent<Interactable>();
        }
        else
            m_textDebug.text = _coll.tag;
    }

    public void SetJointConnectedBody(Rigidbody _rigid)
    {
        m_jointThis.connectedBody = _rigid;
    }

    public void ReleaseJointConnectedBody()
    {
        m_jointThis.connectedBody = m_rigidDummy;
    }
}
