using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    private Rigidbody2D Player;
    [SerializeField] private float speed;
    public float jumpForce;
    /*public float jump;*/
    private bool isJumping;
    private Animator anim;
    bool facingRight;

    [Header("layer")]
    private bool Ground;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Jump Function")]
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("coyote time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Extra jump")]
    [SerializeField] private int extraJump;
    private int jumpCounter;

    [Header("Wall Jump")]
    [SerializeField] private float walljumpX;
    [SerializeField] private float walljumpY;

    //diamond
    public DiamondManager dm;
    private int totalDiamond;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    [Header("Portal")]
    public GameObject portal;


    private void Awake()
    {
       
        
    }

    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {


        //coba kode buat flip
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0.1f)
            transform.localScale = new Vector3(3, 3, 3);
        else if (horizontalInput < -0.1f)
            transform.localScale = new Vector3(-3, 3, 3);

        anim.SetBool("Run", isGrounded() && Mathf.Abs(horizontalInput) > 0.1f);

        anim.SetBool("Ground", isGrounded());

        //kode atur jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        //kode atur ketinggian jump
        if(Input.GetKeyUp(KeyCode.Space))
            Player.velocity = new Vector2(Player.velocity.x, Player.velocity.y / 2);

        if (onWall())
        {
            Player.gravityScale = 0;
        }
        else
        {
            Player.gravityScale = 1;
            Player.velocity = new Vector2(horizontalInput * speed, Player.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJump;
            }
            else
                coyoteCounter -= Time.deltaTime;
        }



        //kode portal
        if (dm.DiamondCount >= 3)
        {
            portal.SetActive(true);
            Debug.Log(dm.DiamondCount);
        }
        if (Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
    }

    /*kode buat kekuatan jump & wall climbing*/
    private void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;
        SoundManager.instance.PlaySound(jumpSound);

        if(onWall())
            WallJump();

        else
        {
            if (isGrounded())
                Player.velocity = new Vector2(Player.velocity.x, jumpForce);
            else
            {
                if(coyoteCounter >0)
                    Player.velocity = new Vector2(Player.velocity.x, jumpForce);
                else
                {
                    if(jumpCounter > 0)
                    {
                        Player.velocity = new Vector2(Player.velocity.x, jumpForce);
                        jumpCounter--;
                    }
                }
            }
        }
        Ground = false;
        isJumping = true;
    }

    private void WallJump()
    {
        if (onWall() && !isGrounded())
        {
            Player.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * walljumpX, walljumpY));
            wallJumpCooldown = 0;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Ground = true;
            isJumping = false;
        }

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DiamondItem"))
        {
            dm.DiamondCount +=1;
            Destroy(collision.gameObject);
            Debug.Log(dm.DiamondCount);
        }
    }

}