using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{

    // This code provides our camera to move.
    public GameObject karakter;

    Vector3 offset;          
    void Start()
    {
        offset = transform.position - karakter.transform.position;
    }

    void LateUpdate()
    {
        transform.position = karakter.transform.position + offset;
    }
}

