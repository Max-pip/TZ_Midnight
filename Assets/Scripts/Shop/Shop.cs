using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;

    [SerializeField] private ShopCategoryButton _characterSkinsButton;
    [SerializeField] private ShopCategoryButton _detailSkinsButton;

    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private Button _selectionButton;
    [SerializeField] private Image _selectedText;

    [SerializeField] private ShopPanel _shopPanel;

    [SerializeField] private SkinPlacement _skinPlacement;

    [SerializeField] private Camera _modelCamera;
    [SerializeField] private float _sizeCharacterSkins;
    [SerializeField] private float _sizeDetailSkins;

    private IDataProvider _dataProvider;

    private ShopItemView _previewedItem;

    private Wallet _wallet;

    private SkinSelector _skinSelector;
    private SkinUnlocker _skinUnlocker;
    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;
    
    private void OnEnable()
    {
        _characterSkinsButton.Click += OnCharacterSkinsButtonClick;
        _detailSkinsButton.Click += OnDetailSkinsButtonClick;

        _buyButton.Click += OnBuyButtonClick;
        _selectionButton.onClick.AddListener(OnSelectionButtonClick);
    }

    private void OnDisable()
    {
        _characterSkinsButton.Click -= OnCharacterSkinsButtonClick;
        _detailSkinsButton.Click -= OnDetailSkinsButtonClick;
        _shopPanel.ItemViewClicked -= OnItemViewClicked;

        _buyButton.Click -= OnBuyButtonClick;
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
    }
    
    public void Initialize(IDataProvider dataProvider, Wallet wallet, OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker)
    {
        _wallet = wallet;
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinChecker = selectedSkinChecker;
        _skinSelector = skinSelector;
        _skinUnlocker = skinUnlocker;

        _dataProvider = dataProvider;

        _shopPanel.Initialize(openSkinsChecker, selectedSkinChecker);

        _shopPanel.ItemViewClicked += OnItemViewClicked;

        OnCharacterSkinsButtonClick();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _dataProvider.Delete();
        }
    }

    private void OnItemViewClicked(ShopItemView item)
    {
        _previewedItem = item;
        _skinPlacement.InstantiateModel(_previewedItem.Model);

        _openSkinsChecker.Visit(_previewedItem.Item);

        if (_openSkinsChecker.IsOpened)
        {
            _selectedSkinChecker.Visit(_previewedItem.Item);

            if (_selectedSkinChecker.IsSelected)
            {
                ShowSelectedText();
                return;
            }

            ShowSelectionButton();
        }
        else
        {
            ShowBuyButton(_previewedItem.Price);
        }
    }
    
    
    private void OnBuyButtonClick()
    {
        if (_wallet.IsEnough(_previewedItem.Price))
        {
            _wallet.Spend(_previewedItem.Price);

            _skinUnlocker.Visit(_previewedItem.Item);

            SelectSkin();

            _previewedItem.Unlock();

            _dataProvider.Save();
        }
    }
    
    
    private void OnSelectionButtonClick()
    {
        SelectSkin();

        _dataProvider.Save();
    }
    

    private void OnDetailSkinsButtonClick()
    {
        _detailSkinsButton.Select();
        _characterSkinsButton.Unselect();

        UpdateCameraSize(_sizeDetailSkins);

        _shopPanel.Show(_contentItems.DetailSkinItems.Cast<ShopItem>());
    }

    private void OnCharacterSkinsButtonClick()
    {
        _detailSkinsButton.Unselect();
        _characterSkinsButton.Select();

        UpdateCameraSize(_sizeCharacterSkins);

        _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>());
    }
    
    private void UpdateCameraSize(float newSize)
    {
        _modelCamera.orthographicSize = newSize;
    }
    
    
    private void SelectSkin()
    {
        _skinSelector.Visit(_previewedItem.Item);
        _shopPanel.Select(_previewedItem);
        ShowSelectedText();
    }
    

    
    private void ShowSelectionButton()
    {
        _selectionButton.gameObject.SetActive(true);
        HideBuyButton();
        HideSelectedText();
    }

    private void ShowSelectedText()
    {
        _selectedText.gameObject.SetActive(true);
        HideSelectionButton();
        HideBuyButton();
    }
    
    
    private void ShowBuyButton(int price)
    {
        _buyButton.gameObject.SetActive(true);
        _buyButton.UpdateText(price);

        if (_wallet.IsEnough(price))
            _buyButton.Unlock();
        else
            _buyButton.Lock();

        HideSelectedText();
        HideSelectionButton();
    }
    

    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
    private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);
    private void HideSelectedText() => _selectedText.gameObject.SetActive(false);
}
