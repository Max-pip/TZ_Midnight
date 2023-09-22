using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<ModelItem> _modelItems;
    [SerializeField] private List<SkinItem> _skinItems;

    public IEnumerable<ModelItem> ModelItems => _modelItems;
    public IEnumerable<SkinItem> SkinItems => _skinItems;
}
