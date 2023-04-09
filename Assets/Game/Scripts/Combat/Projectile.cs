using TD.Attributes;
using UnityEngine;
using TD.Core;


namespace TD.Combat
{
    public class Projectile : MonoBehaviour
    {
        EDamageType damageType;
        Target target;
        float damage = 0;
        [SerializeField] float projectileSpeed = 2f;
        Transform spawnPoint;

        private void Awake() 
        {
            spawnPoint = FindObjectOfType<SpawnPoint>().transform; 
        }


        public void Setup(EDamageType damageType, Target target, float damage)
        {
            this.damage = damage;
            this.damageType = damageType;
            this.target = target;
        }

        private void Update() 
        {
            if(target == null) return;
            if(target.transform.position == spawnPoint.position) this.Destroy();
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height/2;
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.GetComponent<Target>() == this.target)
            {
                other.GetComponent<Enemy>().TakeDamage(damage, damageType);
                Destroy(gameObject);
            }    
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
