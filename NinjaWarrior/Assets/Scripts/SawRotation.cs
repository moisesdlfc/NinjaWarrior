using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotation : MonoBehaviour
{
    private Transform sawRotation;
    void Awake()
    {
        sawRotation = this.transform;
    }

    void FixedUpdate()
    {
        // Efecto rotar saw
        sawRotation.Rotate(new Vector3(sawRotation.rotation.x, sawRotation.rotation.y, sawRotation.rotation.z + 5));
    }
}
