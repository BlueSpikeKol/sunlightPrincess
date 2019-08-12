using UnityEngine;
using System.Collections;

namespace SXP
{
    public enum FinishingLevelEnum { REPLAY_LEVEL, NEXT_LEVEL }

    public enum LevelBoundaryEnum { LEFT, RIGHT, BOTTOM, TOP }

    public enum EnemyStateEnum { NONE, IDLE, ATTACK }
    public enum EnemyMovementTypeEnum { WALKER, FLYER, JUMPER }
    public enum EnemyStanceTypeEnum { PASSIVE, AGGRESIVE, ATTACK_ON_HIT }
}
