using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI catcherScoreText;
    [SerializeField] private TextMeshProUGUI pitcherScoreText;
    private int _catcher;
    private int _pitcher;

    private void Start()
    {
        catcherScoreText.text = $"Catchers: {PlayerPrefs.GetInt("cathcerScoreValue",0)}";
        pitcherScoreText.text = $"Pitchers: {PlayerPrefs.GetInt("pitcherScoreValue",0)}";
        
    }

    private void OnEnable()
    {
        Events.OnBallAtLastBaseCatcher.AddListener(UpdateCatcherScore);
        Events.OnPitcherAtLastBase.AddListener(UpdatePitcherScore);
    }
    private void OnDisable()
    {
        Events.OnBallAtLastBaseCatcher.RemoveListener(UpdateCatcherScore);
        Events.OnPitcherAtLastBase.RemoveListener(UpdatePitcherScore);
    }
    private void UpdateCatcherScore()
    {
        _catcher = PlayerPrefs.GetInt("cathcerScoreValue",0);
        PlayerPrefs.SetInt("cathcerScoreValue", _catcher + 1);
        catcherScoreText.text = $"Catchers: {PlayerPrefs.GetInt("cathcerScoreValue",0)}";
    }
    private void UpdatePitcherScore()
    {
        _pitcher = PlayerPrefs.GetInt("pitcherScoreValue",0);
        PlayerPrefs.SetInt("pitcherScoreValue", _pitcher + 1);
        pitcherScoreText.text = $"Pitchers: {PlayerPrefs.GetInt("pitcherScoreValue",0)}";
    }
}
