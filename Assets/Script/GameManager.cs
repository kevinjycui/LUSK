using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] WaterPuddle waterPuddle;
    
    [Header("Summer")]

    
    [Header("Fall")]


    [Header("Winter")]
    [SerializeField] GameObject Icicle;
    [SerializeField] GameObject snowParticles;

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
        if (Icicle.gameObject.isActiveAndEnabled) new_stal.gameObject.SetActive(false);
    }

    void Summer()
    {
        rainParticles.SetActive(false);
        if (new_waterPuddle != null && new_waterPuddle.isActiveAndEnabled) new_waterPuddle.gameObject.SetActive(false);
    }

    void Fall()
    {

    }

    void Winter()
    {
        snowParticles.SetActive(true);
        if(new_stal == null) new_stal = GameObject.Instantiate(stal);
        else new_stal.gameObject.SetActive(true);
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
        if (new_waterPuddle)
        {
            if (new_waterPuddle == null)
            {
                GameObject.Instantiate(waterPuddle);
            }
            else { new_waterPuddle.gameObject.SetActive(true); }
        }
    }

}
