using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;

    [SerializeField] private ShopCategoryButton _modelButton;
    [SerializeField] private ShopCategoryButton _skinButton;

    [SerializeField] private ShopPanel _shopPanel;

    private void OnEnable()
    {
        _modelButton.Click += OnModelsButtonClick;
        _skinButton.Click += OnSkinsButtonClick;
    }

    private void OnDisable()
    {
        _modelButton.Click -= OnModelsButtonClick;
        _skinButton.Click -= OnSkinsButtonClick;
    }

    private void OnModelsButtonClick()
    {
        _skinButton.Unselect();
        _modelButton.Select();
        _shopPanel.Show(_contentItems.ModelItems.Cast<ShopItem>());
    }

    private void OnSkinsButtonClick()
    {
        _skinButton.Select();
        _modelButton.Unselect();
        _shopPanel.Show(_contentItems.SkinItems.Cast<ShopItem>());
    }
}
