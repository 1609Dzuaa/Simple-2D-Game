﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    //Private Field
    BaseState state;
    public IdleState idleState = new IdleState();
    public RunState runState = new RunState();
    public JumpState jumpState = new JumpState();
    public FallState fallState = new FallState();
    //public DashState dashState = new DashState();
    //public DeadState deadState = new DeadState();

    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    float dirX, dirY;
    //make sure only if the player is on ground then he can jump
    bool isOnGround;
    bool isDashing;
    int numOrange;

    [SerializeField] float vX, vY, dashSpeed;
    [SerializeField] Text txtCount;

    //Public Field
    public Rigidbody2D getRigidbody2D() { return this.rb; }

    //Tạo các enum của State để gán giá trị tương ứng cho Animations
    public enum EnumState { idle, walk, jump, fall, dash, dead }

    public float getvX() { return this.vX; }

    public float getvY() { return this.vY; }

    public float getDirX() { return this.dirX; }

    public float getDashSpeed() { return this.dashSpeed; }

    public float getDirY() { return this.dirY; }

    public bool getIsOnGround() { return this.isOnGround; }

    public void setIsOnGround(bool value) { this.isOnGround = value; }

    public bool getIsDashing() { return this.isDashing; }

    public Animator GetAnimator() { return this.anim; }

    public SpriteRenderer GetSpriteRenderer() { return this.sprite; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        numOrange = 0;
        state = idleState;
        state.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        state.UpdateState(this);
    }

    private void FixedUpdate()
    {
        state.FixedUpdate(this);
    }

    public void ChangeState(BaseState state)
    {
        this.state = state;
        state.EnterState(this);
    }

    void HandleInput()
    {
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isOnGround = true;
        //if (collision.collider.CompareTag("Spike"))
            //ChangeState(deadState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orange"))
        {
            numOrange++;
            txtCount.text = "Oranges: " + numOrange.ToString();
            Destroy(collision.gameObject);
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
