using UnityEngine;



namespace Character
{
    internal class PlayerController : CharacterProperties
    {
#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| SERIALIZE FIELDS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||

        [Header("Classes And Components")]


        [Tooltip("Add the game manager class to this variable!")]
        [SerializeField] private GameManager gameManager;



        [Tooltip("Assign the character's Rigidbody Component to this variable")]
        [SerializeField] private Rigidbody2D rb;


        [Tooltip("Drag the character's collaborator here!")]
        [SerializeField] private CapsuleCollider2D characterCollider;



        [Tooltip("Add the character's animator component here!")]
        [SerializeField] private Animator animator;


        [Tooltip("Assign the Transform component of the character the player controls here!")]
        [SerializeField] private Transform characterTransform;









        



        
        [Space(20f)]

        [Header("Character Other Properties")]


        [Tooltip("Jump Force")]
        [SerializeField] private float jumpForce = 100;




        
        [Header("Jump properties")]


        [Tooltip("Jump Control Collider add to this variable")]
        [SerializeField] private Transform groundCheck;

        [Tooltip("jump control layer mask")]
        [SerializeField] private LayerMask groundLayerMask;

#endregion  // ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||
















#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| PRIVATE FIELDS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||

        // If the value character touches the ground, this variable will take the value true!
        private bool isGrounded = false;



        private float _characterSpeed;


        private const float _characterSlowSpeed = 2.2f;

#endregion  // ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||









#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| PROPERTIES ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||


    internal float _characterSpeedProperties
    {
        get { return this._characterSpeed; }
        set { this._characterSpeed = value;}
    }


#endregion ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| PRIVATE FIELDS ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||














#region ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| MonoBehaviour Call Backs Functions ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||
        

        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.collider.gameObject.layer == LayerMask.NameToLayer("Dead"))
            {
                TakeDamage(null);
            }
        }


        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Coin")
            {
                this.gameManager.CoinCalculation();
                Destroy(other.gameObject);
            }


            if (other.gameObject.tag == "Win")
            {
                StartCoroutine(this.gameManager.GameWinPanel());
            }
        }

#endregion  // ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~|| XXXX ||~~~~~~~~~~~~~~|| XX ||~~~~~~~~~~~~~~||





        /// <summary>
        /// With this method, our player will be able to move our character!
        /// </summary>
        internal override void Move()
        {
            // ~~ Variables ~~
            float _horizontal;


            _horizontal = Input.GetAxis("Horizontal");

            this.rb.velocity = new Vector2(_horizontal * this._characterSpeed, rb.velocity.y);
            
            if (_horizontal > 0f || Input.GetKey(KeyCode.D))
            {
                this.characterTransform.transform.localScale = new Vector2(-1, +1);
                this.animator.SetInteger("playerState", 1);
            }
            else if (_horizontal < 0f || Input.GetKey(KeyCode.A))
            {
                this.characterTransform.transform.localScale = new Vector2(+1, +1);
                this.animator.SetInteger("playerState", 1);
            }
            else
            {
                this.animator.SetInteger("playerState", 0);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }



        internal override void Damage(CharacterProperties characterProperties)
        {
            
        }


        /// <summary>
        /// This method will work when the character takes damage!
        /// </summary>
        /// <param name="characterProperties">Enter as a parameter which character was damaged by. Optionally, this parameter can be set as 'null'!</param>
        internal override void TakeDamage(CharacterProperties characterProperties = null)
        {
            this._dead = true;
            this.animator.SetInteger("playerState", 3);
            this.characterCollider.isTrigger = true;
            this.rb.simulated = false;
            StartCoroutine(this.gameManager.GameOverPanel());
        }



        /// <summary>
        /// Method that allows the character to jump!
        /// </summary>
        internal void Jump()
        {
            this.isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.6f,0.3f), CapsuleDirection2D.Horizontal, 0, groundLayerMask);
            if (this.isGrounded)
            {
                Debug.Log("True");
                this.rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                this.animator.SetInteger("playerState", 2);
            }
        }

    
        
    }
}