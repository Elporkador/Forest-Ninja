using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float runningSpeed = 1;
    [SerializeField] int jumpForce = 5;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject mainMenu;

    Rigidbody2D rb;
    BoxCollider2D bc; 
    Vector2 v;
    SpriteRenderer sr;
    Animator a;
    bool isGrounded = false;
    bool isDead = false;
    bool isGliding = false;
    bool dropping = false;
    public static bool inMenu = true;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        a = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        a.SetBool("isDead", false);
        a.SetBool("isRunning", false);
        a.SetBool("isSliding", false);
        a.SetBool("isGliding", false);
    }

    // Update is called once per frame
    void Update()
    {
        //runningSpeed += 0.001f;
        if (!isDead && !inMenu)
            Move();
        rb.velocity = new Vector2(runningSpeed, rb.velocity.y);
    }

    void Move()
    {
        

        if (Input.GetKey(KeyCode.UpArrow) && !isGrounded && rb.velocity.y < 0 && !dropping)
        {
            isGliding = true;
            a.SetBool("isGliding", true);
            a.SetBool("isJumping", false);
            rb.velocity = new Vector2(rb.velocity.x, -2);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && isGliding)
        {
            isGliding = false;
            a.SetBool("isGliding", false);
            a.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, -5);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (isGrounded)
            {
                a.SetBool("isSliding", true);
                a.SetBool("isRunning", false);
                a.SetBool("isJumping", false);

                bc.size = new Vector2(2f, 2);
                bc.offset = new Vector2(0, -1);

                //bc.transform.position = new Vector2(bc.transform.position.x, bc.transform.position.y - 0.2f);
            }
            else if (rb.velocity.y < 0)
            {
                dropping = true;
                isGliding = false;
                a.SetBool("isGliding", false);
                a.SetBool("isJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*1.05f);
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            a.SetBool("isSliding", false);

            bc.offset = new Vector2(0, 0);
            //bc.transform.position = new Vector2(bc.transform.position.x, bc.transform.position.y + 0.2f);
            bc.size = new Vector2(2f, 4);

            if (isGrounded)
                a.SetBool("isRunning", true);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Die();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dangerous")
            Die();
        if (collision.contacts[0].normal.y > -0.5)
        {
            //a.Play("NinjaRunning");
            a.SetBool("isRunning", true);
            a.SetBool("isJumping", false);
            a.SetBool("isGliding", false);
            isGrounded = true;
            isGliding = false;
            dropping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bc.offset = new Vector2(0, 0);
        bc.size = new Vector2(3.5f, 4);

        a.SetBool("isRunning", false);
        a.SetBool("isSliding", false);
        a.SetBool("isJumping", true);
        isGrounded = false;
    }

    void Die()
    {
        isDead = true;
        a.SetBool("isDead", true);
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(DieAfterDelay());
        particleSystem.Play();
        Debug.Log("Particles");
    }

    private IEnumerator DieAfterDelay()
    {
        yield return new WaitForSeconds(1);
        deathScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        deathScreen.SetActive(false);
        SceneManager.LoadScene("Scene1");
    }

    public void StartGame()
    {
        inMenu = false;
        mainMenu.SetActive(false);
    }
}
