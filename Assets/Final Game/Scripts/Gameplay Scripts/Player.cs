using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float maxFastFallSpeed;
    [Header("Collision")]
    [SerializeField] private float stunDuration;
    [SerializeField] private int stunSpins;
    [SerializeField] private float bounceSpeed;
    [SerializeField] private float bounceAccleration;
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform sprite;
    [SerializeField] private Animator animator;
    [Header("Expand")]
    [SerializeField] private float sizeDecay;
    [SerializeField] private float sizeDecayFastFallMultiplier;
    [SerializeField] private float sizeIncrease;
    [SerializeField] private float sizeJumpSpeedMultiplier;
    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip bounceSound;
    private bool isStunned;
    private Vector2 move;
    private Vector2 target;
    private float size;
    private float sizeDecayMultiplier;
    private int pearlCount;

    void Update()
    {
        if (!isStunned) // normal movement
        {
            // falling
            move.y -= fallSpeed * Time.deltaTime;
            if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                move.y = Mathf.Clamp(move.y, -maxFastFallSpeed, Mathf.Infinity);
                sizeDecayMultiplier = sizeDecayFastFallMultiplier;
            }
            else
            {
                move.y = Mathf.Clamp(move.y, -maxFallSpeed, Mathf.Infinity);
                sizeDecayMultiplier = 1;
            }

            // jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                move.y = jumpSpeed + (size - 1) * sizeJumpSpeedMultiplier;
                size += sizeIncrease;
                audioSource.PlayOneShot(jumpSound);
            }

            // moving
            move.x = Mathf.Lerp(move.x, Input.GetAxis("Horizontal") * moveSpeed, Time.deltaTime * moveAcceleration);
            sprite.rotation = Quaternion.identity;

            // animation
            if(Input.GetAxis("Horizontal") != 0)
            {
                animator.Play("swimming");
            }
            else    
            {
                animator.Play("idle");
            }

        }
        else // stunned movement
        {
            move = Vector2.Lerp(move, Vector2.zero, bounceAccleration * Time.deltaTime);
            sprite.Rotate(Vector3.forward * (stunSpins / stunDuration * 360) * Time.deltaTime);
            sizeDecayMultiplier = 1;
        }

        // sprite flipping
        if(move.x > 0)
        {
            sprite.localScale = new Vector3(1, 1, 1);
        }
        else if(move.x < 0)
        {
            sprite.localScale = new Vector3(-1, 1, 1);
        }

        // apply velocity
        rb.velocity = move;

        // player scaling
        size -= sizeDecay * Time.deltaTime * sizeDecayMultiplier;
        size = Mathf.Clamp(size, 1, Mathf.Infinity);
        transform.localScale = new Vector3(size, size, size);
    }

    // collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall" && !isStunned)
        {
            StartCoroutine(StunCooldown());
            move = collision.GetContact(0).normal * bounceSpeed;
            audioSource.PlayOneShot(bounceSound);
        }
    }

    // stun cooldown
    IEnumerator StunCooldown()
    {
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }

    // player triggers
    void OnTriggerEnter2D(Collider2D collider)
    {
        // eel kills player
        if (collider.tag == "Eel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            pearlCount = 0;
        }
        // pearl pickup
        if (collider.tag == "Pearl")
        {
            Destroy(collider.gameObject);
            pearlCount += 1;
        }
    }

   public int GetPearlCount()
    {
       return pearlCount;
    }

}