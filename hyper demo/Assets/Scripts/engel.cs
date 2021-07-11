using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engel : MonoBehaviour
{
    int a,b;
    public GameObject[] engeller;

    void Start()
    {
        // This lines activates some walls randomly.
        a = Random.Range(0, 3);
        b = Random.Range(0, 3);
        engeller[a].SetActive(true);
        engeller[b].SetActive(true);
    }


}
