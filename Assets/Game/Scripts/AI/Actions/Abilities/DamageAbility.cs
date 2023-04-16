using UnityEngine;

[CreateAssetMenu(fileName = "DamageHeroAbility", menuName = "Something Wicked This Way Comes/DamageAbility", order = 0)]
public class DamageAbility : Ability
{
    [SerializeField] float damage = 50;

    public override bool CastToHero(Hero target)
    {
        if(target.IsDead())
        {
            return false;
        }
        else
        {
            target.TakeDamage(damage);
            return true;
        }
        
    }
}