using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Button _closePopup;

    private void Start()
    {
        _closePopup.onClick.AddListener(Hide);
    }

    public void Show(Item item)
    {
        if (item != null)
        {
            _itemName.text = item.Name;
            _itemIcon.sprite = item.Icon;
            gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
