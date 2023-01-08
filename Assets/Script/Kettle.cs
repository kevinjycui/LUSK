using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettle : MonoBehaviour
{
    [SerializeField] GameObject steamEffect;
    [SerializeField] bool isActive = false;
    [SerializeField] bool isLoaded = false;
    [SerializeField] GameObject spiderWeb;
    [SerializeField] GameObject leaf;
    [SerializeField] FieldManager fm;
    void Start()
    {
        // steamEffect.SetActive(false);
    }


    void Update()
    {
        if (isActive)
        {
            Steaming();
        }
        LoadedAndSummer();
    }

    IEnumerator Steaming()
    {
        steamEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject.Destroy(spiderWeb);
    }

    void OnTriggerEven(Collider target)
    {
        if (target.gameObject.tag.Equals("leaf") == true)
        {
            GameObject.Destroy(leaf);
            isLoaded = true;
        }
    }
    
    void LoadedAndSummer()
    {
        if (isLoaded && fm.season == 1)
        {
            isActive = true;
        }
    }
}
