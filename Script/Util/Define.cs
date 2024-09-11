using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum Keys
    {
        Jump,
        down,
        left,
        right,
        attack,
        interaction,
        None
    }
    public enum Mouse
    {
        LeftClick,
        LeftClickUp,
        RightClick,
    }
    public enum MonsterType
    {
        Flying,
        Ground
    }
    public enum MonsterName
    {
        Oak = 1,
        Goblin = 2,
        Slime = 3,
        Golem = 4,
        Wybern = 5,
        None = 6
    }

    public static void Init()
    {
        //primary value
        keyBinding.Add(Keys.Jump, KeyCode.Space);
        keyBinding.Add(Keys.down, KeyCode.S);
        keyBinding.Add(Keys.left, KeyCode.A);
        keyBinding.Add(Keys.right, KeyCode.D);
        keyBinding.Add(Keys.attack, KeyCode.Mouse0);
        keyBinding.Add(Keys.interaction, KeyCode.F);
    }
    public static Dictionary<Keys, KeyCode> keyBinding = new Dictionary<Keys, KeyCode>();
    public enum timeState
    {
        day,
        night,
    }
    public enum block
    {
        grass,
        Dirt,
        Rock,
        Coal,
        None,
    }
    public enum ItemName
    {
        None,
    }
}
