using UnityEngine;

public class BeeGotHitState : MEnemiesGotHitState
{
    private BeeManager _beeManager;
    private float Zdegree = 0f;
    private float lastRotateTime;

    public override void EnterState(CharactersManager charactersManager)
    {
        base.EnterState(charactersManager);
        _beeManager = (BeeManager)charactersManager;
        _beeManager.Animator.SetInteger("state", (int)EnumState.EMEnemiesState.gotHit);
        _beeManager.Collider2D.enabled = false;
        _beeManager.GetRigidbody2D().bodyType = RigidbodyType2D.Dynamic;
        lastRotateTime = Time.time;
        ApplyForce();
        //Debug.Log("GH");
    }

    public override void ExitState() { }

    public override void Update()
    {
        if (Time.time - lastRotateTime >= _beeManager.GetTimeEachRotate())
        {
            Zdegree -= _beeManager.GetDegreeEachRotation();
            _beeManager.transform.Rotate(0f, 0f, Zdegree);
            lastRotateTime = Time.time;
        }
    }

    protected void ApplyForce()
    {
        _beeManager.GetRigidbody2D().AddForce(_beeManager.KnockForce, ForceMode2D.Impulse);
    }
}
