using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsGame : MonoBehaviour
{
    private bool isActive = false;
    
    private void Awake()
    {
        ActivitiesManager.onAllGamesDeactivated += DeactivateGame;
    }

    private void OnDisable()
    {
        ActivitiesManager.onAllGamesDeactivated -= DeactivateGame;
    }

    public void ActivateGame()
    {
        if (!isActive)
        {
            isActive = true;
            Debug.Log("Game 1 activated!");
        }
        else return;
    }

    private void DeactivateGame()
    {
        if(isActive)
        {
            isActive = false;
            Debug.Log("Game 1 deactivated!");
        }
        else return;
    }

}
