using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// When the scene is played, run some specific functionality
/// </summary>
public class OnSceneStart : MonoBehaviour
{
    // When scene is loaded and play begins
    public UnityEvent OnLoad = new UnityEvent();


    private void Awake()
    { 
        SceneManager.sceneLoaded += PlayEvent;
    }

    private void PlayEvent(Scene scene, LoadSceneMode mode)
    {
        OnLoad.Invoke();
    }
}

