using System.Collections.Generic;
using TD.Attributes;
using UnityEngine;

namespace TD.Attributes
{
    [CreateAssetMenu(fileName = "ArmorChart", menuName = "Attributes/NewArmorChart", order = 0)]
    public class ArmorChart : ScriptableObject 
    {
        [SerializeField] Armor[] armors;
        Dictionary<EArmorType, Dictionary<EDamageType, float>> lookupDict = null;

        
        public float GetDamage(EArmorType armorType, EDamageType damageType, float damage)
        {
            BuildLookup();
            return (lookupDict[armorType][damageType] / 100) * damage;
        }

        private void BuildLookup()
        {
            if(lookupDict != null) return;
            lookupDict = new Dictionary<EArmorType, Dictionary<EDamageType, float>>();
            foreach (var armor in armors)
            {
                var damageLookupTable = new Dictionary<EDamageType, float>();
                foreach (var damageType in armor.damageType)
                {
                    damageLookupTable[damageType.damageType] = damageType.damagePercentage;
                }
                lookupDict[armor.armorType] = damageLookupTable;
            }
        }

        [System.Serializable]
        class Armor
        {
            public EArmorType armorType;
            public DamageType[] damageType;
        }
        
        [System.Serializable]
        class DamageType
        {
            public EDamageType damageType;
            public float damagePercentage;
        }
    }
}