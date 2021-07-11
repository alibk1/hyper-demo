using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public bool shoot,death;
    int upgraded;
    bool upgradebl;
    float oldSpeed;
    public float dist ,a, shootagain, deathfl, speed;
    public GameObject closerenemy, muzzle, controller;
    public AudioSource shootsound,reloadsound;
    public Animator anim;

    void Start()
    {
        oldSpeed = speed; // We make the oldSpeed value equals to speed value because we we'll use it after.
        a = 28; // we need a value to controll character's shoot animation.
        deathfl = 4; // we need deathfl value to controll character's death.
        upgraded = 240; // we will use this value when the character touches to upgrade object. 
    }

    void FixedUpdate()
    {

        if (upgradebl) //When the character touch to "upgrade" object, we make speed value more and we make it equals to older value again after few secs
        {

            speed = oldSpeed + 0.5f;   
            upgraded--;
            if(upgraded == 0)
            {
                speed = oldSpeed;
                upgradebl = false;

            }

        }
        if (!death) // if character is not dead...
        {
            transform.Translate(0, 0, -speed);
            shootagain--;
            if (!shoot) // if we are not shooting to enemies...
            {
                a = 28;
                muzzle.SetActive(false); // muzzle is the fire which opens when character shoot
            }
            else if (shoot) //if we are shooting the enemies...
            {

                a--;
                muzzle.SetActive(true); //  we activate the muzzle
                if (a > 25)
                {
                    shootsound.Play();
                }
                if (a < 15 && a > 12) // between this values, we disactivate muzzle and we play reload sound.
                {
                    muzzle.SetActive(false);
                    reloadsound.Play();

                }
                closerenemy.GetComponent<enemy>().death = true; // when we shoot, the closer enemy to us is dying.
                if (a <= 0)
                {
                    shoot = false; // when a is less then 0, we make the shoot value equals to "false".
                }

            }
        }
        else // when character die, we make the death anim play after few secs. 
        {
            deathfl--;
            if(deathfl == 0)
            {
                anim.Play("m_death_A");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")// when character touch to the enemy, it dies.
        {
            death = true;
            controller.GetComponent<GameControl>().lost(); // we call lost() function of the game controller.
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false; 
           
        }
        if (other.gameObject.name == "FallDownDeath")// when character falls down from game area, it dies.
        {
            death = true;
            controller.GetComponent<GameControl>().isCharacterFalled = true;
            controller.GetComponent<GameControl>().lost(); // we call lost() function of the game controller.
           
        }
        if (other.gameObject.name == "finish") // when character reach to the finish, it wins.
        {
            controller.GetComponent<GameControl>().enemycount = 0;

           
        }
        if (other.gameObject.name == "Upgrade")// when character touch to the "upgrade" object, its speed is being more for a while.
        {
            upgradebl = true;
            other.gameObject.SetActive(false);
           
        }
    }

}
