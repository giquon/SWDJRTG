using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrounCheck : MonoBehaviour
{
    private GrounCheck gc;
    public bool isGrounded = false;

    // Start is called before the first frame update
    void Awake()
    {
        gc = GetComponent<GrounCheck>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Platform")
        {
            isGrounded = true; ;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Platform")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Platform")
        {
            isGrounded = false;
        }
    }
}
