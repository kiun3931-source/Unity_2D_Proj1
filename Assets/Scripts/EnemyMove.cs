using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 5);
    }

    void FixedUpdate()
    {
        // Move
        rigid.linearVelocity = new Vector2(nextMove, rigid.linearVelocity.y);

        // Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        // Sprite Animation
        animator.SetInteger("WalkSpeed", nextMove);

        //  Flip Sprite
        if(nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        // 재귀는 마지막에 적어주는 것이 일반적
        // Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 5);
    }
}
