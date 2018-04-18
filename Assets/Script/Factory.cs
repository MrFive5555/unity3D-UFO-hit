using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_UFO {
    private Dictionary<int, UFO> UFO_available;
    private Dictionary<int, UFO> UFO_store;
    private List<int> idList;
    private Factory_UFO() {

    }
    static private Factory_UFO _instance;
    static public Factory_UFO getInstance() {
        if(_instance == null) {
            _instance = new Factory_UFO();
            _instance.UFO_available = new Dictionary<int, UFO>();
            _instance.UFO_store = new Dictionary<int, UFO>();
            _instance.idList = new List<int>();
        }
        return _instance;
    }

    public UFO getUFOInstance() {
        if(idList.Count == 0) {
            UFO newUFO;
            switch(Random.Range(0, 3)) {
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
            newUFO.hide();
            UFO_store.Add(newUFO.gameobject.GetInstanceID(), newUFO);
            return newUFO;
        } else {
            int id = idList[Random.Range(0, idList.Count)];
            UFO product = UFO_available[id];
            product.hide();
            UFO_available.Remove(id);
            idList.Remove(id);
            return product;
        }
    }

    public UFO getUFOInstance(int id) {
        return UFO_store[id];
    }

    public void Recovery(UFO ufo) {
        int id = ufo.gameobject.GetInstanceID();
        UFO_available.Add(id, ufo);
        idList.Add(id);
        ufo.hide();
    }
}
