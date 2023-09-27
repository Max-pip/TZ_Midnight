using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersFactory", menuName = "Game/CharactersFactory")]
public class CharacterFactory : ScriptableObject
{
    [SerializeField] private CarController _toyota;
    [SerializeField] private CarController _renault;
    [SerializeField] private CarController _subaru;
    [SerializeField] private CarController _porsche;

    public CarController Get(CharacterSkins skinType, Vector3 spawnPosition)
    {
        CarController instance = Instantiate(GetPrefab(skinType), spawnPosition, Quaternion.identity, null);
        instance.Initialization();
        return instance;
    }

    private CarController GetPrefab(CharacterSkins skinType)
    {
        switch (skinType)
        {
            case CharacterSkins.Toyota:
                return _toyota;
            case CharacterSkins.Renault:
                return _renault;
            case CharacterSkins.Subaru:
                return _subaru;
            case CharacterSkins.Porsche:
                return _porsche;

            default: 
                throw new ArgumentException(nameof(skinType));
        }
    }
}
