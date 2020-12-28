using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public GameDriver gameDriver;

    public KeyCode interactKey;
    public KeyCode zoomOutKey;

    [Range(0.0f, 10.0f)]
    public float moveSpeed = 6.5f;
    public const float Max_Zoom_Out = 10;
    public const float Default_Zoom = 5;

    public Rigidbody2D rb;
    public Animator playerAnimator;
    public CinemachineVirtualCamera playerCamera;

    private Vector2 _movement;
    private bool isZooming = false;

    public Image inventoryImage;
    public Sprite emptyInventory;
    private GameObject inventory;
    private GameObject curNearPickupFix;
    private GameObject curNearStaticFix;
    private GameObject curNearMalFunction;

    public AudioSource movementSounds;
    public AudioSource itemSounds;
    public AudioClip pickup;
    public AudioClip dropoff;

    private void Start() {
        if (rb == null) {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void Update() {
        if (!isZooming) {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
        }

        // Movement sounds
        if (_movement.x == 0f && _movement.y == 0f) {
            movementSounds.Stop();
        } else if (!movementSounds.isPlaying) {
            movementSounds.Play();
        }

        if (Input.GetKeyDown(interactKey)) {
            interact();
        }
        if (Input.GetKey(zoomOutKey) && playerCamera.m_Lens.OrthographicSize < Max_Zoom_Out) {
            isZooming = true;
            float newSize = Mathf.Min(playerCamera.m_Lens.OrthographicSize + 0.1f, Max_Zoom_Out);
            playerCamera.m_Lens.OrthographicSize = newSize;
        }
        if (!Input.GetKey(zoomOutKey) && playerCamera.m_Lens.OrthographicSize > Default_Zoom) {
            isZooming = false;
            float newSize = Mathf.Max(playerCamera.m_Lens.OrthographicSize - 0.2f, Default_Zoom);
            playerCamera.m_Lens.OrthographicSize = newSize;
        }
        // Animations control.
        playerAnimator.SetFloat("Horizontal", _movement.x);
        playerAnimator.SetFloat("Vertical", _movement.y);
        playerAnimator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    private void FixedUpdate() {
        if (!isZooming) {
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * _movement);
        }
    }

    private void interact() {
        if (curNearMalFunction) {  // If near a malfunction object
            // Check the malfunction. If it doesnt require an item:
            // If this is the current malfunction, solve(true). Else solve(false).
            GameObject fix = gameDriver.curMalfunction.fixObject;
            if (fix.Equals(null)) {
                if (gameDriver.curMalfunction.malfunctionObject.Equals(curNearMalFunction)) {
                    gameDriver.solve(true);
                }
                else {
                    gameDriver.solve(false);
                }
            } 
            // Else, if it does require an item but not a pickup, solve(false).
            else if (fix.tag.Equals("StaticFixes")) {
                gameDriver.solve(false);
            }
            // Else, if it does require an item that is picked up:
            //     - If the item is in the inventory: remove item from inventory, re-enable it's image and solve(true).
            //     - Else solve(false).
            else if (fix.tag.Equals("PickupFixes")) {
                if (inventory && fix.Equals(inventory)) {
                    // Play dropoff sound
                    itemSounds.clip = dropoff;
                    itemSounds.Play();
                    
                    inventory = null;
                    inventoryImage.sprite = emptyInventory;
                    fix.SetActive(true);
                    gameDriver.solve(true);
                }
                else {
                    gameDriver.solve(false);
                }
            }
        } else if (curNearPickupFix) {  // Else, if near a fix object
            // Check if there's already an item in the inventory.
            // if so re-enable it's image.
            if (inventory) {
                inventory.SetActive(true);
            }
            // Either way, put the new item in the inventory and disable it's image.
            inventory = curNearPickupFix;
            curNearPickupFix.SetActive(false);
            // Update inventory image
            inventoryImage.sprite = inventory.GetComponent<SpriteRenderer>().sprite;
            // Play pickup sound
            itemSounds.clip = pickup;
            itemSounds.Play();
        } else if (curNearStaticFix) {  // Else, if near a static fix
            // Check the malfunction fix. If it doesnt require an item, solve(false).
            GameObject fix = gameDriver.curMalfunction.fixObject;
            if (fix.Equals(null)) {
                gameDriver.solve(false);
            }
            // Else, if requires a static fix, and it's the correct one - solve(true).
            else if (fix.tag.Equals("StaticFixes") && fix.Equals(curNearStaticFix)) {
                gameDriver.solve(true);
            }
            // Else, solve(false)
            else {
                gameDriver.solve(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("PickupFixes")) {
            curNearPickupFix = other.gameObject;
            other.gameObject.GetComponent<Animator>().SetTrigger("EnteredRange");
        } else if (other.gameObject.tag.Equals("StaticFixes")) {
            curNearStaticFix = other.gameObject;
            other.gameObject.GetComponent<Animator>().SetTrigger("EnteredRange");
        } else if (other.gameObject.tag.Equals("Malfunctions")) {
            curNearMalFunction = other.gameObject;
            other.gameObject.GetComponent<Animator>().SetTrigger("EnteredRange");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("PickupFixes")) {
            curNearPickupFix = null;
            other.gameObject.GetComponent<Animator>().SetTrigger("LeftRange");
        } else if (other.gameObject.tag.Equals("StaticFixes")) {
            curNearStaticFix = null;
            other.gameObject.GetComponent<Animator>().SetTrigger("LeftRange");
        } else if (other.gameObject.tag.Equals("Malfunctions")) {
            curNearMalFunction = null;
            other.gameObject.GetComponent<Animator>().SetTrigger("LeftRange");
        }
    }
}
