using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        public float topSpeed = 15;
        [SerializeField] private float MaxSpeed = 10f; // The fastest the player can travel in the x axis.
        public float Acceleration = 0.25f;
        [SerializeField] private float JumpForce = 400f; // Amount of force added when the player jumps.

        [SerializeField] private bool AirControl = false; // Whether or not a player can steer while jumping;

        [Header("Grounded Settings")]
        public Transform GroundCheckTransform; // A position marking where to check if the player is grounded.

        public Vector2 groundCheckArea = new Vector2(0.1f, 0.05f);
        [SerializeField] private LayerMask CanStandOver; // A mask determining what is ground to the character
        [SerializeField] private LayerMask CanSlideOver;

        [Header("Wall Slide Settings")]
        public Transform BottomWallCheckTransformL; // A position marking where to check if the player is wallSliding.

        public Transform BottomWallCheckTransformR; // A position marking where to check if the player is wallSliding.
        public Vector2 BottomWallCheckArea = new Vector2(0.05f, 0.1f);
        public float WallSlideDescendingSpeed = -1.5f;
        [Range(100, 800)] public float WallSlideVerticalForce = 400;
        [Range(100, 800)] public float WallSlideRepulsionForce = 500;

        [Range(0.1f, 1)] public float WallSlideAirTime = 0.25f;


        [Header("Wall Slide Cohesion Settings")] // Used to stop generating imposible forces
        public Transform FullWallCheckTransformL; // A position marking where to check if the player is against a wall.

        public Transform FullWallCheckTransformR; // A position marking where to check if the player is against a wall.
        public Vector2 FullWallCheckArea = new Vector2(0.05f, 0.1f);

        public bool semistate_bottomWallCheckL { get; private set; }
        public bool semistate_bottomWallCheckR { get; private set; }
        public bool semistate_fullWallCheckL { get; private set; }
        public bool semistate_fullWallCheckR { get; private set; }
        public bool semistate_grounded { get; private set; }

        public Animator anim; // Reference to the player's animator component.

        private Rigidbody2D body;
        private bool facingRight = true;

        private bool wallSliding;
        private bool pendingAirControl;
        private float airControlTime;
        private bool pending_Flip;

        [Header("Turning Settings")] [Range(0.05f, 0.25f)] public float TurningSpeed = 0.1f;

        [Space(10)] private float actualDirection = 1;
        private int direction = 1;
        public int offsetDirection = 1;
        private float offsetDirectionTime;
        [Range(0.05f, 0.5f)] public float offsetDirectionCountDown = 0.1f;
        private bool pendingOffsetDirectionChange;
        public bool receivingAttack;
        public bool canTurn = true;

        public void ReceiveAttack()
        {
            controllable = false;
            receivingAttack = true;
        }

        public void RecoverFromAttack()
        {
            controllable = true;
            receivingAttack = false;
        }

        public bool FacingRight
        {
            get { return facingRight; }
        }

        private void Awake()
        {
            GroundCheckTransform = transform.Find("GroundCheck");
            BottomWallCheckTransformL = transform.Find("BottomWallCheckL");
            BottomWallCheckTransformR = transform.Find("BottomWallCheckR");
            FullWallCheckTransformL = transform.Find("FullWallCheckL");
            FullWallCheckTransformR = transform.Find("FullWallCheckR");
            body = GetComponent<Rigidbody2D>();
        }

        private bool Collide(Transform checker, Vector2 area, LayerMask layers)
        {
            return Physics2D.OverlapBox(checker.position, area, 0, layers) != null;
        }

        private void UpdateSemistates()
        {
            semistate_grounded = Collide(GroundCheckTransform, groundCheckArea, CanStandOver);
            semistate_bottomWallCheckL = Collide(BottomWallCheckTransformL, BottomWallCheckArea, CanSlideOver);
            semistate_bottomWallCheckR = Collide(BottomWallCheckTransformR, BottomWallCheckArea, CanSlideOver);
            semistate_fullWallCheckL = Collide(FullWallCheckTransformL, FullWallCheckArea, CanSlideOver);
            semistate_fullWallCheckR = Collide(FullWallCheckTransformR, FullWallCheckArea, CanSlideOver);
        }

        private void FixedUpdate()
        {
            UpdateSemistates();
            anim.SetFloat("verSpeed", body.velocity.y);
            anim.SetBool("grounded", semistate_grounded);
        }

        private void Update()
        {
            if (actualDirection != direction)
            {
                actualDirection += TurningSpeed * direction;
                actualDirection = Mathf.Clamp(actualDirection, -1, 1);
                anim.SetFloat("turningOffset", actualDirection);
            }
            else if (pending_Flip)
            {
                anim.GetComponent<SpriteRenderer>().flipX = !facingRight;
                anim.SetBool("turning", false);
            }
            if (pendingAirControl && Time.time > airControlTime + WallSlideAirTime)
            {
                pendingAirControl = false;
                AirControl = true;
            }
            if (pendingOffsetDirectionChange && Time.time > offsetDirectionCountDown + offsetDirectionTime)
            {
                offsetDirection = direction;
                pendingOffsetDirectionChange = false;
            }
        }

        public bool controllable = true;
        private Vector2 lastVelocity;
        public bool getStuckToTheGround = false;
        public float speedMultiple = 1;
        public bool canJump = true;
        public void Move(float move, bool crouch, bool jump)
        {
            if (controllable)
            {
                //only control the player if grounded or airControl is turned on
                if (semistate_grounded || AirControl)
                {
                    // The Speed animator parameter is set to the absolute value of the horizontal input.
                    anim.SetFloat("horSpeed", Mathf.Abs(body.velocity.x));

                    lastVelocity = body.velocity;
                    if (semistate_grounded && getStuckToTheGround)
                    {
                        lastVelocity = Vector2.zero;
                        body.velocity = lastVelocity;
                    }
                    else
                    {
                        if (move != 0)
                        {
                            lastVelocity.x += Acceleration * Mathf.Sign(move);
                            if (Mathf.Abs(lastVelocity.x) > MaxSpeed)
                                lastVelocity.x = MaxSpeed * move;
                        }
                        else lastVelocity.x = 0;
                        lastVelocity.x *= speedMultiple;
                        body.velocity = lastVelocity;
                        if (canTurn)
                        {
                            // If the input is moving the player right and the player is facing left...
                            if (move > 0 && !facingRight)
                            {
                                // ... flip the player.
                                Flip();
                            }
                            // Otherwise if the input is moving the player left and the player is facing right...
                            else if (move < 0 && facingRight)
                            {
                                // ... flip the player.
                                Flip();
                            }
                        }
                    }
                    
                }
                var falling = body.velocity.y < 0;
                var wallL = move < 0 && semistate_bottomWallCheckL && falling && move != 0;
                var wallR = move > 0 && semistate_bottomWallCheckR && falling && move != 0;
                wallSliding = wallL || wallR;

                if ((semistate_fullWallCheckL && direction < 0 && move < 0) ||
                    (semistate_fullWallCheckR && direction > 0 && move > 0))
                    ForceToZero(true, false);

                if (jump && canJump)
                {
                    if (semistate_grounded)
                    {
                        body.velocity = new Vector2(body.velocity.x, 0);
                        body.AddForce(new Vector2(0f, JumpForce));
                    }
//                    else if ((semistate_bottomWallCheckL || semistate_bottomWallCheckR) && AirControl)
//                    {
//                        ForceDirection(!semistate_bottomWallCheckR);
//
//                        ForceToZero(true, true);
//                        body.AddForce(new Vector2(WallSlideRepulsionForce * ((semistate_bottomWallCheckR) ? -1 : 1),
//                            WallSlideVerticalForce));
//                        AirControl = false;
//                        pendingAirControl = true;
//                        airControlTime = Time.time;
//                        anim.SetTrigger("wallJump");
//                    }
                }
                if (AirControl && !semistate_grounded && wallSliding)
                {
                    body.velocity = new Vector2(0, WallSlideDescendingSpeed);
                    anim.SetBool("wallSlide", true);
                    ForceDirection(wallR ? true : false);
                }
                else
                    anim.SetBool("wallSlide", false);

                //clampSpeed
                var vel = body.velocity;
                body.velocity = new Vector3(Mathf.Clamp(vel.x, -topSpeed, topSpeed),
                    Mathf.Clamp(vel.y, -topSpeed, topSpeed));
            }
        }


        public void ForceToZero(bool x, bool y)
        {
            var temp = body.velocity;
            body.velocity = new Vector2(x ? 0 : temp.x, y ? 0 : temp.y);
        }

        private void ForceDirection(bool facingR)
        {
            facingRight = facingR;
            direction = facingR ? 1 : -1;
            pending_Flip = false;
            pendingOffsetDirectionChange = false;
            anim.GetComponent<SpriteRenderer>().flipX = !facingR;
            anim.SetBool("turning", false);
            anim.SetFloat("turningOffset", direction);
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;
            direction *= -1;
            pending_Flip = true;

            anim.GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("turning", true);
            anim.SetFloat("turningOffset", direction);

            offsetDirectionTime = Time.time;
            pendingOffsetDirectionChange = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            //groundCheck
            Gizmos.DrawWireCube(GroundCheckTransform.position, groundCheckArea);
            //floor wallCheck
            Gizmos.DrawWireCube(BottomWallCheckTransformL.position, BottomWallCheckArea);
            Gizmos.DrawWireCube(BottomWallCheckTransformR.position, BottomWallCheckArea);
            //total wallCheck
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(FullWallCheckTransformL.position, FullWallCheckArea);
            Gizmos.DrawWireCube(FullWallCheckTransformR.position, FullWallCheckArea);
            //head wallCheck
        }
    }
}