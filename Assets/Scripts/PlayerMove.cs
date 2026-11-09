using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxSpeed;
    SpriteRenderer spriteRenderer;
    Animator animator;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {

            rigid.linearVelocity = new Vector2(rigid.linearVelocity.normalized.x * 0.8f, rigid.linearVelocity.y);
        }

        // 방향 전환
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1 ;
        }

        // 애니메이션 Walk
        if(Mathf.Abs(rigid.linearVelocity.x)< 0.3)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }
    void FixedUpdate()
    {
        // Move
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigid.linearVelocity.x > maxSpeed)
        {
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        }
        else if (rigid.linearVelocity.x < maxSpeed * (-1))
        {
            rigid.linearVelocity = new Vector2(maxSpeed * (-1), rigid.linearVelocity.y);
        }
    }
}
