using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector2 eelSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = eelSpeed;

    }
}
