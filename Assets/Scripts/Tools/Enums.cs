using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefencerType
{
    None,
    Defencer1,
    Defencer2,
    Defencer3
}
public enum EnemyType
{
    None,
    Enemy1,
    Enemy2,
    Enemy3
}
[System.Flags]
public enum Directions
{
    None = 0,
    Forward = 1,
    Backward = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3
}
//Forward 0001, Backward 0010, Left 0100, Right 1000