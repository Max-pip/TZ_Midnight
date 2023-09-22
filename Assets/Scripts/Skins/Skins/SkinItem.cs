using UnityEngine;

[CreateAssetMenu(fileName = "SkinItem", menuName = "Shop/SkinItem")]
public class SkinItem : ShopItem
{
    [field: SerializeField] public Skins SkinType { get; private set; }
}
