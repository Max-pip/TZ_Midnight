using System.Linq;

public class OpenSkinsChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkinItem)
        => IsOpened = _persistentData.PlayerData.OpenCharacterSkins.Contains(characterSkinItem.SkinType);

    public void Visit(DetailSkinItem detailSkinItem)
        => IsOpened = _persistentData.PlayerData.OpenDetailSkins.Contains(detailSkinItem.SkinType);
}
