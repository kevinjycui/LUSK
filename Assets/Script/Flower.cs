using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] LayerMask tableMask;
    [SerializeField] FieldManager fm;
    private float currentHeight;
    [SerializeField] float maxHeight;
    private Transform pos;
    [SerializeField] float growthInterval;

    void Start()
    {
        currentHeight = 0;
        pos = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (fm.season == 1) { Grow(); }
        else if (fm.season == 0) { }
        else gameObject.SetActive(false);

    }

    public bool IsTableThere()
    {
        if (Physics.Raycast(transform.position, new Vector3(0f, 1f, 0f), 50f, tableMask))
        {
            return true;
        }
        return false;
    }

    IEnumerator Grow()
    {
        yield return 1;
    }
}
