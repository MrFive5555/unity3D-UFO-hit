using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UFO {
    int Score {get; set;}
    GameObject gameobject { get; set; }
    bool canHit { get; set; }
}

public class UFO_Red : UFO {
    public int Score { get; set; }
    public GameObject gameobject { get; set; }
    public bool canHit { get; set; }
    public UFO_Red() {
        gameobject = GameObject.Instantiate(Resources.Load("Prefabs/UFO_Red", typeof(GameObject))) as GameObject;
    }
}

public class UFO_Blue : UFO {
    public int Score { get; set; }
    public GameObject gameobject { get; set; }
    public bool canHit { get; set; }
    public UFO_Blue() {
        gameobject = GameObject.Instantiate(Resources.Load("Prefabs/UFO_Blue", typeof(GameObject))) as GameObject;
    }
}

public class UFO_Green : UFO {
    public int Score { get; set; }
    public GameObject gameobject { get; set; }
    public bool canHit { get; set; }
    public UFO_Green() {
        gameobject = GameObject.Instantiate(Resources.Load("Prefabs/UFO_Green", typeof(GameObject))) as GameObject;
    }
}