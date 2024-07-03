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
        _weapon.OnAmmoChange += UpdateUI;
        UpdateUI(_weapon.Ammo);
    }

    private void UpdateUI(int ammo)
    {
        _ammoCount.text = ammo.ToString();
    }

    private void OnDestroy()
    {
        _weapon.OnAmmoChange -= UpdateUI;
    }
}
