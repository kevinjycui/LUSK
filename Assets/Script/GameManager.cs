using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] FieldManager fieldManager;

    [Header("Season")]
    [SerializeField] private float timeNextSeason = 0f;
    [SerializeField] private float timeInterval;
    [SerializeField, HideInInspector] private int whichSeason;


    [Header("Spring")]
    [SerializeField] GameObject rainParticles;
    [SerializeField] WaterPuddle[] waterPuddles;
    [SerializeField] Flower[] flowers;

    [Header("Summer")]
    [SerializeField] GameObject sunParticles;

    [Header("Fall")]


    [Header("Winter")]
    [SerializeField]
    stalactite[] icicles;
    [SerializeField] GameObject snowParticles;

    [Header("Transition")]
    [SerializeField] private CanvasGroup blackScreen;

    void Start()
    {
        waterPuddles = FindObjectsOfType<WaterPuddle>();
        icicles = FindObjectsOfType<stalactite>();
        flowers = FindObjectsOfType<Flower>();

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
        fieldManager.season = 0;
        snowParticles.SetActive(false);
        rainParticles.SetActive(true);
        for (int i = 0; i < waterPuddles.Length; i++)
        {
            waterPuddles[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < flowers.Length; i++) { flowers[i].gameObject.SetActive(true);}
        fieldManager.raining = true;
    }

    void Summer()
    {
        fieldManager.season = 1;
        rainParticles.SetActive(false);
        fieldManager.raining = false;
        sunParticles.SetActive(true);
    }

    void Fall()
    {
        sunParticles.SetActive(false);
        fieldManager.season = 2;
    }

    void Winter()
    {
        fieldManager.season = 3;
        snowParticles.SetActive(true);
        for(int i = 0; i < icicles.Length; i++) { icicles[i].gameObject.SetActive(true); }
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
