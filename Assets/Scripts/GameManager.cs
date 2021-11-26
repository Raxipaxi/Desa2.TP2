using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private PlayerModel _player;
    [SerializeField]private GoalReached _goal;
    [SerializeField] private GameObject winScreen; 
    [SerializeField] private GameObject deadScreen; 
    
    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        _goal.OnWin += LoadWinScreen;
        _player.OnDead += LoadGameOverScreen;
    }

    void LoadWinScreen()
    {
        winScreen.SetActive(true);
    }

    void LoadGameOverScreen()
    {
        deadScreen.SetActive(true);
    }
    
    
    

}
