using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
