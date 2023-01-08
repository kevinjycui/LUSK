using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour

{
    public bool stal_fell;
    public bool raining;
    public int season;

    // Start is called before the first frame update
    void Start()
    {
        season = 0;
        stal_fell = false;
        raining = false;
    }

    private void Update()
    {
     
    }
}
