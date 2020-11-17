using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animationPlayer;

    //Bibliotecas de movimentação e pulo
    private float move;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private bool jumping;
    [SerializeField] private float jumpSpeed = 4f;
    [SerializeField] private float ghostJump;
    [SerializeField] private Joystick _joystick;

    //Bibliotecas de reconhecimento do chão
    [SerializeField] private bool isGrounded;
    public Transform feetPosition;
    [SerializeField] private Vector2 sizeCapsule;
    [SerializeField] private float angleCapsule = -80f;
    public LayerMask whatIsGround;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animationPlayer = GetComponent<Animator>();

        sizeCapsule = new Vector2(0.3728266f, 0.02194655f);
    }

    void Update()
    {

        // Reconhecer o chao com capscolider
        isGrounded = Physics2D.OverlapCapsule(feetPosition.position, sizeCapsule, CapsuleDirection2D.Horizontal, angleCapsule, whatIsGround);
        Debug.Log(isGrounded);


        //Input de Movimentação do personagem Horizontal
        if (_joystick.Horizontal > 0)
            move = 1 * moveSpeed;
        else if (_joystick.Horizontal < 0)
            move = -1 * moveSpeed;
        else
            move = 0 * moveSpeed;

        // input do pulo do personagem 
        
       // if (Input.GetButtonDown("Jump") || _joystick.Vertical > 0)
        //{
            //jumping = true;
                 //}

        // inverter a posição do personagem! <- -> 
        if (move < 0)
        {
            sprite.flipX = true;
        }

        else if (move > 0)
        {
            sprite.flipX = false;
        }

        if (isGrounded)
        {

            ghostJump = 0.1f;

            animationPlayer.SetBool("JumpingV", false);
            animationPlayer.SetBool("JumpingH", false);
            animationPlayer.SetBool("FallingV", false);
            animationPlayer.SetBool("FallingH", false);

            if (rb.velocity.x != 0 && move != 0)
            {

                animationPlayer.SetBool("Walking", true);
            }
            else
            {
                animationPlayer.SetBool("Walking", false);
            }
        }

        else
        {

            ghostJump -= Time.deltaTime;

            if (ghostJump <= 0)
            {
                ghostJump = 0;
            }

            if (rb.velocity.x == 0)
            {
                animationPlayer.SetBool("Walking", false);
            }

            if (rb.velocity.y > 0)
            {

                animationPlayer.SetBool("JumpingV", true);
                animationPlayer.SetBool("JumpingH", false);
                animationPlayer.SetBool("FallingV", false);
                animationPlayer.SetBool("FallingH", false);
            }
            if (rb.velocity.y < 0)
            {

                animationPlayer.SetBool("JumpingV", false);
                animationPlayer.SetBool("FallingV", true);
                animationPlayer.SetBool("FallingH", false);
                animationPlayer.SetBool("JumpingH", false);
            }

            else
            {

                if (rb.velocity.y > 0)
                {

                    animationPlayer.SetBool("JumpingV", false);
                    animationPlayer.SetBool("JumpingH", true);
                    animationPlayer.SetBool("FallingH", false);
                    animationPlayer.SetBool("FallingV", false);
                }

                if (rb.velocity.y < 0)
                {

                    animationPlayer.SetBool("JumpingH", false);
                    animationPlayer.SetBool("JumpingV", false);
                    animationPlayer.SetBool("FallingH", true);
                    animationPlayer.SetBool("FallingV", false);
                }
            }
        }
    }

    

    void FixedUpdate()
    {

        //Movimentação do Personagem atraves do teclado/Joystick
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Pulo personagem
        if (jumping)
        {

            rb.velocity = Vector2.up * jumpSpeed;
           

            // desativar pulo
            jumping = false;

        
            }
        }
    void OnCollisionEnter2D (Collision2D collision)
    {
        
        
        
        if (collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Serra")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

    }
    
    public void Pulo()
    {
        Debug.Log("Teste");
        
            //jumping = true;
        if (isGrounded )
        {
            jumping = true;
        }
        
    }
}