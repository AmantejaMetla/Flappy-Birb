/*
 * Created by: @feerposser
 * description: This is an unity script for handling a player controller 2D platform
 * Features included: move, dash, jump, wall slide, wall jump
 * 
 * Repository: https://github.com/feerposser/unity-2d-platform-movement
 * Gist: https://gist.github.com/feerposser/147fe370a6df710414d7c2728a96c035
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float maxFallSpeed = -20f;  // Maximum fall speed
    public float flySpeed = 10f;       // Speed when flying upwards
    public float speed = 5f;           // Horizontal movement speed
    public float gravityScale = 3f;    // Gravity scale
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = gravityScale;  // Set gravity scale
    }

    void Update()
    {
        MoveCharacter();
        LimitFallSpeed();
    }

    void MoveCharacter()
    {
        float xinput = Input.GetAxis("Horizontal");
        float yinput = Input.GetAxis("Vertical");

        Vector2 velocity = body.velocity;

        // Handle horizontal movement
        velocity.x = xinput * speed;

        // Handle flying (only positive y input for flying)
        if (yinput > 0)
        {
            velocity.y = yinput * flySpeed;
        }

        body.velocity = velocity;
    }

    public GameOverManager gameOverManager;  // Assign the GameOverManager in the Inspector

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOverManager.GameOver();  // Trigger game over
        }
    }

    void LimitFallSpeed()
    {
        if (body.velocity.y < maxFallSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, maxFallSpeed);
        }
    }
}
