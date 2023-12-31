﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MEnemiesManager : EnemiesManager
{
    //MEnemies <-> MoveableEnemies
    private MEnemiesIdleState _mEnemiesIdleState = new();
    private MEnemiesPatrolState _mEnemiesPatrolState = new();
    private MEnemiesAttackState _mEnemiesAttackState = new();
    private MEnemiesGotHitState _mEnemiesGotHitState = new();

    //Chắc vứt boundaries và check dựa vào wall check ?
    [Header("Boundaries")]
    [SerializeField] protected Transform _boundaryLeft;
    [SerializeField] protected Transform _boundaryRight;

    [Header("Wall Check")]
    [SerializeField] protected Transform _wallCheck;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _wallLayer;
    protected bool _hasCollidedWall;

    [Header("Time")]
    [SerializeField] protected float _restTime;
    [SerializeField] protected float _patrolTime;

    [Header("Speed")]
    [SerializeField] protected Vector2 _patrolSpeed;
    [SerializeField] protected Vector2 _chaseSpeed;

    [Header("Parent")]
    [SerializeField] protected GameObject _parent;

    //Public Field

    public MEnemiesIdleState MEnemiesIdleState { get { return _mEnemiesIdleState; } }
    
    public MEnemiesPatrolState MEnemiesPatrolState { get { return _mEnemiesPatrolState;} set { _mEnemiesPatrolState = value; } }

    public MEnemiesAttackState MEnemiesAttackState { get { return _mEnemiesAttackState; } set { _mEnemiesAttackState = value; } }

    public MEnemiesGotHitState MEnemiesGotHitState { get { return _mEnemiesGotHitState; } set { _mEnemiesGotHitState = value; } }

    public float GetRestTime() { return _restTime; }

    public float GetPatrolTime() { return _patrolTime; }

    public bool HasCollidedWall { get { return _hasCollidedWall; } }

    public Vector2 GetPatrolSpeed() { return _patrolSpeed; }

    public Vector2 GetChaseSpeed() { return _chaseSpeed; }

    public Transform BoundaryLeft { get { return _boundaryLeft; } }

    public Transform BoundaryRight { get { return _boundaryRight; } }

    public void SetHasGotHit(bool para) { _hasGotHit = para; }

    protected override void Awake()
    {
        base.Awake(); //Lấy anim, rb từ EnemiesManager
    }

    //Các lớp sau muốn mở rộng hàm Start, Update,... thì nhớ gọi base.Start(),... trong hàm Start của chính nó
    //Còn 0 implement gì thêm thì 0 cần làm gì, nó tự động đc gọi trong đây ngay cả khi là private
    protected override void Start()
    {
        base.Start();
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

    //Call this in Patrol State
    public virtual void Move(float velo)
    {
        if (_isFacingRight)
            _rb.velocity = new Vector2(velo, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(-velo, _rb.velocity.y);
        //Debug.Log("Move");
    }

    protected virtual void DetectWall()
    {
        if (!_isFacingRight)
            _hasCollidedWall = Physics2D.Raycast(new Vector2(_wallCheck.position.x, _wallCheck.position.y), Vector2.left, _wallCheckDistance, _wallLayer);
        else
            _hasCollidedWall = Physics2D.Raycast(new Vector2(_wallCheck.position.x, _wallCheck.position.y), Vector2.right, _wallCheckDistance, _wallLayer);
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
            EventsManager.Instance.InvokeAnEvent(GameEnums.EEvents.EnemiesOnDie, GameConstants.BULLET_ID);
            ChangeState(_mEnemiesGotHitState);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == GameConstants.PLAYER_NAME && !_hasGotHit)
        {
            _hasGotHit = true;
            EventsManager.Instance.InvokeAnEvent(GameEnums.EEvents.EnemiesOnDie, GameConstants.PLAYER_ID);
            ChangeState(_mEnemiesGotHitState);
        }
    }

    protected virtual void OnDestroy()
    {

    }

    protected void DestroyParent()
    {
        Destroy(_parent);
        //Event của Got Hit Animation
    }

}
