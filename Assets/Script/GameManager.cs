using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("Season")]
    [SerializeField] private float timeNextSeason = 0f;
    [SerializeField] private float timeInterval;
    [SerializeField, HideInInspector] private int whichSeason;

    [Header("Spring")]
    [SerializeField] GameObject rainParticles;
    [SerializeField] WaterPuddle waterPuddle;
    [SerializeField, HideInInspector] WaterPuddle new_waterPuddle = null;
    
    [Header("Summer")]


    [Header("Fall")]


    [Header("Winter")]
    [SerializeField] stalactite stal;
    [SerializeField] GameObject snowParticles;
    [SerializeField, HideInInspector] stalactite new_stal = null;
    [SerializeField] bool stalFell;

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
        CheckPuddle();
        snowParticles.SetActive(false);
        rainParticles.SetActive(true);
        if (new_stal != null && new_stal.isActiveAndEnabled) Destroy(new_stal);
    }

    void Summer()
    {
        rainParticles.SetActive(false);
        if (new_waterPuddle != null && new_waterPuddle.isActiveAndEnabled) Destroy(new_waterPuddle);
    }

    void Fall()
    {

    }

    void Winter()
    {
        snowParticles.SetActive(true);
        new_stal = GameObject.Instantiate(stal);
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

    void CheckPuddle()
    {
        if (stalFell) { GameObject.Instantiate(waterPuddle); }
    }

}
