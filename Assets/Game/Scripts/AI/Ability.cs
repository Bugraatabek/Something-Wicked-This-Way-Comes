using System.Collections;
using TD.Combat;
using TD.Core;
using UnityEngine;

public class Ability : ScriptableObject 
{
    public virtual bool CastToHero(Hero target)
    {
        if(target.IsDead())
        {
            return false;
        }
        return false;
        
    }

    public virtual bool CastToTower(Tower tower)
    {
        return false;
    }

    public virtual bool CastToEnemy(Enemy enemy)
    {
        return false;
    }
}