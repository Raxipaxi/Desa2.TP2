using UnityEngine;
using UnityEngine.UI;

public class LifeUIManager : MonoBehaviour
{
    private Slider _slider;
    [SerializeField]private PlayerModel _player;
    
    private void Awake()
    {

        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        InitSlider(_player.data.maxLife);
    }

    public void Subscribe(PlayerController controller)
    {
        controller.OnHit += ModifyHealthbar;
    }

    public void InitSlider(int inithealth)
    {
        _slider.value =  inithealth;
    }

    public void ModifyHealthbar(int damage)
    {
        _slider.value -= damage;
    }
}
