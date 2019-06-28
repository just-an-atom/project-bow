using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowShoot : MonoBehaviour
{
    public GameManager gameManager;

    public Vector3 com;

    private Rigidbody rb;
    private Animator anim;
    private AudioSource HitMarkerFX;
    public float arrowLife = 5f;
    public GameObject BleedEffect;

    private TrailRenderer tr;

    // Don't switch to Start it kinda breaks
    private void Awake() {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        GameObject HitMarkerFXObj = GameObject.FindGameObjectWithTag("Player");
        HitMarkerFX = HitMarkerFXObj.GetComponent<AudioSource>();

        GameObject animObj = GameObject.FindGameObjectWithTag("HitMarker");
        anim = animObj.GetComponent<Animator>();

        tr = this.GetComponent<TrailRenderer>();
        rb = this.GetComponent<Rigidbody>();
    }

    public void AddTrust(float thrust) {
        if (thrust <= 0.3f) {
            rb.AddForce(0, 0, 0);
        } else {
            rb.AddForce(transform.forward * thrust * 1000);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Arrow" && other.gameObject.name != "Player") {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Destroy(gameObject, arrowLife);
        }

        if (other.gameObject.tag == "Enemy") {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Destroy(gameObject, arrowLife);
            HitMarkerFX.Play();
            anim.Play("HitMarker");
            this.transform.SetParent(other.transform, true);
            tr.enabled = false;

            if(gameManager.blood == true) {
                Instantiate(BleedEffect, this.transform);
            }
        }
    }

    private void Update() {
        rb.centerOfMass = com;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * com, 0.02f);
    }
}
