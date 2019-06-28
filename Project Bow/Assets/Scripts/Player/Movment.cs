using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public GameManager gameManager;

    public float MovementSpeed = 10;

    public Rigidbody rb;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool onGround = true;

    public Transform PlayerView;

    public float CrouchPos;
    public CapsuleCollider PlayerHitBox;
    
    private void Start() {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.S))
        {
            // moving backwards in slower then left right or forward.
            transform.Translate(Vector3.back * MovementSpeed / 1.5f * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * MovementSpeed * Time.deltaTime, Space.Self);
        }

        // May need to add the crouch part to an update and the rest to FixedUpdate if phasing problomes happen
        if (Input.GetKeyDown(KeyCode.LeftControl) && gameManager.isPaused == false)
        {
            PlayerView.transform.Translate(new Vector3(0, -CrouchPos, 0));
            PlayerHitBox.height = 0.5f;
            gameManager.isCrouched = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && gameManager.isPaused == false)
        {
            PlayerView.transform.Translate(new Vector3(0, CrouchPos, 0));
            PlayerHitBox.height = 2f;
            gameManager.isCrouched = false;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
        {
            // Add's a force to the bottom of the object to push it up wards
            rb.AddForce(Vector3.up * 400);
            onGround = false;
        }

        // Better Jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
}
