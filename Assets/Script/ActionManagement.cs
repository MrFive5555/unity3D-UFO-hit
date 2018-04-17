using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 结束时需要调用callback，并把destory设置成true
public class Action : ScriptableObject {
    public GameObject gameobject { get; set; }
    public Callback callback;

    public bool enable = true;
    public bool destroy = false; // 当动作完成后，应令destroy为true

    public virtual void Start() {
        throw new System.NotImplementedException();
    }
    public virtual void Update() {
        throw new System.NotImplementedException();
    }
    public virtual void OnGUI() {
            
    }
}

public class SequenceAction : Action, Callback {
    public List<Action> sequence;
    private int i = 0;

    public static Action getAction(List<Action> _seq) {
        SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>();
        action.sequence = _seq;
        return action;
    }

    public override void Start() {
        foreach (Action ac in sequence) {
            ac.callback = this;
        }
        if (sequence.Count != 0) {
            sequence[0].Start();
        } else {
            callback.call();
            destroy = true;
        }
    }
    public override void Update() {
        if(i < sequence.Count) {
            sequence[i].Update();
        } else {
            destroy = true;
            callback.call();
        }
    }
    public override void OnGUI() {
        if (i < sequence.Count) {
            sequence[i].OnGUI();
        }
    }
    // 子动作的回调函数
    public void call() {
        sequence[i].destroy = true;
        if (++i < sequence.Count) {
            sequence[i].Start();
        }
    }
}

public interface Callback {
    void call();
}

public class ActionManager : MonoBehaviour {
    private Dictionary<int, Action> actions = new Dictionary<int, Action>();
    private List<Action> waitingAdd = new List<Action>();
    private List<int> waitingDelete = new List<int>();

    public void Update() {
        foreach (Action ac in waitingAdd) {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();
        foreach (KeyValuePair<int, Action> kv in actions) {
            Action ac = kv.Value;
            if (ac.destroy) {
                waitingDelete.Add(kv.Key);
            } else if (ac.enable) {
                ac.Update();
            }
        }
        foreach (int key in waitingDelete) {
            Action ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }

    public void OnGUI() {
        foreach (KeyValuePair<int, Action> kv in actions) {
            Action ac = kv.Value;
            ac.OnGUI();
        }
    }
    // 执行动作
    public void RunAction(Action action) {
        waitingAdd.Add(action);
        action.Start();
    }
    // 清空动作队列
    public void Clear() {
        foreach (Action ac in waitingAdd) {
            DestroyObject(ac);
        }
        foreach (KeyValuePair<int, Action> kv in actions) {
            DestroyObject(kv.Value);
        }
        actions.Clear();
        waitingAdd.Clear();
        waitingDelete.Clear();
    }
    // 结束指定动作id号的动作(id通过ac.GetInstanceID()方法得到)
    public void Stop(int id) {
        Action ac = actions[id];
        actions.Remove(id);
        DestroyObject(ac);
    }
}

