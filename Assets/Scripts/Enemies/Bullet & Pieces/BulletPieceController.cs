﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletPiecePair
{
    private GameObject _pair1;
    private GameObject _pair2;

    public BulletPiecePair(GameObject pair1, GameObject pair2)
    {
        _pair1 = pair1;
        _pair2 = pair2;
    }

    public GameObject Pair1 { get { return _pair1; } }

    public GameObject Pair2 { get { return _pair2; } }
}

public class BulletPieceController : MonoBehaviour
{
    [Header("Bouncing Force")]
    [SerializeField] private Vector2 _bouncingForce;

    [Header("Exist Time")]
    [SerializeField] private float _existTime;

    [Header("Type & Ammount")]
    [SerializeField] private GameEnums.EEnemiesBullet _pieceType;

    [Header("OtherPiece")]
    [SerializeField] BulletPieceController _pieceRef;

    private Rigidbody2D _rb;
    private float _entryTime;
    private bool _isShotFromRight = false; //Bắn từ bên nào để áp dụng vector lực hướng ngược lại

    public bool IsShotFromRight { set { _isShotFromRight = value; } }

    public Vector3 SpawnPosition { set { transform.position = value; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        BulletPiecePool.Instance.AddBulletToPoolDictionary(_pieceType);
        BulletPiecePair bPPair = new BulletPiecePair(gameObject, _pieceRef.gameObject);
        BulletPiecePool.Instance.InstantiateBulletPiece(bPPair, _pieceType);
    }

    private void OnEnable()
    {
        if (_isShotFromRight)
            _rb.AddForce(_bouncingForce * new Vector2(-1f, 1f));
        else
            _rb.AddForce(_bouncingForce);
        _entryTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - _entryTime >= _existTime) 
            gameObject.SetActive(false);
    }
}
