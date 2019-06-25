using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowShoot : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 com;
    public Animation anim;

    private void Awake() {
        rb = this.GetComponent<Rigidbody>();
        Destroy(gameObject, 5);
    }

    public void AddTrust(float thrust) {
        rb.AddForce(transform.forward * thrust);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Arrow") {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (other.gameObject.tag = "enemy") {
            
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
