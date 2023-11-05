using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptable", menuName = "Weapon/WeaponType", order =1)]
public class WeaponTypeScriptable : ScriptableObject
{
    public Projectile ProjectileType;
    public Color ProjectileColor;
    
}
