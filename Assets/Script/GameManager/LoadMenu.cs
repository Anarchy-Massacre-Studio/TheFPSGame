using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
	void Start ()
    {
        StartCoroutine(load());
	}

    IEnumerator load()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        yield return SceneManager.LoadSceneAsync("Menu");
    }
}
