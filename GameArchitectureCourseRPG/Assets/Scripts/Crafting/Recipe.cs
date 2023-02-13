using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Recipe")]
public class Recipe : ScriptableObject
{
    public List<Item> Ingredients;
    public List<Item> Rewards;
}
