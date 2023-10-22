﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateManager : MonoBehaviour
{
    protected BaseState state;
    //protected Animator anim;
    protected SpriteRenderer sprite;  //use for control sprite

    protected virtual void Start()
    {
        //anim = GetComponent<Animator>(); 
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        state.UpdateState();
    }

    private void FixedUpdate()
    {
        state.FixedUpdate();
    }

    public virtual void ChangeState(BaseState state)
    {
        this.state.ExitState();
        this.state = state;
        state.EnterState(this);
    }

    //public Animator GetAnimator() { return this.anim; }

    public SpriteRenderer GetSpriteRenderer() { return this.sprite; }
}
