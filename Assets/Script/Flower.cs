using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] LayerMask wallMask;
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
        // if (fm.season == 1) { Grow(); }
        // else if (fm.season == 0) { }
        // else gameObject.SetActive(false);

    }

    public bool IsSomethingAbove()
    {
        if (Physics.Raycast(transform.up, new Vector3(0f, 1f, 0f), 2f, wallMask))
        {
            return true;
        }
        return false;
    }

    IEnumerator Grow()
    {
        Debug.Log("Grow");
        while (!IsSomethingAbove() || transform.position.y < maxHeight)
        {
            transform.localScale += new Vector3(0f, 0.5f, 0f);
            yield return 5f;
        }
    }
}
