using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTest : MonoBehaviour
{

    [SerializeField] public Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        manager.season = 0;
    }
}
