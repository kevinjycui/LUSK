using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CanvasGroup blackScreen;

    private void OnCollisionStay(Collision collision)
    {
        TheEnd();
    }

    IEnumerator TheEnd()
    {
        while(player.position.z < 70f)
        {
            player.Translate(0f, 0.5f, 2f);
        }

        yield return new WaitForSeconds(2f);

        while (blackScreen.alpha < 1f)
        {
            blackScreen.alpha += 0.005f;
            yield return 5f;
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("MainMenu");
    }
}
