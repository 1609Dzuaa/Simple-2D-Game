﻿public static class GameConstants
{
    #region Range Constants
    public const float START_CONVERSATION_RANGE = 0.05f;

    //Range tối thiểu mà bat có thể flip khi chase player
    //tránh flip loạn xạ khi dist to player quá gần
    public const float BAT_FLIPABLE_RANGE = 0.15f;

    #endregion

    #region Time Constants
    public const float DELAYPLAYERRUNSTATE = 0.1f;

    #endregion

    #region Animation Constants
    //-----------------------------//

    #region Parameter State
    public const string ANIM_PARA_STATE = "state";
    #endregion

    #region Player
    public const string DEAD_ANIMATION = "dead";
    #endregion

    #region Shield String Constants
    public const string RUNNINGOUT = "RunningOut";
    public const string IDLE = "Idle";
    #endregion

    //-----------------------------//
    #endregion

    #region Layer Constants
    public const string GROUND_LAYER = "Ground";
    public const string PLAYER_LAYER = "Player";
    public const string IGNORE_ENEMIES_LAYER = "Ignore Enemies";
    public const string SHIELD_LAYER = "Shield";
    #endregion

    #region Render Layer Constants
    public const string RENDER_MAP_LAYER = "Map_Layer";
    #endregion

    #region Tag Constants
    public const string GROUND_TAG = "Ground";
    public const string PLATFORM_TAG = "Platform";
    public const string TRAP_TAG = "Trap";
    public const string BULLET_TAG = "Bullet";
    public const string SHIELD_TAG = "Shield";
    public const string ENEMIES_TAG = "Enemy";
    public const string BUFF_TAG = "Buff";
    #endregion

    #region Name Constants
    public const string PLAYER_NAME = "Player";
    #endregion

    #region Axis Constants
    public const string VERTICAL_AXIS = "Vertical";
    public const string HORIZONTAL_AXIS = "Horizontal";
    #endregion

    #region HEALTHPOINT(HP) Constants
    public const int HP_STATE_NORMAL = 0;
    public const int HP_STATE_LOST = 1;
    public const int HP_STATE_TEMP = 2;
    #endregion

    #region Enemies Bullets Constants
    public const int PLANT_BULLET = 3;
    public const int BEE_BULLET = 4;
    public const int TRUNK_BULLET = 5;
    #endregion

    #region Effect
    public const string DASHABLE_EFFECT = "DashableEffect";
    public const string GECKO_APPEAR_EFFECT = "GeckoAppearEffect";
    public const string GECKO_DISAPPEAR_EFFECT = "GeckoDisappearEffect";
    public const string HIT_SHIELD_EFFECT = "HitShieldEffect";
    public const string COLLECT_FRUITS_EFFECT = "CollectFruitsEffect";
    public const string COLLECT_DIAMOND_EFFECT = "CollectDiamondEffect";
    public const string COLLECT_HP_EFFECT = "CollectHPEffect";
    public const string BROWN_EXPLOSION = "BrownExplosionEffect";
    #endregion

    #region Sound
    public const string COLLECT_FRUITS_SOUND = "CollectFruitsSound";
    public const string COLLECT_HP_SOUND = "CollectHPSound";
    public const string PLAYER_GOT_HIT_SOUND = "PlayerGHSound";
    public const string PLAYER_JUMP_SOUND = "PlayerJumpSound";
    public const string PLAYER_DASH_SOUND = "PlayerDashSound";
    public const string PLAYER_DEAD_SOUND = "PlayerDeadSound";
    #endregion

    //Dùng để phân biệt function đã đky event OnEnemiesDie => Vì cả Player lẫn Bullet đều có thể dmg enemies
    #region GameObjects ID 
    public const int PLAYER_ID = 10;
    public const int BULLET_ID = 11;
    #endregion
}
