using UnityEngine;

public static class Definitions
{
    public enum eClassType
    {
        None,
        WARRIOR,
        ARCHER,
        MAGE,
    }

    public static string NAMESPACE_INGAMESKILL = "IngameSkill";

    #region Layer

    public static LayerMask PLAYER_LAYER = LayerMask.GetMask("Player");
    public static LayerMask OBJECT_LAYER = LayerMask.GetMask("Object");
    public static LayerMask BEHIND_LAYER = LayerMask.GetMask("BehindMask");
    public static LayerMask DAMAGABLE_LAYER = PLAYER_LAYER | BEHIND_LAYER;

    #endregion
}
