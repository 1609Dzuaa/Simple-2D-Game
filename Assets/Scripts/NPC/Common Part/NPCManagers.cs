using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManagers : CharactersManager
{
    [Header("Range")]
    [SerializeField] protected float _triggerConversationRange;

    [Header("Player")]
    [SerializeField] protected Transform _playerRef;

    protected bool _hasDetectedPlayer;

    public bool HasDetectedPlayer { get { return _hasDetectedPlayer; } }

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
        DetectedPlayer();
        DrawLineDetectPlayer();
    }

    protected virtual bool DetectedPlayer()
    {
        return _hasDetectedPlayer = Vector2.Distance(transform.position, _playerRef.position) <= _triggerConversationRange;
    }

    protected void DrawLineDetectPlayer()
    {
        if (_hasDetectedPlayer)
            Debug.DrawLine(transform.position, _playerRef.position, Color.red);
        else
            Debug.DrawLine(transform.position, _playerRef.position, Color.green);

    }
}
