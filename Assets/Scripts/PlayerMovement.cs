using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float moveSpeed = 6.5f;
    public Rigidbody2D rb;
    public Animator playerAnimator;  // TODO: uncomment when animations are available
    private Bounds curIslandBounds;
    
    private Vector2 _movement;

    private void Start() {
        if (rb == null) {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
        GameManager.Instance.setPlayer(gameObject);
    }

    void Update() {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.goNextIsland();
        }
        // Animations control.
        playerAnimator.SetFloat("Horizontal", _movement.x);
        playerAnimator.SetFloat("Vertical", _movement.y);
        playerAnimator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    private void FixedUpdate() {
        
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * _movement);
    }
}
