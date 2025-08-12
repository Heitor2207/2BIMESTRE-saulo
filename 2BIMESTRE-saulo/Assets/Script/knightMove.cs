using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 40;
    public float forcadopulo = 4;
    public float velocidaderolagem = 60; // velocidade extra para rolar

    private bool nochao = false;
    private bool andando = false;
    private bool rolando = false;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        andando = false;
        rolando = false;

        // Movimento para esquerda
        if (Input.GetKey(KeyCode.A))
        {
            float vel = velocidade * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift)) // rolar
            {
                vel = velocidaderolagem * Time.deltaTime;
                rolando = true;
            }
            gameObject.transform.position += new Vector3(-vel, 0, 0);
            sprite.flipX = true;
            andando = !rolando;
        }

        // Movimento para direita
        if (Input.GetKey(KeyCode.D))
        {
            float vel = velocidade * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift)) // rolar
            {
                vel = velocidaderolagem * Time.deltaTime;
                rolando = true;
            }
            gameObject.transform.position += new Vector3(vel, 0, 0);
            sprite.flipX = false;
            andando = !rolando;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && nochao == true)
        {
            rb.AddForce(new Vector2(0, forcadopulo), ForceMode2D.Impulse);
        }

        // Define estados no Animator
        animator.SetBool("parado", !andando && !rolando && nochao);
        animator.SetBool("andando", andando);
        animator.SetBool("rolando", rolando);
        animator.SetBool("pulo", !nochao);
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("chao"))
        {
            nochao = true;
        }
    }

    void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("chao"))
        {
            nochao = false;
        }
    }
}
