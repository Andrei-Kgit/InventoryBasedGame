using TMPro;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoCount;
    private Weapon _weapon;

    void Start()
    {
        _weapon = GetComponent<Weapon>();
        _weapon.OnShoot += UpdateUI;
    }

    private void UpdateUI()
    {
        _ammoCount.text = _weapon.Ammo.ToString();
    }
}
