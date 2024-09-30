﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameEnums;
using DG.Tweening;
using Unity.Profiling;
using static GameConstants;

public class HUDController : MonoBehaviour
{
    //hp, time, coins, icons
    //chuyển đống này thành InGameUICanvas

    [SerializeField] TextMeshProUGUI _txtTimer;
    [SerializeField, Tooltip("Delay bộ đếm 1 khoảng nhỏ" +
        " lấy tương đối gần = thgian quá trình chuyển scene")] int _delayCount;
    [SerializeField] TweenCoin _tweenCoin;
    int _timeLeft, _timeAllow = 0, _bonusTime = 0;
    Tween _timerTween;
    LevelInfo _levelInfo;
    private static ProfilerMarker performanceMarker = new ProfilerMarker("ImprovedCode");

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.Instance.SubcribeToAnEvent(EEvents.OnSetupLevel, SetupTimer);
        EventsManager.Instance.SubcribeToAnEvent(EEvents.OnReturnMainMenu, KillTweenTimer);
        EventsManager.Instance.SubcribeToAnEvent(EEvents.OnResetLevel, HandleReset);
    }

    private void OnDestroy()
    {
        EventsManager.Instance.UnSubcribeToAnEvent(EEvents.OnSetupLevel, SetupTimer);
        EventsManager.Instance.UnSubcribeToAnEvent(EEvents.OnReturnMainMenu, KillTweenTimer);
        EventsManager.Instance.UnSubcribeToAnEvent(EEvents.OnResetLevel, HandleReset);
    }

    /*private void Update()
    {
        performanceMarker.Begin();
        if (Time.time - _timer >= 1f)
        {
            _count--;
            _timer = Time.time;
            TimeDisplayHelper.DisplayTime(ref _txtTimer, _timeLeft, 10);
        }
        performanceMarker.End();
    }*/

    private void SetupTimer(object obj)
    {
        //performanceMarker.Begin();
        LevelInfo info = (LevelInfo)obj;
        _levelInfo = info;
        _bonusTime = info.ListActiveSkills.Find(x => x.SkillName == ESkills.Hourglass) != null ? HOURGLASS_BONUS_TIME : 0;
        _timeLeft = _timeAllow = info.LevelTimeAllow + _bonusTime;
        TimeDisplayHelper.DisplayTime(ref _txtTimer, _timeLeft, _timeAllow);
        //performanceMarker.End();
    }

    public void Countdown()
    {
        _timerTween = DOTween.To(() => _timeLeft, x => _timeLeft = x, 0, _timeLeft).OnUpdate(() =>
        {
            TimeDisplayHelper.DisplayTime(ref _txtTimer, _timeLeft, _timeAllow);
        }).SetEase(Ease.Linear).OnComplete(() =>
        {
            //truyen them tham so fail hay win
            int timeComplete = _timeAllow - _timeLeft;
            ResultParam pr = new(ELevelResult.Failed, _tweenCoin.SCoinCollected, _tweenCoin.GCoinCollected, timeComplete, _timeAllow);
            UIManager.Instance.TogglePopup(EPopup.Result, true);
            EventsManager.Instance.NotifyObservers(EEvents.OnLockLimitedSkills);
            EventsManager.Instance.NotifyObservers(EEvents.OnFinishLevel, pr);
        });
        Debug.Log("start Count");
    }

    private void KillTweenTimer(object obj)
    {
        _timerTween.Kill();
        _bonusTime = 0;
        EventsManager.Instance.NotifyObservers(EEvents.OnLockLimitedSkills);
    }

    private void HandleReset(object obj)
    {
        _tweenCoin.ResetCoins();

        //TH replay level, có mua cgi
        //thì gọi cái list active skill
        //r tìm trong đó cái skill cần
        List<Skills> listActiveSkills = ToggleAbilityItemHelper.GetListActivatedSkills();
        _bonusTime = listActiveSkills.Find(x => x.SkillName == ESkills.Hourglass) != null ? HOURGLASS_BONUS_TIME : 0;
        _timeLeft = _timeAllow = _levelInfo.LevelTimeAllow + _bonusTime;
        TimeDisplayHelper.DisplayTime(ref _txtTimer, _timeAllow, _timeAllow);

        Debug.Log("Reset");
    }
}
