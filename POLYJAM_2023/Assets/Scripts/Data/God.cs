using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
	Thunder,
	FireBall
}

[CreateAssetMenu(fileName = "Game", menuName = "Game/Gods")]
public class God : ScriptableObject
{
    public int ID;

    public string Name;
    public string Description;
	
	public float Damage;
	public float Range;
	public float Rate;

	public Sprite Sprite;
	public Sprite IconUI;

	public GameObject AbilityPrefab;

	public AbilityType AbilityType;
}
