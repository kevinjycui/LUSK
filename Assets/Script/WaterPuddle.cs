using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPuddle : MonoBehaviour

{
    [SerializeField]
    public FieldManager fm;

    private void Update()
    {
        if (!(fm.season == 1) || !fm.stal_fell) gameObject.SetActive(false);
    }


}
