using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Product {
    public GameObject gameobject { get; set; }
    public void recycle() {
        gameobject.SetActive(false);
        gameobject.transform.rotation = Quaternion.identity;
    }
    public Arrow () {
        gameobject = GameObject.Instantiate(Resources.Load("Prefabs/Arrow", typeof(GameObject))) as GameObject;
    }
}
