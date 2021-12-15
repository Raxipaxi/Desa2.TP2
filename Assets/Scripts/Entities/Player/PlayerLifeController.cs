using System;
using UnityEngine;


public class PlayerLifeController : MonoBehaviour
{
    // private LifeUIManager _lifeUI;
    private PlayerModel _player;
    [SerializeField] private Animator _screenFeedback;
    
    private int _currLife;

    public event Action OnDie;
    private void Awake()
    {
        // _lifeUI = FindObjectOfType<LifeUIManager>();
        _player = FindObjectOfType<PlayerModel>();
    }

    private void Start()
    {
        _currLife = _player.data.maxLife;
        // _lifeUI.InitSlider(_currLife);
    }

    public void Subscribe(PlayerController controller)
    {
        controller.OnHit += DamageHandler;
    }


    private void DamageHandler(int damage)
    {
        _screenFeedback.Play("Hitted");
        
        _currLife -= damage;
        AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Grunt);
        
        if (_currLife<=0) {OnDie?.Invoke(); return; }
    }
    
    
}
