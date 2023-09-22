using UnityEngine;

[CreateAssetMenu(fileName = "ModelItem", menuName = "Shop/ModelItem")]
public class ModelItem : ShopItem
{
    [field: SerializeField] public Models SkinType { get; private set; }
}
