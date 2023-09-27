using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _characterSkinItemPrefab;
    [SerializeField] private ShopItemView _detailSkinItemPrefab;
    
    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(_characterSkinItemPrefab, _detailSkinItemPrefab);
        visitor.Visit(shopItem);

        ShopItemView instance = Instantiate(visitor.Prefab, parent);
        instance.Initialize(shopItem);

        return instance;
    }

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopItemView _characterSkinItemPrefab;
        private ShopItemView _detailSkinItemPrefab;

        public ShopItemVisitor(ShopItemView characterSkinItemPrefab, ShopItemView detailSkinItemPrefab)
        {
            _characterSkinItemPrefab = characterSkinItemPrefab;
            _detailSkinItemPrefab = detailSkinItemPrefab;
        }

        public ShopItemView Prefab { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinItem characterSkinItem) => Prefab = _characterSkinItemPrefab;

        public void Visit(DetailSkinItem detailSkinItem) => Prefab = _detailSkinItemPrefab;
    }
    
}
