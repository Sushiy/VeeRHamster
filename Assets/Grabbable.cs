using UnityEngine;
using System.Collections;

public class Grabbable : MonoBehaviour
{
    Rigidbody m_rigidThis;

    void Awake()
    {
        m_rigidThis = GetComponent<Rigidbody>();
    }

    public void Grab(Transform _trans)
    {
        this.transform.SetParent(_trans);
        m_rigidThis.isKinematic = true;
    }

    public void Release(Transform _trans)
    {
        this.transform.SetParent(null);
        m_rigidThis.isKinematic = false;
    }
}
