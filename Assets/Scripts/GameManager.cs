using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator animator;
    private int fadeOutToHash;

    private string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        fadeOutToHash = Animator.StringToHash("fadeOut");
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SceneTransition(string sceneName)
    {
        animator.SetTrigger(fadeOutToHash);
        Debug.Log("HitScene");
        if (GameObject.Find("Dialogue"))
        {
            GameObject.Find("Dialogue").SetActive(false);
        }
        levelToLoad = sceneName;
    }

    public void OnFadeComplete()
    {
        Debug.Log("Fade Complete");
        SceneManager.LoadScene(levelToLoad);
    }

    public void EndGame()
    {
        animator.SetTrigger(fadeOutToHash);
    }

    public void Retry()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
