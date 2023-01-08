using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CanvasGroup blackScreen;
    [SerializeField] GameObject cam;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("End");
        StartCoroutine(TheEnd());
    }

    IEnumerator TheEnd()
    {
        player.GetComponent<SnailController>().enabled = false;
        cam.GetComponent<CameraController>().enabled = false;
        while (player.position.z < 70f)
        {
            player.position += new Vector3(0f, 0.1f, 0.5f);
            yield return 5f;
        }

        yield return new WaitForSeconds(2f);
        
        while (blackScreen.alpha < 1f)
        {
            blackScreen.alpha += 0.005f;
            yield return 5f;
        }

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainMenu");
    }
}
