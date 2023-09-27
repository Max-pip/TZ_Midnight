using UnityEngine;

[CreateAssetMenu(fileName = "DetailSkinItem", menuName = "Shop/DetailSkinItem")]
public class DetailSkinItem : ShopItem
{
    [field: SerializeField] public DetailSkins SkinType { get; private set; }
}
