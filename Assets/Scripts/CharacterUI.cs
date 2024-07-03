using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Character))]
public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private TMP_Text _hpCountText;
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _character.OnHealthChange += UpdateHP;
    }

    private void UpdateHP(float hp)
    {
        _hpBar.fillAmount = hp;
        _hpCountText.text = (hp * _character.MaxHealth).ToString();
    }

    private void OnDestroy()
    {
        _character.OnHealthChange -= UpdateHP;
    }
}
