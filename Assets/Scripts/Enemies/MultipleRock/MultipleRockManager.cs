﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleRockManager : MEnemiesManager
{
    //Con vk này có 3 form: Big - Medium - Tiny
    //Add effect cho nó sau
    //Add effect = particle system ?
    [Tooltip("0: Big|1: Medium|2: Tiny")]
    [Header("Type and Rock Clone"), Range(0, 2)]
    [SerializeField] private int _type;
    [SerializeField] private GameObject _rockClone;

    [Header("SpawnPos")]
    [SerializeField] private Transform _spawnPos1;
    [SerializeField] private Transform _spawnPos2;
    [SerializeField] private Transform _spawnPos3;
    [SerializeField] private Transform _spawnPos4;

    [Header("Effect")]
    [SerializeField] private GameObject _deadEffect;

    public void SetIsFacingRight(bool isFacingRight) { this._isFacingRight = isFacingRight; }

    protected override void Awake()
    {
        base.Awake(); 
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected virtual void SpawnClone()
    {
        if (_type != 2)
        {
            GameObject brownEff = EffectPool.Instance.GetObjectInPool(GameConstants.BROWN_EXPLOSION);
            brownEff.SetActive(true);
            brownEff.GetComponent<EffectController>().SetPosition(transform.position);
        }

        switch(_type)
        {
            case 0:
                GameObject medRock1 = Instantiate(_rockClone, _spawnPos1.transform.position, Quaternion.identity, null);
                Instantiate(_rockClone, _spawnPos2.transform.position, Quaternion.identity, null);
                medRock1.GetComponent<MultipleRockManager>().FlippingSprite();
               // medRock2.GetComponent<BigRockManager>().SetIsFacingRight(false);*/
                break;
            case 1:
                GameObject tinyRock1 = Instantiate(_rockClone, _spawnPos1.transform.position, Quaternion.identity, null);
                Instantiate(_rockClone, _spawnPos2.transform.position, Quaternion.identity, null);
                tinyRock1.GetComponent<MultipleRockManager>().FlippingSprite();
                break;
        }
        Destroy(this.gameObject);
    }
}
