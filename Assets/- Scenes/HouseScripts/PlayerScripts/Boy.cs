using UnityEngine;

namespace HouseScripts.PlayerScripts
{
    public class Boy : Player
    {
        // Update is called once per frame
        void Update()
        {
        
        }

        public LayerMask what_to_hit;
        
        public override void Punch()
        {
            Vector2 looking = (character.facingRight) ? Vector2.right : Vector2.left;
            RaycastHit2D[] hits = new RaycastHit2D[8];
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = what_to_hit;
            Physics2D.Raycast(character.transform.position, looking, filter, hits, 1.5f);
            foreach (var item in hits)
            {
                EnemyBehaviour enemy = item.collider.GetComponent<EnemyBehaviour>();
                Hit(enemy);
                //TODO: play animation
            }
        }

        public virtual void Hit(EnemyBehaviour enemy)
        {
            //enemy.Hit(father);
        }
    }
}
