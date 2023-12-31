﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHPController : ItemsController //Tạo class Item
{
    [Header("Parent")]
    [SerializeField] private GameObject _parent;

    [Header("Boundaries")]
    [SerializeField] private Transform _topPos;
    [SerializeField] private Transform _botPos;

    [Header("Speed")]
    [SerializeField] private float _speedY;

    [Header("Collected Effect")]
    [SerializeField] private Transform _collectedEffect;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= _topPos.position.y)
        {
            transform.position = new Vector3(transform.position.x, _topPos.position.y, transform.position.z);
            _speedY = -_speedY;
        }
        else if (transform.position.y <= _botPos.position.y)
        {
            transform.position = new Vector3(transform.position.x, _botPos.position.y, transform.position.z);
            _speedY = -_speedY;
        }
        transform.position += new Vector3(0f, _speedY, 0f) * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == GameConstants.PLAYER_NAME)
        {
            SpawnEffect();
            PlayerHealthController.Instance.ChangeHPState(GameConstants.HP_STATE_NORMAL);
            SoundsManager.Instance.GetTypeOfSound(GameConstants.COLLECT_HP_SOUND).Play();
            Destroy(_parent);
        }
    }

    protected override void SpawnEffect()
    {
        GameObject collectEff = EffectPool.Instance.GetObjectInPool(GameConstants.COLLECT_HP_EFFECT);
        collectEff.SetActive(true);
        collectEff.GetComponent<EffectController>().SetPosition(transform.position);
    }
}
