using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static event Action OnGameOver;

    [SerializeField] private float time;
    [SerializeField] private TextMeshProUGUI _timerText;

    private float _timeLeft = 0f;

    public void Initialization()
    {
        _timeLeft = time;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (_timeLeft > 0f)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
        OnGameOver?.Invoke();
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0f)
        {
            _timeLeft = 0f;
        }
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        _timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
