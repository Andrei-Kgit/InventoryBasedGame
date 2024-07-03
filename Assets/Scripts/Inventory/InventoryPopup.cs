using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    public event Action<Item> OnItemUse;
    [Header("Item stats UI")]
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _weightText;
    [SerializeField] private TMP_Text _defenceText;
    [SerializeField] private TMP_Text _healValueText;
    [SerializeField] private TMP_Text _ammoValueText;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private GameObject _defStatsBox;
    [SerializeField] private GameObject _healStatsBox;
    [SerializeField] private GameObject _ammoStatsBox;
    [Header("Popup behaviour")]
    [SerializeField] private TMP_Text _useButtonText;
    [SerializeField] private Button _removeItem;
    [SerializeField] private Button _useItemButton;
    private Button _backButton;

    private void Awake()
    {
        _backButton = GetComponent<Button>();
        _backButton.onClick.AddListener(Hide);
        Hide();
    }

    public void Show(Item item, InventorySlot slot)
    {
        if (item != null)
        {
            _useItemButton.onClick.AddListener(delegate { item.Use(); Hide(); });
            _removeItem.onClick.AddListener(delegate { item.RemoveItem(); Hide(); });

            _defStatsBox.SetActive(false);
            _healStatsBox.SetActive(false);

            _weightText.text = $"{item.Weight.ToString()} Í„";
            _itemName.text = item.Name;
            _useButtonText.text = item.ActionButtonText;
            _itemIcon.sprite = item.Icon;
            gameObject.SetActive(true);

            switch (item.ItemType)
            {
                case ItemType.Ammo:
                    _ammoStatsBox.SetActive(true);
                    _ammoValueText.text = item.CurrentStacks.ToString();
                    break;
                case ItemType.Med:
                    _healStatsBox.SetActive(true);
                    var medItem = item as MedItem;
                    _healValueText.text = medItem.HealStrength.ToString();
                    break;
                case ItemType.Equipment:
                    _defStatsBox.SetActive(true);
                    var equipment = item as EquipmentItem;
                    _defenceText.text = equipment.Defence.ToString();
                    _useItemButton.onClick.RemoveAllListeners();
                    _useItemButton.onClick.AddListener(delegate { slot.EquipItem(slot); Hide(); });
                    break;

                default:
                    break;
            }

            
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _useItemButton.onClick.RemoveAllListeners();
        _removeItem.onClick.RemoveAllListeners();
    }
}
