using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbillityType
{
	Thunder,
	FireBall,
	Resurrection,
	Sun

}

[CreateAssetMenu(fileName = "Game", menuName = "Game/Gods")]
public class God : ScriptableObject
{
    public int ID;
   
	[Space(10)]
    public string Name;
    public string Description;

	[Space(10)]
    public float TimeToUnlock;

	[Space(10)]
	public Sprite Sprite;
	public Sprite IdleGlowSprite;
	public Sprite EmptySprite;
	public Sprite HoveredSprite;
	public Sprite CooldownFillSprite;
	public Sprite IconUI;
	
	[Space(10)]
	public Color CooldownUIColor;

	[Space(10)]
	public Abillity Abillity;
	public AbillityType AbillityType;

	[Space(10)]
	public Sprite[] RangeSprites;

	[Space(10)]
	public Skill[] Skills;
}
