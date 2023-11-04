using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Playerdata", menuName = "Scriptable Object/Playerdata", order = int.MaxValue)]

public class PlayerData_Y : ScriptableObject
{
    [SerializeField] public float[] maxexp;
}
