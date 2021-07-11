using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject karakter,blood, punchsoundgo, controller;
    public Animator  anim;
    public bool  death, engelcontrol, karakterdeath;
    public float b,a, distance, deathcontrol, maxDistToDie, deathDestroy,krkdeathfl;

    void Start()
    {
        controller = GameObject.Find("GameController"); // We define our controller gameobject.
        a = Random.Range(31, 35); // We defined an integer to dettermine an enemy's spped randomly.
        b = a / 100f;
        karakter = GameObject.Find("karakter"); // We call our character for per enemy.
        maxDistToDie = 50; //This value is the longest distance between the character and the enemy, which is required for the enemy to hit the wall and die.
        deathcontrol = 25; // DeathControl is an integer to controll the distance which enemy between our character.
        deathDestroy = 60; // DeathDestroy is an integer to destroy enemies from hierarchy after their death. 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        punchsoundgo = GameObject.Find("PunchSound");
        karakterdeath = karakter.GetComponent<character>().death;
        krkdeathfl = karakter.GetComponent<character>().deathfl;
        if (distance < maxDistToDie)
        {
            engelcontrol = true;
        }
        if (!death && !karakterdeath) // If this enemy or the character did not die.
        {
            transform.Translate(0, 0, b);
        }
        if (karakterdeath) // If character did die
        {
            if (krkdeathfl > 0)
            {
                anim.Play("m_melee_combat_attack_A");
                if (!controller.GetComponent<GameControl>().isCharacterFalled)
                {
                    punchsoundgo.GetComponent<AudioSource>().Play();
                }
            }
        }

        distance = Vector3.Distance(this.transform.position, karakter.transform.position); // distance between character and enemy.
        if (distance < deathcontrol)
        {
            transform.LookAt(karakter.transform);
            if(distance < karakter.GetComponent<character>().dist) // Controlls the distance between character.If the enemy is the closer enemy to our character, character shoots the enemy 
            {
                
                if (karakter.GetComponent<character>().shootagain <= 0) // Character must wait for a while to kill others after he killed an enemy.
                {
                    karakter.GetComponent<character>().closerenemy = this.gameObject;
                    karakter.GetComponent<character>().shoot = true;
                    karakter.GetComponent<Animator>().Play("m_pistol_shoot");
                    karakter.GetComponent<character>().dist = distance;
                }
            }
        }
        else
        {
            engelcontrol = false;
        }
        if (death) // When enemy died...
        {
            if (deathDestroy == 60)
            {
                controller.GetComponent<GameControl>().enemycount -= 1;// ...the game controller's enemycount value decreases by 1 unit.
            }
            blood.SetActive(true); 
            deathcontrol = 0;
            karakter.GetComponent<character>().shootagain = 10; // ...we send this value to character script to make the character wait for a while.
            anim.Play("m_death_A");
            deathDestroy--;
            if(deathDestroy == 58)
            {
                karakter.GetComponent<character>().dist = 50; // ...we send this value to character script to fix some bugs.
            }
            if(deathDestroy == 0)
            {
                GameObject.Destroy(this.gameObject); // ...after few secs we destroy enemy
            }
        }
    }
}
