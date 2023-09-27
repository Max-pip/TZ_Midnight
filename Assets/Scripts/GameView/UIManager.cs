using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private UpdateScoreText _updateScoreText;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameOver _gameOver;

    private Wallet _wallet;
    private IDataProvider _dataProvider;

    public void Initialization(CarController carController, Wallet wallet, IDataProvider dataProvider)
    {
        _gameTimer.gameObject.SetActive(true);
        _gameTimer.Initialization();

        _updateScoreText.gameObject.SetActive(true);
        _updateScoreText.Initialization(carController);

        _playerInput.Initialization(carController);
        _playerInput.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(false);

        _wallet = wallet;
        _dataProvider = dataProvider;
    }
    
    private void OnEnable()
    {
        GameTimer.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GameTimer.OnGameOver -= GameOver;
    }

    private void GameOver()
    {
        _gameTimer.gameObject.SetActive(false);
        _updateScoreText.gameObject.SetActive(false);
        _playerInput.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(true);
        _gameOver.Initialization(_wallet, _dataProvider);
    }
    
}
