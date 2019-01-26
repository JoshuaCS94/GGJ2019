using UnityEngine;
using UnityStandardAssets._2D;

public class CharacterLifeInteraction : MonoBehaviour
{
    public Animator[] lifePoints;
    private PlatformerCharacter2D platformerCharacter2D;
    public int MaxLifePoints = 5;
    private int ActualLifePoints;

    public float controlLossOnHitTime = 0.5f;

    public float invulnerableAfterRecoveryTime = 0.5f;
    private bool invulnerable = false;

    private float time;

    public Animator anim;

    public Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
        ActualLifePoints = MaxLifePoints;
    }

    public void Die()
    {
        anim.SetTrigger("die");
        invulnerable = true;
        controlLossOnHitTime = 5;
        body.drag = 1;
        //set Timeout and then reset;
    }

    public void UpdateLifePoints(Damager hitter)
    {
        ActualLifePoints = Mathf.Clamp(ActualLifePoints - hitter.Damage, 0, MaxLifePoints);
        for (var i = 0; i < MaxLifePoints; i++)
        {
            lifePoints[i].SetBool("Active", i < ActualLifePoints);
        }
        if (ActualLifePoints == 0)
        {
            Die();
        }
    }

    public void GetLife(Damager hitter)
    {
        anim.SetBool("healing", true);
        UpdateLifePoints(hitter);
    }

    public void GetHit(Damager hitter)
    {
        if (!invulnerable)
        {
            platformerCharacter2D.ReceiveAttack();
            invulnerable = true;
            time = Time.time;
            anim.SetBool("invulnerable", true);
            anim.SetBool("uncontrollable", true);
            anim.SetTrigger("hit");

            body.velocity = Vector2.zero;

            var vector = transform.position - hitter.transform.position;
            var vector2d = new Vector2(vector.x, vector.y);
            vector2d.Normalize();
            body.AddForce(vector2d * hitter.HitForce);
            UpdateLifePoints(hitter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerable)
        {
            var t = Time.time;
            if (platformerCharacter2D.receivingAttack && t > time + controlLossOnHitTime)
            {
                platformerCharacter2D.RecoverFromAttack();
                anim.SetBool("uncontrollable", false);
            }
            else if (t > time + invulnerableAfterRecoveryTime + controlLossOnHitTime)
            {
                invulnerable = false;
                anim.SetBool("invulnerable", false);
            }
        }
    }
}