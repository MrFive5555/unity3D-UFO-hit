using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControllor : MonoBehaviour {
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("UFO") == true) {
            int id = collision.gameObject.GetInstanceID();
            UFO ufo = Factory_UFO.getInstance().getProduct(id);
            if(ufo.canHit) {
                Scorer.getInstance().addScore(ufo.Score);
            }
        }
    }
}
