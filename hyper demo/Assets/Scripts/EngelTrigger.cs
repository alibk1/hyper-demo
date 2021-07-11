using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelTrigger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // When enemy touch to the walls, it dies (if it close to the character) 
        if (other.gameObject.tag == "enemy")
        {
            if (other.gameObject.GetComponent<enemy>().engelcontrol == true)
            {
                other.gameObject.GetComponent<enemy>().death = true;
            }
        }
    }
}
