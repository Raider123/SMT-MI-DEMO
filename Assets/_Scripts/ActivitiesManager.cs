using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitiesManager : MonoBehaviour
{
    /// <summary>
    /// This Script works as a brain and overall manager for all the activities in the game.
    /// Whatever games are present in the game, they should be managed by this script.
    /// </summary>
    public static ActivitiesManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public delegate void OnAllGamesDeactivated();
    public static event OnAllGamesDeactivated onAllGamesDeactivated;

    /// <summary>
    /// Every game should subscribe to this method with the respective deactivation method.
    /// </summary>
    public void DeactivateAllGames()
    {
        if (onAllGamesDeactivated != null)
        {
            onAllGamesDeactivated();
        }
    }

}
