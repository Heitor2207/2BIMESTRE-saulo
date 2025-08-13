using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 40;
    public float forcadopulo = 4;
    public float velocidaderolagem = 60; // velocidade extra para rolar (opcional no pulo)

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

        // Movimento para esquerda
        if (Input.GetKey(KeyCode.A))
        {
            float vel = velocidade * Time.deltaTime;
            gameObject.transform.position += new Vector3(-vel, 0, 0);
            sprite.flipX = true;
            andando = true;
        }

        // Movimento para direita
        if (Input.GetKey(KeyCode.D))
        {
            float vel = velocidade * Time.deltaTime;
            gameObject.transform.position += new Vector3(vel, 0, 0);
            sprite.flipX = false;
            andando = true;
        }

        // Pulo + rolar
        if (Input.GetKeyDown(KeyCode.Space) && nochao == true)
        {
            rb.AddForce(new Vector2(0, forcadopulo), ForceMode2D.Impulse);
            rolando = true; // ativa rolagem no ar
        }

        // Define estados no Animator
        animator.SetBool("parado", !andando && !rolando && nochao);
        animator.SetBool("andando", andando && nochao);
        animator.SetBool("rolando", rolando);
        animator.SetBool("pulo", !nochao);
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("chao"))
        {
            nochao = true;
            rolando = false; // para de rolar quando tocar no chão
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
