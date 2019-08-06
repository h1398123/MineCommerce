using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum Religion { HEALTH, WEALTH, FOOD, WEAPON, ARMOR, BUFF, EMPTY };
public enum Currency { HEALTH, WEALTH, FOOD, WEAPON, ARMOR, BUFF, EMPTY };
[CreateAssetMenu(fileName ="NewCountry",menuName ="Country/New Country",order =1)]
public class Coutry : ScriptableObject
{
    public string buildName = "New Builds";
}
