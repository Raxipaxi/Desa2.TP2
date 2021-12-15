using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]private PlayerModel _player;
    [SerializeField]private GoalReached _goal;
    [SerializeField] private GameObject winScreen; 
    [SerializeField] private GameObject deadScreen;
   // [SerializeField] private AudioManager _audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
        AudioManager.instance.EnviromentMusic(EnviromentSoundClip.LevelMusic);
    }

    private void Subscribe()
    {
        _goal.OnWin += LoadWinScreen;
        _player.OnDead += LoadGameOverScreen;
    }

    void LoadWinScreen()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
            return;
        }
        winScreen.SetActive(true);
    }

    void LoadGameOverScreen()
    {
        deadScreen.SetActive(true);
    }
    
    
    

}
