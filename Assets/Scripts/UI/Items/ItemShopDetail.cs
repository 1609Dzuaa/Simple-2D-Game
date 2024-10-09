using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameEnums;

public class ItemShopDetail : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtName;
    [SerializeField] Image _imgItem;
    [SerializeField] TextMeshProUGUI _txtDescribe;
    ItemShop _itemShop;

    // Start is called before the first frame update
    void Awake()
    {
        EventsManager.Instance.SubcribeToAnEvent(EEvents.ShopItemOnClick, ShowDetail);
    }

    private void OnDestroy()
    {
        EventsManager.Instance.UnSubcribeToAnEvent(EEvents.ShopItemOnClick, ShowDetail);
    }

    private void ShowDetail(object obj)
    {
        _itemShop = (ItemShop)obj;
        _txtName.text = _itemShop.ItemSData.ItemName;
        _imgItem.sprite = _itemShop.ItemSData.ItemImage;
        _txtDescribe.text = _itemShop.ItemSData.ItemDescribe;
    }

    public void ButtonBuyOnClick()
    {
        EventsManager.Instance.NotifyObservers(EEvents.PlayerOnBuyShopItem, _itemShop);
    }
}