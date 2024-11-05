using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;
    public MeleeAttack meleeAttack;
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private bool canMove = true;
    private bool isMeleeMode = false;

    public bool IsMeleeMode => isMeleeMode;

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMeleeMode();
            Debug.Log("Switched to " + (isMeleeMode ? "Melee" : "Ranged"));
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (isMeleeMode)
            {
                meleeAttack.Attack();
            }
            else
            {
                weapon.Fire();
                Debug.Log("Current Ammo: " + weapon.currentAmmo);
            }
            

        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void ToggleMeleeMode()
    {
        isMeleeMode = !isMeleeMode;

        // Toggle the weapon's visibility based on melee mode
        Renderer weaponRenderer = weapon.GetComponent<Renderer>();
        if (weaponRenderer != null)
        {
            weaponRenderer.enabled = !isMeleeMode;
        }
    }
    private void FixedUpdate()
    {
        if(canMove)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void DisableMovement()
    {
        canMove = false; // Disable player movement
    }

    public void EnableMovement()
    {
        canMove = true; // Enable player movement (if needed for respawn or other conditions)
    }
}
