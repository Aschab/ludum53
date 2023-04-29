using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public float itemSpawnRate = 0.2f;
}
