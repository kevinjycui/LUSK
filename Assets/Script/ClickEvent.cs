using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickEvent : MonoBehaviour
{

    [SerializeField]
    public AudioSource audioMusic; 
    [SerializeField]
    public AudioSource flipSound; 

    bool clickFlag = false;
    public int fadeTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            flipSound.Play();

            clickFlag = true;
        }

        if (clickFlag) {
            if (audioMusic.volume > 0.01f)
            {
                audioMusic.volume -= Time.deltaTime / fadeTime;
                return;
            }
    
            audioMusic.volume = 0;
            audioMusic.Stop();
            SceneManager.LoadScene("Sandbox");
        }
    }
}
