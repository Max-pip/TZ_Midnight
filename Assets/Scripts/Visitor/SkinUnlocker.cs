public class SkinUnlocker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public SkinUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkinItem)
        => _persistentData.PlayerData.OpenCharacterSkin(characterSkinItem.SkinType);

    public void Visit(DetailSkinItem detailSkinItem)
        => _persistentData.PlayerData.OpenDetailSkin(detailSkinItem.SkinType);
}