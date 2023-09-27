public interface IShopItemVisitor
{
    void Visit(ShopItem shopItem);
    void Visit(CharacterSkinItem characterSkinItem);
    void Visit(DetailSkinItem detailSkinItem);
}
