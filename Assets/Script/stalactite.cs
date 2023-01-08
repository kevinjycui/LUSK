using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stalactite: MonoBehaviour
{
    private Transform player;
    private Vector3 pos;
    [SerializeField] public LayerMask snailLayer;
    private Rigidbody rigid;
    [SerializeField] FieldManager fm;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        pos = gameObject.transform.position;
    }

    void Update()
    {
        if (fm.season == 3)
        {
            gameObject.SetActive(true);

            player = GameObject.FindWithTag("Player").transform;
            if ((Mathf.Abs(transform.position.x - player.position.x) < 2.5) && (Mathf.Abs(transform.position.z - player.position.z) < 2.5) && (0 < (transform.position.y - player.position.y)) && (transform.position.y - player.position.y) < 20)
            {
                rigid.useGravity = true;
            }
            else if (fm.stal_fell)
            {
                rigid.useGravity = true;
            }
        }
        else
        {
            fm.stal_fell= false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = pos;
        fm.stal_fell = true;

    }

}
