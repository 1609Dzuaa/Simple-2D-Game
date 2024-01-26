﻿public static class GameConstants
{
    #region Range Constants
    //Có thể hằng số quá nhỏ dẫn đến việc check vị trí trò chuyện 0 đc như ý
    //=>Tăng giá trị hằng số cho lớn xíu
    public const float CAN_START_CONVERSATION_RANGE = 0.7f;
    public const float NEAR_CONVERSATION_RANGE = 0.1f;

    //Range tối thiểu mà bat có thể flip khi chase player
    //tránh flip loạn xạ khi dist to player quá gần
    public const float BAT_FLIPABLE_RANGE = 0.15f;
    public const float PIG_FLIPABLE_RANGE = 0.6f;
    public const float BUNNY_KNOCK_FORCE_DECREASE = 3.0f;
    public const float NEAR_ZERO_THRESHOLD = 0.1f;

    //Định nghĩa các hằng số lock cam move cũng như player dưới
    public const float GAME_MIN_BOUNDARY = 10.0f;

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

    #region Fire Trap Constants
    public const string FIRE_TRAP_ANIM_GOT_HIT = "GotHit";
    public const string FIRE_TRAP_ANIM_ON = "On";
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
    public const int RENDER_MAP_ORDER = 5;
    #endregion

    #region Tag Constants
    public const string GROUND_TAG = "Ground";
    public const string PLATFORM_TAG = "Platform";
    public const string TRAP_TAG = "Trap";
    public const string BULLET_TAG = "Bullet";
    public const string SHIELD_TAG = "Shield";
    public const string ENEMIES_TAG = "Enemy";
    public const string BUFF_TAG = "Buff";
    public const string PLAYER_TAG = "Player";
    public const string BOX_TAG = "Box";
    public const string PORTAL_TAG = "Portal";
    public const string ONLY_FAN_TAG = "OnlyFan";
    public const string DEAD_ZONE_TAG = "DeadZone";
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

    #region Sound
    public const string COLLECT_FRUITS_SOUND = "CollectFruitsSound";
    public const string COLLECT_HP_SOUND = "CollectHPSound";
    public const string PLAYER_GOT_HIT_SOUND = "PlayerGHSound";
    public const string PLAYER_JUMP_SOUND = "PlayerJumpSound";
    public const string PLAYER_DASH_SOUND = "PlayerDashSound";
    public const string PLAYER_DEAD_SOUND = "PlayerDeadSound";
    public const string PLAYER_LAND_SOUND = "PlayerLandSound";

    public const string PLANT_SHOOT_SOUND = "PlantShootSound";
    public const string TRUNK_SHOOT_SOUND = "TrunkShootSound";
    public const string ENEMIES_DEAD_SOUND = "EnemiesDeadSound";

    public const string GECKO_ATTACK_SOUND = "GeckoAttackSound";
    #endregion

    #region Button Constants

    public const string JUMP_BUTTON = "Jump";
    public const string DASH_BUTTON = "Dash";
        
    #endregion
}
