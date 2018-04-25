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
        /*
        if (Input.GetMouseButton(0)) {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                GameObject hitObj = hitInfo.collider.gameObject;
                if (hitObj.CompareTag("UFO")) {
                    UFO ufo = Factory_UFO.getInstance().getProduct(hitObj.GetInstanceID());
                    if (ufo.canHit) {
                        ufo.canHit = false;
                        scorer.addScore(ufo.Score);
                    }
                }
            }
        }*/
        
        if (Input.GetMouseButtonDown(0)) {
            const int ARROW_WIDTH = 300; // 箭射出的初始范围
            const int ARROW_HEIGHT = 70;
            Vector3 pos = new Vector3(
                Input.mousePosition.x / Screen.width * ARROW_WIDTH - ARROW_WIDTH / 2,
                Input.mousePosition.y / Screen.height * ARROW_HEIGHT,
                0
            );
            RunAction(ShootArrow.getAction(pos, 500f));
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
