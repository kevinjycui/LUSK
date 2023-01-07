using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickEvent : MonoBehaviour
{

    [SerializeField]
    public AudioSource flipSound; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            flipSound.Play();
            SceneManager.LoadScene("Sandbox");
        }
    }
}
