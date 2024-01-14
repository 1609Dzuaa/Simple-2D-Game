﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MEnemiesManager : EnemiesManager
{
    //MEnemies <-> MoveableEnemies
    protected MEnemiesIdleState _mEnemiesIdleState = new();
    protected MEnemiesPatrolState _mEnemiesPatrolState = new();
    protected MEnemiesAttackState _mEnemiesAttackState = new();
    protected MEnemiesGotHitState _mEnemiesGotHitState = new();

    [Header("SO2")]
    [SerializeField] protected MEnemiesStats _mEnemiesSO;

    [Header("Wall Check")]
    [SerializeField] protected Transform _wallCheck;
    protected bool _hasCollidedWall;

    //Public Field

    public MEnemiesIdleState MEnemiesIdleState { get { return _mEnemiesIdleState; } }
    
    public MEnemiesPatrolState MEnemiesPatrolState { get { return _mEnemiesPatrolState;} set { _mEnemiesPatrolState = value; } }

    public MEnemiesAttackState MEnemiesAttackState { get { return _mEnemiesAttackState; } set { _mEnemiesAttackState = value; } }

    public MEnemiesGotHitState GetMEnemiesGotHitState { get { return _mEnemiesGotHitState; } set { _mEnemiesGotHitState = value; } }

    public bool HasCollidedWall { get { return _hasCollidedWall; } }

    public MEnemiesStats MEnemiesSO { get { return _mEnemiesSO; } }

    protected override void Awake()
    {
        base.Awake(); //Lấy anim, rb từ EnemiesManager
    }

    //Các lớp sau muốn mở rộng hàm Start, Update,... thì nhớ gọi base.Start(),... trong hàm Start của chính nó
    //Còn 0 implement gì thêm thì 0 cần làm gì, nó tự động đc gọi trong đây ngay cả khi là private
    protected override void Start()
    {
        base.Start();
    }

    protected override void SetUpProperties()
    {
        base.SetUpProperties();
        _state = _mEnemiesIdleState;
        _state.EnterState(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); //Đã bao gồm việc Update state và detect Player trong đây
        DetectWall();
        //Debug.Log("Hit Wall: " + _hasCollidedWall);
    }

    protected virtual void FixedUpdate()
    {
        _state.FixedUpdate();
        //Debug.Log("Called");
    }

    protected virtual void DetectWall()
    {
        if (!_isFacingRight)
            _hasCollidedWall = Physics2D.Raycast(new Vector2(_wallCheck.position.x, _wallCheck.position.y), Vector2.left, _mEnemiesSO.WallCheckDistance, _mEnemiesSO.WallLayer);
        else
            _hasCollidedWall = Physics2D.Raycast(new Vector2(_wallCheck.position.x, _wallCheck.position.y), Vector2.right, _mEnemiesSO.WallCheckDistance, _mEnemiesSO.WallLayer);
    }

    //Hàm này dùng để Invoke khi detect ra Player
    protected virtual void AllowAttackPlayer()
    {
        if (BuffsManager.Instance.GetTypeOfBuff(GameEnums.EBuffs.Invisible).IsAllowToUpdate)
            return;

        ChangeState(_mEnemiesAttackState);
        //Debug.Log("Called");
        //Nhằm delay việc chuyển state Attack 
        //Tạo cảm giác enemies phản ứng rồi attack chứ 0 phải attack ngay lập tức
    }

    protected virtual void ChangeToIdle()
    {
        ChangeState(_mEnemiesIdleState);
        //Dùng để Invoke trong state Attack nếu 0 detect ra player
        //Với Mushroom thì 0 detect ra ở sau lưng
        //Với Rhino thì 0 detect ra trước mặt
        //Với Bat thì là Event của Animation CeilOut
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.collider.CompareTag(GameConstants.BULLET_TAG))
        {
            EventsManager.Instance.NotifyObservers(GameEnums.EEvents.BulletOnHit, GameConstants.BULLET_ID);
            ChangeState(_mEnemiesGotHitState);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(GameConstants.PLAYER_TAG) && !_hasGotHit && _state is not MEnemiesGotHitState)
        {
            _hasGotHit = true;
            EventsManager.Instance.NotifyObservers(GameEnums.EEvents.PlayerOnJumpPassive, null);
            ChangeState(_mEnemiesGotHitState);
        }
    }

    protected virtual void OnDestroy()
    {

    }

}
