using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UFO : Product {
    public int Score { get; set; }
    public bool canHit { get; set; }
    public GameObject gameobject { get; set; }
    public void recycle() {
        gameobject.SetActive(false);
        gameobject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    public void show() {
        gameobject.SetActive(true);
    }
}

public class UFO_Red : UFO {
    
    public UFO_Red() {
        gameobject = GameObject.Instantiate(Resources.Load("Prefabs/UFO_Red", typeof(GameObject))) as GameObject;
    }
}

public class UFO_Blue : UFO {
    public UFO_Blue() {
        gameobject = GameObject.Instantiate(Resources.Load("Prefabs/UFO_Blue", typeof(GameObject))) as GameObject;
    }
}

public class UFO_Green : UFO {
    public UFO_Green() {
        gameobject = GameObject.Instantiate(Resources.Load("Prefabs/UFO_Green", typeof(GameObject))) as GameObject;
    }
}