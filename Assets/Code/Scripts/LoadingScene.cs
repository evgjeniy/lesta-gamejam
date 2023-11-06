using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Image loadedBarFill;
    [SerializeField] private bool instaLoad;
    [SerializeField] private string sceneName;

    private void Start()
    {
        if(instaLoad)
            LoadScene(sceneName);
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone) 
        { 
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadedBarFill.fillAmount = progress;

            yield return null;
        }
    }
}
