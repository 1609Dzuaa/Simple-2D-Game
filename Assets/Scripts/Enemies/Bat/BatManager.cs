﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManager : MEnemiesManager
{
    [Header("Sleep Postion")]
    [SerializeField] private Transform _sleepPos;

    [Header("Time2")]
    [SerializeField] private float _sleepTime;

    [Header("AttackRange")]
    [SerializeField] private float _attackRange;

    [Header("Boundaries")]
    [SerializeField] private Transform _boundaryLeft;
    [SerializeField] private Transform _boundaryRight;

    private BatSleepState _batSleepState = new();
    private BatCeilInState _batCeilInState = new();
    private BatCeilOutState _batCeilOutState = new();
    private BatIdleState _batIdleState = new();
    private BatPatrolState _batPatrolState = new();
    private BatFlyBackState _batFlyBackState = new();
    private BatAttackState _batAttackState = new();
    private BatRetreatState _batRetreatState = new();
    private BatGotHitState _batGotHitState = new();
    private float _distanceToPlayer;

    //Public Field
    public Transform SleepPos { get { return _sleepPos; } }

    public float SleepTime { get { return _sleepTime; } }

    public Transform BoundaryLeft { get { return _boundaryLeft; } }

    public Transform BoundaryRight { get { return _boundaryRight; } }

    public Transform Player { get { return _playerCheck; } }

    public BatCeilInState BatCeilInState { get { return _batCeilInState; } }

    public BatCeilOutState BatCeilOutState { get { return _batCeilOutState; } }

    public BatIdleState BatIdleState { get { return _batIdleState; } }

    public BatPatrolState BatPatrolState { get { return _batPatrolState; } }

    public BatFlyBackState BatFlyBackState { get => _batFlyBackState; }

    public BatAttackState BatAttackState { get { return _batAttackState; } }

    public BatRetreatState BatRetreatState { get { return _batRetreatState; } }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        _state = _batSleepState;
        _state.EnterState(this);
        MEnemiesGotHitState = _batGotHitState; //convert chứ 0 nó lại xài state GotHit chung
        _playerCheck = FindObjectOfType<PlayerStateManager>().transform;
    }

    protected override void Update()
    {
        _state.Update();
        DetectedPlayer();
        //Debug.Log("v: " + _rb.velocity);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override bool DetectedPlayer()
    {
        if (BuffsManager.Instance.GetTypeOfBuff(GameEnums.EBuffs.Invisible).IsAllowToUpdate)
            return _hasDetectedPlayer = false;

        _distanceToPlayer = Vector2.Distance(transform.position, _playerCheck.position);
        return _hasDetectedPlayer =  _distanceToPlayer <= _attackRange;
    }

    public void FlipLeft()
    {
        _isFacingRight = false;
        transform.Rotate(0, 180, 0);
    }

    public void FlipRight()
    {
        _isFacingRight = true;
        transform.Rotate(0, 180, 0);
    }

    private void Sleep()
    {
        ChangeState(_batSleepState);
        //Event của Ceil In animation nhằm chuyển state đi ngủ sau khi về tổ
    }

    protected override void ChangeToIdle()
    {
        _batCeilOutState.AllowIdle = true;
        //Event của Ceil Out animation nhằm chuyển state idle sau khi wake up
        //và tránh việc animation từ wake up => attack bị giữ nguyên
        //do 0 có khớp nối 2 animations đó
    }

    protected override void AllowAttackPlayer()
    {
        if (BuffsManager.Instance.GetTypeOfBuff(GameEnums.EBuffs.Invisible).IsAllowToUpdate)
            return;

        ChangeState(_batAttackState);
    }

}
