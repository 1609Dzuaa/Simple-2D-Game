﻿public class GameEnums
{
    //Tạo các enum của State để gán giá trị tương ứng cho Animations
    #region EnumStates
    // ======================================================================== //
    
    public enum EPlayerState
    { idle, run, jump, fall, wallSlide, doubleJump, gotHit, wallJump, dash }

    public enum EMEnemiesState
    { idle, patrol, attack, gotHit }

    public enum ENMEnemiesState
    { idle, attack, gotHit }

    public enum ERhinoState
    { idle, patrol, attack, gotHit, wallHit }

    public enum EBatState
    { idle, patrol, attack, gotHit, sleep, ceilIn, ceilOut, retreat }

    public enum EBunnyState
    { idle, patrol, attackJump, gotHit, attackFall }

    public enum ESnailState
    { idle, patrol, attack, gotHit, shellHit}

    public enum EHedgehogState
    { idle, spikeOut, gotHit, spikeIn, spikeIdle }

    public enum ENPCState
    { idle }

    public enum ESlimState
    { idle, gotHit }

    public enum EGhostState
    { disappear, appear, idle, gotHit }

    public enum EPigState
    { idle, patrol, attackGreen, gotHitGreen, attackRed,  gotHitRed }

    public enum EGeckoState
    { idle, patrol, attack, gotHit, hide  }

    public enum ETrunkState
    { idle, patrol, withdrawn, attack, gotHit }

    public enum ERockMove { Left, Top, Right, Bottom }

    // ======================================================================== //
    #endregion

    #region EnumBuffs

    public enum EBuffs
    { None, Speed, Jump, Invisible, Shield, Absorb }

    #endregion

    #region EnumEvents

    public enum EEvents
    {
        BulletOnHit,
        BulletOnReceiveInfo,
        PlayerOnTakeDamage,
        PlayerOnJumpPassive,
        PlayerOnInteractWithNPCs,
        PlayerOnStopInteractWithNPCs,
        PlayerOnBeingPushedBack,
        FanOnBeingDisabled
    }

    #endregion

    #region EnumVfxs&Bullets

    public enum EPoolable
    {
        Dashable,
        HitShield,
        GeckoAppear,
        GeckoDisappear,
        CollectFruits,
        CollectDiamond,
        CollectHP,
        BrownExplosion,
        PlantBullet,
        BeeBullet,
        TrunkBullet
    }

    #endregion

    #region Sound

    public enum ESoundName
    {
        StartMenuTheme,
        Level1Theme,
        Level2Theme,
        BossTheme,
        CollectFruitSfx,
        CollectHPSfx,
        PlayerGotHitSfx,
        PlayerJumpSfx,
        PlayerDashSfx,
        PlayerDeadSfx,
        PlayerLandSfx,
        PlantShootSfx,
        TrunkShootSfx,
        EnemiesDeadSfx,
        GeckoAttackSfx
    }

    #endregion

    #region UI

    public enum EUI { }

    #endregion
}
