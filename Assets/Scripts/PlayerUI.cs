using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private TMP_Text _hpCountText;
    [SerializeField] private TMP_Text _headDefText;
    [SerializeField] private TMP_Text _bodyDefText;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _player.OnDefenceChange += UpdateDefence;
        _player.OnHealthChange += UpdateHP;
    }

    private void UpdateDefence(float head, float body)
    {
        _headDefText.text = head.ToString();
        _bodyDefText.text = body.ToString();
    }

    private void UpdateHP(float hp)
    {
        _hpBar.fillAmount = hp;
        _hpCountText.text = (hp * _player.MaxHealth).ToString();
    }

    private void OnDestroy()
    {
        _player.OnHealthChange -= UpdateHP;
        _player.OnDefenceChange -= UpdateDefence;
    }
}
