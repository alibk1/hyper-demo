using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlScript : MonoBehaviour
{
    bool turnright, turnleft;
    public float TurnSpeed;

    public void solagit() 
    {
        turnleft = true;
    }
   public void sagagit()
    {
        turnright = true;
    }
    public void birak()
    {
        turnright = false;
        turnleft = false;
    }
    private void FixedUpdate()
    {
        if (turnright)
        {
            transform.Translate(-TurnSpeed, 0, 0);

        }
        if (turnleft)
        {
            transform.Translate(TurnSpeed, 0, 0);

        }
    }
}
