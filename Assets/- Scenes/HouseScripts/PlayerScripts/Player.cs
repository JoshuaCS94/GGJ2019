using UnityEngine;
using UnityStandardAssets._2D;

namespace HouseScripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public float controlLossOnHitTime = 0.5f;
        public float invulnerableAfterRecoveryTime = 0.5f;
        protected PlatformerCharacter2D character;
        
        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }
        
        void Update()
        {
            if (invulnerable)
            {
                var t = Time.time;
                if (character.receivingAttack && t > time + controlLossOnHitTime)
                {
                    character.RecoverFromAttack();
                    character.anim.SetBool("uncontrollable", false);
                }
                else if (t > time + invulnerableAfterRecoveryTime + controlLossOnHitTime)
                {
                    invulnerable = false;
                    character.anim.SetBool("invulnerable", false);
                }
            }
        }

        public bool invulnerable;
        private float time;
        public void GetForce(Vector2 origin, float force)
        {
            if (!invulnerable)
            {
                character.ReceiveAttack();
                invulnerable = true;
                time = Time.time;
                character.anim.SetBool("invulnerable", true);
                character.anim.SetBool("uncontrollable", true);
                character.anim.SetTrigger("hit");

                character.body.velocity = Vector2.zero;
                var pos = transform.position;
                Vector2 vector = new Vector2(pos.x-origin.x,pos.y-origin.y);
                var vector2d = new Vector2(vector.x, vector.y);
                vector2d.Normalize();
                character.body.AddForce(vector2d * force);
            }
        }

        public virtual void Punch()
        {
            Vector2 looking = (character.facingRight) ? Vector2.right : Vector2.left;
            RaycastHit2D[] hits = new RaycastHit2D[8];
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = LayerMask.NameToLayer("Enemy");
            Physics2D.Raycast(character.transform.position, looking, filter, hits, 1.5f);
            foreach (var item in hits)
            {
                EnemyBehaviour enemy = item.collider.GetComponent<EnemyBehaviour>();
                Hit(enemy);
                //TODO: play animation
            }
        }

        public virtual void Hit(EnemyBehaviour enemy, bool father = false)
        {
            enemy.Hit(father);
        }
    }
}
