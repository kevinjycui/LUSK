using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] LayerMask tableMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isTableThere()
    {
        if (Physics.Raycast(transform.position, new Vector3(0f, 1f, 0f), 50f, tableMask))
        {
            return true;
        }
        return false;
    }
}
