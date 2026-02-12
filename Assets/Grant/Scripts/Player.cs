using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float jumpSpeed;

    Vector2 move;
    [SerializeField] private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Mathf.Lerp(move.x, Input.GetAxis("Horizontal") * moveSpeed, Time.deltaTime * moveAcceleration);
        move.y = Mathf.Lerp(move.y, Input.GetAxis("Vertical") * jumpSpeed, Time.deltaTime * fallSpeed);

        rb.velocity = move;
    }
}
