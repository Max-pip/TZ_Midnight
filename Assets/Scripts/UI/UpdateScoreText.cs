using System.Collections;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class UpdateScoreText : MonoBehaviour
{
    [SerializeField] private CarController _carController;
    [SerializeField] private TextMeshProUGUI _allScoreText;
    [SerializeField] private TextMeshProUGUI _updateScoreText;
    [SerializeField] private float _speedUpdate = 20f;
    [SerializeField] private float _scaleModifier = 1.5f;
    [SerializeField] private float _scaleChangeDuration = 0.1f;
    [SerializeField] private float _fadeDuration = 0.05f;
    private int _updateScore = 0;
    public int AllScore { get; private set; }

    public void Initialization()
    {
        _updateScoreText.alpha = 0;
    }

    private void OnEnable()
    {
        _carController.OnScoreUpdate += UpdateCoroutine;
    }

    private void OnDisable()
    {
        _carController.OnScoreUpdate -= UpdateCoroutine;
    }

    private void UpdateCoroutine()
    {
        StartCoroutine(UpdateScoreCoroutine());
    }

    private IEnumerator UpdateScoreCoroutine()
    {
        _updateScoreText.DOFade(1f, _fadeDuration);
        _updateScore = 0;
        float countValue = 0;

        while (_carController.IsSlip)
        {
            countValue += Time.deltaTime * _speedUpdate;
            _updateScore = Mathf.RoundToInt(countValue);
            _updateScoreText.SetText(_updateScore.ToString());
            yield return null;
        }

        AllScore += _updateScore;
        _allScoreText.SetText(AllScore.ToString());
        Vector3 originalScale = _updateScoreText.transform.localScale;
        Vector3 originalAllScoreScale = _allScoreText.transform.localScale;
        AnimationUpdateScoreText(originalScale);
        AnimationAllScoreText(originalAllScoreScale);
        _carController.IsCanInvokeEvent = true;
    }

    private void AnimationUpdateScoreText(Vector3 originalScale)
    {
        _updateScoreText.transform.DOScale(originalScale * _scaleModifier, _scaleChangeDuration)
        .OnComplete(() =>
        {
            _updateScoreText.transform.DOScale(originalScale, _scaleChangeDuration)
            .OnComplete(() =>
            {
                _updateScoreText.DOFade(0f, _fadeDuration);
            });
        });
    }

    private void AnimationAllScoreText(Vector3 originalScale)
    {
        _allScoreText.transform.DOScale(originalScale * _scaleModifier, _scaleChangeDuration)
        .OnComplete(() =>
        {
            _allScoreText.transform.DOScale(originalScale, _scaleChangeDuration);
        });
    }
}
