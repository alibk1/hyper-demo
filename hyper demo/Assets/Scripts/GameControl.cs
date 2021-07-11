using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public int level,i, enemycount; 
    float x, z;
    public GameObject enemy,savior, panelstart,panellost,panelwon;
    public GameObject[] upgrades;
    public Text enemylefttxt, leveltxt;
    public AudioSource WonSound, LostSound, GameTheme;
    public bool isCharacterFalled;

    void Start()
    {
        upgrades[Random.Range(0, 3)].SetActive(true); // This code activates a "upgrade" gameobject randomly..
        Time.timeScale = 0; // We need timeScale to be 0 at start.
        level = PlayerPrefs.GetInt("level");
        if(level == 0)
        {
            level = 1;
            PlayerPrefs.SetInt("level", level);
        }
        enemycount = 10 + level*2; // This code counts our enemies at start
    }

    void FixedUpdate()
    {
        enemylefttxt.text = enemycount.ToString();
        leveltxt.text = level.ToString();
        z = Random.Range(-12,11); // This code chooses the z parameter of an enemy we created.
        x = Random.Range(205,215); // This code chooses the x parameter of an enemy we created.
        if (i<level*2)  // We have 10 default enemies. And every level, game creates 2 more enemies with this code. 
        {
            GameObject.Instantiate(enemy, new Vector3(x, 0.25f, z), savior.transform.rotation);
            i += 1;
        }
        if(enemycount == 0) // When we kill all of the enemies, this code calls won() function.
        {
            won();
        }
    }
    public void startt() // To do function at start
    {
        Time.timeScale = 1;
        panelstart.gameObject.SetActive(false);
    }
    public void won() // To do function when we won
    {
        level += 1;
        PlayerPrefs.SetInt("level", level);
        Time.timeScale = 0;
        panelwon.gameObject.SetActive(true);
        GameTheme.Stop();
        WonSound.Play();
        
    }
    public void lost() // to do function when we lost
    {
        panellost.gameObject.SetActive(true);
        GameTheme.Stop();
        LostSound.Play();
    }
    public void replay() 
    {
        Application.LoadLevel(0);
    }
}
