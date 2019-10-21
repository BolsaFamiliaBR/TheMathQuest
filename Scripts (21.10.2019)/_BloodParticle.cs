using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BloodParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
