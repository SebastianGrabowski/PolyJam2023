using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbillityType
{
	Thunder,
	FireBall
}

[CreateAssetMenu(fileName = "Game", menuName = "Game/Gods")]
public class God : ScriptableObject
{
    public int ID;

	[Space(10)]
    public string Name;
    public string Description;
	
	[Space(10)]
	public float Damage;
	public float Range;
	public float Rate;

	[Space(10)]
	public Sprite Sprite;
	public Sprite IconUI;

	[Space(10)]
	public Abillity Abillity;
	public AbillityType AbillityType;
}
