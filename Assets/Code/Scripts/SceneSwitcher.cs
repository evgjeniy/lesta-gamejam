using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] string nextlevel;
    [SerializeField] Animator transition;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadScene(nextlevel));
    }

    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition?.SetTrigger("Start");
        yield  return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
}
