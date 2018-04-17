using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CountBack : Action {
    private float time = 4;
    private string str = "";
    static public Action getAction() {
        Action ac = ScriptableObject.CreateInstance<CountBack>();
        return ac;
    }
    override
    public void Start() {
        
    }
    override
    public void Update() {
        time -= Time.deltaTime;
        if(time > 3) {
            str = "3";
        } else if(time > 2) {
            str = "2";
        } else if(time > 1) {
            str = "1";
        } else if(time > 0) {
            str = "Go";
        } else {
            callback.call();
            destroy = true;
        }
    }
    override
    public void OnGUI() {
        GUI.Label(View.LabelPos, str, View.LabelStyle());
    }
}

class ThrowUFO : Action {
    public UFO ufo;
    private static int maxDistance = 100;
    private float speed;
    private Factory_UFO factory;
    public static Action getAction(int score, float _speed) {
        ThrowUFO action = ScriptableObject.CreateInstance<ThrowUFO>();
        action.factory = Factory_UFO.getInstance();
        action.ufo = action.factory.getUFOInstance();
        action.speed = _speed;
        action.ufo.Score = score;
        action.ufo.canHit = true;
        action.gameobject = action.ufo.gameobject;
        return action;
    }
    override
    public void Start() {
        float x_pos = Random.Range(8, 10);
        float x_vel = Random.Range(-1, 0);
        if (Random.value < 0.5) { // 左边抛出
            x_pos *= -1;
            x_vel *= -1;
        }
        ufo.gameobject.transform.position = new Vector3(
            x_pos,
            Random.Range(5, 14),
            0
        );
        ufo.gameobject.GetComponent<Rigidbody>().velocity = new Vector3(
            x_vel,
            Random.Range(-0.3f, 0.3f),
            1
        ) * speed;
    }
    override
    public void Update () {
        if(gameobject.transform.position.z > maxDistance
            || gameobject.transform.position.y < 0) {
            destroy = true;
            callback.call();
            factory.Recovery(ufo);
        }
    }
}

class newRound : SequenceAction {
    private class roundHint : Action {
        private string indexStr;
        private float time;
        static public Action getAction(int roundIndex) {
            roundHint ac = ScriptableObject.CreateInstance<roundHint>();
            ac.indexStr = "Round " + roundIndex.ToString();
            return ac;
        }
        override
        public void Start() {
            time = 1;
        }
        override
        public void Update() {
            if(time > 0) {
                time -= Time.deltaTime;
            } else {
                destroy = true;
                callback.call();
            }
        }
        override 
        public void OnGUI() {
            GUI.Label(View.LabelPos, indexStr, View.LabelStyle());
        }
    }
    static public Action getAction(int roundNum) {
        List<Action> acList = new List<Action>();
        acList.Add(roundHint.getAction(roundNum));
        switch(roundNum) {
            case 1:
                for (int times = 3; times-- != 0;)
                    acList.Add(ThrowUFO.getAction(1, 20));
                break;
            case 2:
                for (int times = 3; times-- != 0;)
                    acList.Add(ThrowUFO.getAction(2, 20));
                break;
            case 3:
                for (int times = 5; times-- != 0;)
                    acList.Add(ThrowUFO.getAction(5, 30));
                break;
            case 4:
                for(int times = 10; times-- != 0; )
                    acList.Add(ThrowUFO.getAction(5, 40));
                break;
        }
        return SequenceAction.getAction(acList);
    }
}

class OverAction : Action {
    string score;
    public static Action getAction(int _score) {
        OverAction ac = ScriptableObject.CreateInstance<OverAction>();
        ac.score = _score.ToString();
        return ac;
    }
    override public void Start() {

    }
    override public void Update() {

    }
    override public void OnGUI() {
        GUI.Label(View.LabelPos, score, View.LabelStyle());
    }
}