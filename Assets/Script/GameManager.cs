using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Season")]
    [SerializeField, HideInInspector] private float timeNextSeason = 0f;
    [SerializeField, HideInInspector] private float timeInterval = 600f;
    [SerializeField, HideInInspector] private int whichSeason = 0;

    [Header("Spring")]
    
    
    [Header("Summer")]


    [Header("Fall")]


    [Header("Winter")]
    [SerializeField] stalactite stal;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeNextSeason < Time.time)
        {
            if(whichSeason == 0)
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
            whichSeason = (whichSeason + 1) % 4;
            timeNextSeason += timeInterval;
        }
    }

    void Spring()
    {

    }

    void Summer()
    {

    }

    void Fall()
    {

    }

    void Winter()
    {

    }
}
