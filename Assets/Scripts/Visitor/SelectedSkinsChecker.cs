public class SelectedSkinChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsSelected { get; private set; }

    public SelectedSkinChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkinItem)
        => IsSelected = _persistentData.PlayerData.SelectedCharacterSkin == characterSkinItem.SkinType;

    public void Visit(DetailSkinItem mazeSkinItem)
        => IsSelected = _persistentData.PlayerData.SelectedDetailSkin == mazeSkinItem.SkinType;
}
