using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _weightText;
    [SerializeField] private TMP_Text _defenceText;
    [SerializeField] private TMP_Text _healValueText;
    [SerializeField] private TMP_Text _useButtonText;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Button _removeItem;
    [SerializeField] private Button _useItemButton;
    [SerializeField] private GameObject _defStatsBox;
    [SerializeField] private GameObject _healStatsBox;
    private Button _backButton;

    private void Start()
    {
        _backButton = GetComponent<Button>();
        _backButton.onClick.AddListener(Hide);
    }

    public void Show(Item item)
    {
        if (item != null)
        {
            _defStatsBox.SetActive(false);
            _healStatsBox.SetActive(false);

            _weightText.text = $"{item.Weight.ToString()} Í„";
            _itemName.text = item.Name;
            _useButtonText.text = item.ActionButtonText;
            _itemIcon.sprite = item.Icon;
            gameObject.SetActive(true);

            if (item.ItemType == ItemType.Med)
            {
                _healStatsBox.SetActive(true);
                var medItem = item as MedItem;
                _healValueText.text = medItem.HealStrength.ToString();
            }
            else if (item.ItemType == ItemType.Equipment)
            {
                _defStatsBox.SetActive(true);
                var equipment = item as EquipmentItem;
                _defenceText.text = equipment.Defence.ToString();
            }

            _useItemButton.onClick.AddListener(delegate { item.Use(); Hide(); });
            _removeItem.onClick.AddListener(delegate { item.RemoveItem(); Hide(); });
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
