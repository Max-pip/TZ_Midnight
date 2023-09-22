using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private AdManager _adManager;
    [SerializeField] private UpdateScoreText _updateScoreText;
    [SerializeField] private Button _multiplyRewardButton;

    [SerializeField] private TextMeshProUGUI _test1;
    [SerializeField] private TextMeshProUGUI _test2;

    public void Initialization()
    {
        _multiplyRewardButton.onClick.AddListener(ShowRewardVideo);
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
        _test1.SetText("Test");
        _test1.SetText((_updateScoreText.AllScore).ToString());
        _test2.SetText((_updateScoreText.AllScore * 2).ToString());
    }
}
