using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : ActionManager, Callback {
    private Scorer scorer;
    private int round;
    private bool isReady = false;

    // next round callback
    public void call() {
        if (round < 4) {
            ++round;
            Action ac = newRound.getAction(round);
            ac.callback = this;
            RunAction(ac);
        } else {
            Action ac = OverAction.getAction(scorer.getScore());
            ac.callback = null;
            RunAction(ac);
        }
    }

    // Use this for initialization
    void Start () {
        scorer = Scorer.getInstance();
        round = 0;
        isReady = true;
	}
	
	// Update is called once per frame
	new void Update () {
        if(isReady) {
            Action ac = CountBack.getAction();
            ac.callback = this;
            RunAction(ac);
            isReady = false;
        }
        if (Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                GameObject hitObj = hitInfo.collider.gameObject;
                if (hitObj.CompareTag("UFO")) {
                    UFO ufo = Factory_UFO.getInstance().getUFOInstance(hitObj.GetInstanceID());
                    if (ufo.canHit) {
                        ufo.canHit = false;
                        scorer.addScore(ufo.Score);
                    }
                    hitObj.GetComponent<Rigidbody>().AddForce(new Vector3(
                            Random.Range(-1, 1),
                            -1f,
                            Random.value
                        ) * 3000
                    );
                }
            }
        }
        base.Update();
    }

    new void OnGUI() {
        base.OnGUI();
        if(GUI.Button(View.buttonPos, "重新开始", View.ButtonStyle())) {
            round = 0;
            scorer.clear();
            isReady = true;
            Clear();
        }
        GUI.Label(View.scorePos, "Score : " + scorer.getScore(), View.scoreStyle());
    }
}
