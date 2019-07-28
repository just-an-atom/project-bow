using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    private float Health = 100;

    private void Update() {
        if (Health <= 0) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Arrow") {
            Health = Health - 25;
        }
    }
}
