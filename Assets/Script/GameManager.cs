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
    [SerializeField] Flower flower;
    
    [Header("Summer")]

    
    [Header("Fall")]


    [Header("Winter")]
    [SerializeField] GameObject Icicle;
    private Transform Icicle_transform;
    [SerializeField] GameObject snowParticles;

    [Header("Transition")]
    [SerializeField] private CanvasGroup blackScreen;

    void Start()
    {
        Icicle_transform = Icicle.transform;
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
        rainParticles.SetActive(true);
        fieldManager.raining = true;
        flower.gameObject.SetActive(true);

        if (Icicle.gameObject.activeSelf) Icicle.gameObject.SetActive(false);
        Icicle.transform.position = Icicle_transform.position;
        if (fieldManager.stal_fell == true) waterPuddle.gameObject.SetActive(true);
        fieldManager.stal_fell = false;
    }

    void Summer()
    {
        rainParticles.SetActive(false);
        fieldManager.raining = false;
        if (waterPuddle != null && waterPuddle.gameObject.activeSelf) waterPuddle.gameObject.SetActive(false);
    }

    void Fall()
    {
        flower.gameObject.SetActive(false);
    }

    void Winter()
    {
        snowParticles.SetActive(true);
        Icicle.gameObject.SetActive(true);
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
