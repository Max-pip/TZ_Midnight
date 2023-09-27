using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private AdManager _adManager;
    [SerializeField] private UpdateScoreText _updateScoreText;
    [SerializeField] private Button _multiplyRewardButton;

    [SerializeField] private TextMeshProUGUI _rewardValueText;

    private Wallet _wallet;
    private IDataProvider _dataProvider;

    public void Initialization(Wallet wallet, IDataProvider dataProvider)
    {
        _multiplyRewardButton.onClick.AddListener(ShowRewardVideo);
        _rewardValueText.text = $"Your reward {_updateScoreText.AllScore}";
        _wallet = wallet;
        _dataProvider = dataProvider;
        _wallet.AddCoins(_updateScoreText.AllScore);
        _dataProvider.Save();
    }

    private void OnEnable()
    {
        _adManager.OnGetReward += MultiplyReward;
    }

    private void OnDisable()
    {
        _adManager.OnGetReward -= MultiplyReward;
    }

    private void ShowRewardVideo()
    {
        _adManager.ShowRewardedAd();
    }

    private void MultiplyReward()
    {
        _rewardValueText.text = $"Your reward {_updateScoreText.AllScore * 2}";
        _wallet.AddCoins(_updateScoreText.AllScore);
        _dataProvider.Save();
    }
}
