using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerData
{
    private CharacterSkins _selectedCharacterSkin;
    private DetailSkins _selectedDetailSkin;

    private List<CharacterSkins> _openCharacterSkins;
    private List<DetailSkins> _openDetailSkins;

    private int _money;

    public PlayerData()
    {
        _money = 1000;

        _selectedCharacterSkin = CharacterSkins.Toyota;
        _selectedDetailSkin = DetailSkins.Default;

        _openCharacterSkins = new List<CharacterSkins>() { _selectedCharacterSkin };
        _openDetailSkins = new List<DetailSkins>() { _selectedDetailSkin };
    }

    [JsonConstructor]
    public PlayerData(int money, CharacterSkins selectedCharacterSkin, DetailSkins selectedDetailSkin,
        List<CharacterSkins> openCharacterSkins, List<DetailSkins> openDetailSkins)
    {
        Money = money;

        _selectedCharacterSkin = selectedCharacterSkin;
        _selectedDetailSkin = selectedDetailSkin;

        _openCharacterSkins = new List<CharacterSkins>(openCharacterSkins);
        _openDetailSkins = new List<DetailSkins>(openDetailSkins);
    }

    public int Money
    {
        get => _money;

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _money = value;
        }
    }

    public CharacterSkins SelectedCharacterSkin
    {
        get => _selectedCharacterSkin;
        set
        {
            if (_openCharacterSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedCharacterSkin = value;
        }
    }

    public DetailSkins SelectedDetailSkin
    {
        get => _selectedDetailSkin;
        set
        {
            if (_openDetailSkins.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            _selectedDetailSkin = value;
        }
    }

    public IEnumerable<CharacterSkins> OpenCharacterSkins => _openCharacterSkins;

    public IEnumerable<DetailSkins> OpenDetailSkins => _openDetailSkins;

    public void OpenCharacterSkin(CharacterSkins skin)
    {
        if (_openCharacterSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openCharacterSkins.Add(skin);
    }

    public void OpenDetailSkin(DetailSkins skin)
    {
        if (_openDetailSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openDetailSkins.Add(skin);
    }
}
