using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Season")]
    [SerializeField] private float timeNextSeason = 0f;
    [SerializeField] private float timeInterval;
    [SerializeField, HideInInspector] private int whichSeason;

    [Header("Spring")]
    [SerializeField] GameObject rainParticles;
    
    [Header("Summer")]


    [Header("Fall")]


    [Header("Winter")]
    [SerializeField] stalactite stal;
    [SerializeField] GameObject snowParticles;
    [SerializeField, HideInInspector] stalactite new_stal = null;

    [Header("Transition")]
    [SerializeField] private CanvasGroup blackScreen;

    void Start()
    {
        Spring();
        whichSeason = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeNextSeason < Time.time)
        {
            StartCoroutine(fadeBlack(whichSeason));
            whichSeason = (whichSeason + 1) % 4;
            timeNextSeason += timeInterval;
        }
    }

    void Spring()
    {
        snowParticles.SetActive(false);
        GameObject.Destroy(new_stal);
        rainParticles.SetActive(true);
    }

    void Summer()
    {
        rainParticles.SetActive(false);
    }

    void Fall()
    {

    }

    void Winter()
    {
        snowParticles.SetActive(true);
        new_stal = GameObject.Instantiate<stalactite>(stal);
    }

    IEnumerator fadeBlack(int whichSeason)
    {
        while(blackScreen.alpha < 1f)
        {
            blackScreen.alpha += 0.005f;
            yield return 5f;
        }

        yield return new WaitForSeconds(0.25f);

        if (whichSeason == 0)
        {
            Spring();
        }
        else if (whichSeason == 1)
        {
            Summer();
        }
        else if (whichSeason == 2)
        {
            Fall();
        }
        else if (whichSeason == 3)
        {
            Winter();
        }

        while (blackScreen.alpha > 0f)
        {
            blackScreen.alpha -= 0.005f;
            yield return 5f;
        }
    }

}
