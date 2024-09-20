﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] ItemLevel _itemLevel;
    LevelStaticData[] _dataLevels;
    Button[] _arrItemLevels;
    Dictionary<Button, Image> _dictButtonLevels = new();
    private static ProfilerMarker performanceMarker = new ProfilerMarker("ImprovedCode");

    // Start is called before the first frame update
    void Start()
    {
        LoadDataItemLevel();
        SetupDictionary();
        //performanceMarker.Begin();
        SetupForLevelButtons(_arrItemLevels);
        //performanceMarker.End();
    }

    private void LoadDataItemLevel()
    {
        //load static & progress data lên
        _dataLevels = Resources.LoadAll<LevelStaticData>("Levels Data");
        if (_dataLevels != null)
        {
            foreach(var item in _dataLevels)
            {
                ItemLevel itemLevel = Instantiate(_itemLevel, transform);
                itemLevel.LvlSData = item;
                string jsonFIle = Application.dataPath + GameConstants.LEVEL_DATA_PATH + item.OrderID.ToString() + ".json";
                itemLevel.LvlPData = JSONDataHelper.LoadFromJSon<LevelProgressData>(jsonFIle);
            }
        }
    }

    private void SetupDictionary()
    {
        _arrItemLevels = GetComponentsInChildren<Button>();
        foreach (var item in _arrItemLevels)
            if (!_dictButtonLevels.ContainsKey(item))
                _dictButtonLevels.Add(item, item.GetComponent<Image>());
    }

    private void SetupForLevelButtons(Button[] buttons)
    {
        foreach (var button in buttons)
            button.onClick.AddListener(() => ButtonLevelOnClick(button));
    }

    private void ButtonLevelOnClick(Button buttonClicked)
    {
        foreach (var item in _dictButtonLevels)
            item.Value.color = new(0.45f, 0.45f, 0.45f, 1f);

        _dictButtonLevels[buttonClicked].color = new(1f, 1f, 1f, 1f);
    }
}
