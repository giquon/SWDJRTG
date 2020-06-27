using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
