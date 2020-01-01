using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum BuildLevel { Village, Town, City, Capital };
[CreateAssetMenu(fileName = "NewBuilds", menuName = "Builds/New Builds", order = 1)]
public class Builds : ScriptableObject
{
    public string buildName = "New Builds";
    public BuildLevel buildLevel = BuildLevel.Village;
    public int maxStackSize = 100;
    public int maxStackSlot = 10;
    public int Money = 10000;



    public Vector3 buildPosition = new Vector3(0, 0, 0);
}
