using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private CharacterFactory _characterFactory;
    [SerializeField] private DetailFactory _detailFactory;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private UIManager _uiManager;

    private Wallet _wallet;
    private CarController _character;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    private void Awake()
    {
        InitializeData();

        InitializeWallet();

        SpawnCharacter();

        _cameraFollow.Initialization(_character.transform, _character.Rigidbody);

        _uiManager.Initialization(_character, _wallet, _dataProvider);
    }


    private void InitializeData()
    {
        _persistentPlayerData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentPlayerData);

        LoadDataOrInit();
    }

    private void InitializeWallet()
    {
        _wallet = new Wallet(_persistentPlayerData);
    }

    private void SpawnCharacter()
    {
        _character = _characterFactory.Get(_persistentPlayerData.PlayerData.SelectedCharacterSkin);
        GameObject detail = _detailFactory.Get(_persistentPlayerData.PlayerData.SelectedDetailSkin, _character.DetailSpawn.position, _character.DetailSpawn);
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentPlayerData.PlayerData = new PlayerData();
    }
}
