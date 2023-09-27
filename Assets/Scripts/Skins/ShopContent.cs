using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<CharacterSkinItem> _characterSkinItems;
    [SerializeField] private List<DetailSkinItem> _detailSkinItems;

    public IEnumerable<CharacterSkinItem> CharacterSkinItems => _characterSkinItems;
    public IEnumerable<DetailSkinItem> DetailSkinItems => _detailSkinItems;
}
