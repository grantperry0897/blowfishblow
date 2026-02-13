using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxFallSpeed;
    [Header("Collision")]
    [SerializeField] private float stunDuration;
    [SerializeField] private float bounceDistance;
    [SerializeField] private float bounceSpeed;
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    private bool isStunned;
    private Vector2 move;
    private Vector2 target;

    void Update()
    {
        if (!isStunned) // normal movement
        {
            // falling
            move.y -= fallSpeed * Time.deltaTime;
            move.y = Mathf.Clamp(move.y, -maxFallSpeed, Mathf.Infinity);

            // jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                move.y = jumpSpeed;
            }

            // moving
            move.x = Mathf.Lerp(move.x, Input.GetAxis("Horizontal") * moveSpeed, Time.deltaTime * moveAcceleration);
        }
        else // stunned movement
        {
            transform.position = Vector3.Lerp(transform.position, target, bounceSpeed*Time.deltaTime);
        }

        // apply velocity
        rb.velocity = move;
    }

    // collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall" && !isStunned)
        {
            StartCoroutine(StunCooldown());
            target = collision.GetContact(0).point + collision.GetContact(0).normal * bounceDistance;
            move = Vector2.zero;
        }
    }

    // stun cooldown
    IEnumerator StunCooldown()
    {
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }

}