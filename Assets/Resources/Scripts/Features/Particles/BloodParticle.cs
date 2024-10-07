using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Deactivate", 0.6f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
