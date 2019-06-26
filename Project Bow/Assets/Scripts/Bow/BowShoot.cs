using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowShoot : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 com;
    public Animator anim;
    public AudioSource HitMarkerFX;

    private void Awake() {
        GameObject HitMarkerFXObj = GameObject.FindGameObjectWithTag("Player");
        HitMarkerFX = HitMarkerFXObj.GetComponent<AudioSource>();

        GameObject animObj = GameObject.FindGameObjectWithTag("HitMarker");
        anim = animObj.GetComponent<Animator>();

        rb = this.GetComponent<Rigidbody>();
        Destroy(gameObject, 5);
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
        }

        if (other.gameObject.tag == "Enemy") {
            HitMarkerFX.Play();
            anim.Play("HitMarker");
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
