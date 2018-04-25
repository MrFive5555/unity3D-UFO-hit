using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_UFO : Factory<UFO> {
    new
    static public Factory_UFO getInstance() {
        if (_instance == null) {
            _instance = new Factory_UFO();
        }
        return _instance as Factory_UFO;
    }
    override
    protected UFO generateProduct() {
        UFO newUFO;
        switch (Random.Range(0, 3)) {
            case 0:
                newUFO = new UFO_Red();
                break;
            case 1:
                newUFO = new UFO_Green();
                break;
            default:
                newUFO = new UFO_Blue();
                break;
        }
        return newUFO;
    }
}

public class Factory_Arrow : Factory<Arrow> {
    new
    static public Factory_Arrow getInstance() {
        if (_instance == null) {
            _instance = new Factory_Arrow();
        }
        return _instance as Factory_Arrow;
    }
    override
    protected Arrow generateProduct() {
        return new Arrow();
    }
}