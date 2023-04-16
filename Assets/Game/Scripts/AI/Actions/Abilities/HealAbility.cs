using TD.Combat;
using TD.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "HealAbility", menuName = "Something Wicked This Way Comes/HealAbility", order = 0)]
public class HealAbility : Ability 
{
    public float healAmount = 20f;
    public override bool CastToEnemy(Enemy enemy)
    {
        enemy.Heal(healAmount);
        return true;
    }
}