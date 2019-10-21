using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Hearth : MonoBehaviour
{
    public GameObject hearth;
    public void Pegar()
    {
        Destroy(hearth);
    }
}
