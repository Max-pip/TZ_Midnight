using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _modelItemPrefab;
    [SerializeField] private ShopItemView _skinItemPrefab;

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemView instance;

        switch (shopItem)
        {
            case ModelItem modelItem:
                instance = Instantiate(_modelItemPrefab, parent);
                break;

            case SkinItem skinItem:
                instance = Instantiate(_skinItemPrefab, parent);
                break;

            default:
                throw new ArgumentException(nameof(shopItem));
        }

        instance.Initialization(shopItem);
        return instance;
    }
}
