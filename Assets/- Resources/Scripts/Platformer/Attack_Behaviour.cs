using UnityEngine;
using UnityStandardAssets._2D;

public class Attack_Behaviour : MonoBehaviour
{
    public PlatformerCharacter2D platformer;
    public Rigidbody2D body;
    public float unfoldingTime = 0.2f;
    public float AttackTime = .5f;
    public float coolDownTime = 0.4f;
//    public GameObject CollisionParticles;
    private float time;
//    private ParticleSystem particleSystem;
//    public GameObject attackObject;
    public float speedWhileUnfolding = .8f;
    public LayerMask canAffectTo;
    public float AttackRange = 2.5f;

    public enum AttackState
    {
        Idle,
        Unfolding,
        Attacking,
        Cooling
    }

    public AttackState State;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        platformer = gameObject.GetComponent<PlatformerCharacter2D>();
        State = AttackState.Idle;
//        particleSystem = CollisionParticles.GetComponent<ParticleSystem>();
//        StopParticleSystem();
//        CollisionParticles.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (State == AttackState.Idle)
        {
            var a = Input.GetButtonDown("Attack");
            if (Input.GetButtonDown("Attack") && platformer.controllable)
            {
                time = Time.time;
                platformer.anim.SetTrigger("attack");
                platformer.canTurn = false;
                platformer.canJump = false;
                platformer.speedMultiple = speedWhileUnfolding;
                State = AttackState.Unfolding;
            }
        }
        else if (State == AttackState.Unfolding && Time.time > time + unfoldingTime)
        {
            platformer.getStuckToTheGround = true;
            State = AttackState.Attacking;
            time = Time.time;
            Attack();
        }
        else if (State == AttackState.Attacking && Time.time > time + AttackTime)
        {
            if (!platformer.receivingAttack)
                platformer.controllable = true;
            State = AttackState.Cooling;
            time = Time.time;
            platformer.getStuckToTheGround = false;
            platformer.canTurn = true;
            platformer.speedMultiple = 1;
            platformer.canJump = true;
        }
        else
        {
            var timestamp = Time.time;
            if (State == AttackState.Cooling && timestamp > time + coolDownTime)
            {
                State = AttackState.Idle;
            }
        }
    }

    Vector2 lastHitPosition;

    void Attack()
    {
        var raycast = Physics2D.Raycast(transform.position, platformer.FacingRight ? Vector2.right : Vector2.left,
            AttackRange, canAffectTo);
        if (raycast.collider)
        {
            lastHitPosition = raycast.point;
//            CollisionParticles.transform.position = lastHitPosition;
//            particleSystem.Play();
            Invoke("StopParticleSystem", .75f);
        }
    }

    void StopParticleSystem()
    {
//        particleSystem.Stop();
    }

    private void OnDrawGizmos()
    {
        Vector2 to = (platformer.FacingRight ? Vector2.right : Vector2.left);
        Vector2 pos = transform.position;
        Gizmos.DrawLine(pos, pos + to*AttackRange);
        Gizmos.DrawSphere(lastHitPosition, .2f);
    }
}