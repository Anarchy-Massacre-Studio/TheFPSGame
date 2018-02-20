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
        yield return SceneManager.LoadSceneAsync("Menu");
    }
}
