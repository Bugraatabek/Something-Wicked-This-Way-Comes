using TD.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageTowerAbility", menuName = "Something Wicked This Way Comes/DamageTowerAbility", order = 0)]
public class DamageTowerAbility : Ability 
{
    [SerializeField] float damage = 50;
    public override bool CastToTower(Tower tower)
    {
        tower.TakeDamage(damage);
        return true;
    }
}