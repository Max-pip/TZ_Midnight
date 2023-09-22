using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private UpdateScoreText _updateScoreText;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameOver _gameOver;

    private void Start()
    {
        _gameTimer.gameObject.SetActive(true);
        _gameTimer.Initialization();
        _updateScoreText.gameObject.SetActive(true);
        _updateScoreText.Initialization();
        _playerInput.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(false);

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
        _gameOver.Initialization();
    }
    
}
