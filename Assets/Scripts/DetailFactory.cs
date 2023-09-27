using System;
using UnityEngine;

[CreateAssetMenu( fileName = "DetailFactory", menuName = "Game/DetailFactory")]
public class DetailFactory : ScriptableObject
{
    [SerializeField] private GameObject _default;
    [SerializeField] private GameObject _dino;
    [SerializeField] private GameObject _dog;
    [SerializeField] private GameObject _horse;
    [SerializeField] private GameObject _goblin;
    [SerializeField] private GameObject _figure;
    [SerializeField] private GameObject _officer;

    public GameObject Get(DetailSkins skinType, Vector3 spawnPosition, Transform parent)
    {
        GameObject instance = Instantiate(GetPrefab(skinType), spawnPosition, Quaternion.identity, parent);
        return instance;
    }

    private GameObject GetPrefab(DetailSkins skinType)
    {
        switch (skinType)
        {
            case DetailSkins.Default:
                return _default;
            case DetailSkins.Dino:
                return _dino;
            case DetailSkins.Dog:
                return _dog;
            case DetailSkins.Horse:
                return _horse;
            case DetailSkins.Goblin:
                return _goblin;
            case DetailSkins.Figure:
                return _figure;
            case DetailSkins.Officer:
                return _officer;

            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}
