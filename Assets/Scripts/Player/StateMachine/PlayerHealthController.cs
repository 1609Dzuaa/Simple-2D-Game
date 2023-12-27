﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct HP
{
    public int _state;
    public Sprite _HPSprite;
    public Dictionary<int, Sprite> _dictHP; //Key là state, tương ứng là sprite của state đó
}

public class PlayerHealthController : MonoBehaviour
{
    //Class này dùng Quản lý HP và phụ trách render HP lên UI

    [Header("HP Icon")]
    [SerializeField] private Image[] _uiHP = new Image[10];
    [SerializeField] private Sprite _normalHPSprite;
    [SerializeField] private Sprite _lostHPSprite;
    [SerializeField] private Sprite _tempHPSprite;

    [Header("SO")]
    [SerializeField] private PlayerStats _playerSO;

    private static PlayerHealthController _Instance;
    private HP[] _HPs = new HP[7];
    private int _maxHP;
    private int _currentHP;
    private int _tempHP;

    public Image[] UIHPs { get { return _uiHP; } set { _uiHP = value; } }

    public HP[] HPs { get { return _HPs; } set { HPs = value; } }

    public int MaxHP { get { return _maxHP; } }

    public int CurrentHP { get { return _currentHP; } }

    public int TempHP { get { return _tempHP; } set { _tempHP = value; } }

    public static PlayerHealthController Instance
    {
        get
        {
            if (!_Instance)
            {
                //Tìm xem có Instance có trong Scene kh ?
                _Instance = FindObjectOfType<PlayerHealthController>();

                if (!_Instance)
                    Debug.Log("No HP Controller in scene");
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        _maxHP = _playerSO.MaxHP;
        _currentHP = _maxHP;
        _tempHP = 0;
        InitHPArray();
        InitHPDictionary();
        InitUIHP();
    }

    private void CreateInstance()
    {
        if (!_Instance)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject); //Đảm bảo thằng này 0 bị huỷ khi chuyển Scene
            //Docs: Don't destroy the target Object when loading a new Scene.
        }
        else
        {
            //Nếu đã tồn tại thằng Instance != ở trong game thì destroy thằng này
            Destroy(gameObject);
        }
    }

    private void InitHPDictionary()
    {
        for (int i = 0; i < _maxHP; i++)
        {
            _HPs[i]._dictHP = new Dictionary<int, Sprite>()
            {
                {GameConstants.HP_STATE_NORMAL, _normalHPSprite },
                {GameConstants.HP_STATE_LOST, _lostHPSprite },
                {GameConstants.HP_STATE_TEMP, _tempHPSprite }
            };        
        }
        //Thiết lập từng key - value cho từng HP
    }

    private void InitUIHP()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i < _maxHP)
                _uiHP[i].enabled = true;
            else
                _uiHP[i].enabled = false;
        }
        //Enable những thằng có index < maxHP
        //Disable những thằng còn lại
    }

    private void InitHPArray()
    {
        for (int i = 0; i < _maxHP; i++)
        {
            _HPs[i]._state = GameConstants.HP_STATE_NORMAL;
            //Khởi tạo cho các HP mặc định là state Normal
        }
    }

    private void Update()
    {
        UpdateHPToUI();
        Debug.Log("temp: " + _tempHP);
        //Debug.Log("Curr, TempHP , state: " + _currentHP + ", " + _tempHP + "," + _HPs[_tempHP]._state);
    }

    public void ChangeHPState(int state)
    {
        HandleIfIncreaseHP(state);
        HandleIfDecreaseHP(state);
        HandleIfIncreaseTempHP(state);
        
        //Buff hấp thụ thì 0 cần quan tâm maxHP, cộng cho dư luôn
        //GH thì state thành Lost
        //Gọi thằng này thông qua Instance mỗi khi có sự thay đổi về HP
        //(GH, Ăn HP, Ăn TempHP)
        //Debug.Log("Cur HP: " + _currentHP);
    }

    private void HandleIfIncreaseHP(int state)
    {
        if (state == GameConstants.HP_STATE_NORMAL)
        {
            _HPs[_currentHP]._state = state;
            if (_currentHP < _maxHP)
                _currentHP++;
        }
        //Func này ổn r
    }

    private void HandleIfDecreaseHP(int state)
    {
        if (state == GameConstants.HP_STATE_LOST)
        {
            //Check có máu ảo thì trừ nó, 0 thì trừ thẳng hp hiện tại
            if (_tempHP != 0)
            {
                if (_tempHP + _currentHP > _maxHP)
                {
                    //Minus 1 bcuz it's a array
                    //Nếu tổng máu ảo + máu real vượt quá maxHP thì disable máu ảo 
                    _uiHP[_tempHP + _currentHP - 1].enabled = false;
                    _tempHP--;
                }
                else
                {
                    //Nếu tổng máu ảo + máu real <= maxHP thì set cái state của máu ảo về LOST 
                    _HPs[_tempHP + _currentHP - 1]._state = state;
                    _tempHP--;
                }
            }
            else
            {
                //Tại sao trừ trước r mới gán state
                //=> vì là mảng nên index từ 0 (obviously!) nên phải trừ trước khi gán
                //vd: máu hiện tại là 3 tương ứng với index là 2
                if (_currentHP > 0)
                    _currentHP--;
                _HPs[_currentHP]._state = state;
            }
            //Debug.Log("current, state: " + _currentHP + ", " + _HPs[_currentHP]._state);
        }
    }

    private void HandleIfIncreaseTempHP(int state)
    {
        if (state == GameConstants.HP_STATE_TEMP)
        {
            //Set state máu ảo cho các HP từ máu hiện tại trở đi
            _HPs[_currentHP + _tempHP]._state = state;
            _tempHP++;

            //Sau khi đã tăng máu ảo thì check nếu tổng máu hiện tại + máu ảo >= maxHP
            //thì cho phép các thanh máu sau maxHP enable
            if (_tempHP + _currentHP >= _maxHP)
                _uiHP[_tempHP + _currentHP - 1].enabled = true;
        }
        //Ổn
    }

    private void UpdateHPToUI()
    {
        //Tách 2 phần, Lượng máu đc hiển thị trên UI tính theo:
        //Máu hiện tại + máu ảo (nếu có):
        //1. Loop đến max, render từng phần máu tuỳ theo state của nó
        //2. Loop đến máu ảo hiện tại, render từng phần máu

        //Loop đến max
        for (int i = 0; i < _maxHP; i++)
        {
            _uiHP[i].sprite = _HPs[i]._dictHP[_HPs[i]._state];
            //Debug.Log("Curr, TempHP , Curr_state: " + _currentHP + ", " + _tempHP + "," + _HPs[_currentHP]._state);
        }

        //Loop đến Temp
        for (int i = 0; i < _tempHP; i++)
        {
            //Thêm dòng này vì có thể tempHP vượt quá max dẫn đến dictionary bị null
            if (_HPs[i + _currentHP]._dictHP == null)
            {
                _HPs[i + _currentHP]._dictHP = new Dictionary<int, Sprite>()
                {
                        { GameConstants.HP_STATE_NORMAL, _normalHPSprite },
                        { GameConstants.HP_STATE_LOST, _lostHPSprite },
                        { GameConstants.HP_STATE_TEMP, _tempHPSprite }
                };
            }
            //Debug.Log("Co vao day");
            _uiHP[i + _currentHP].sprite = _HPs[i + _currentHP]._dictHP[_HPs[i + _currentHP]._state];
            
            //Render lượng HP max lên sprite dựa trên state của nó 
        }
    }
}
