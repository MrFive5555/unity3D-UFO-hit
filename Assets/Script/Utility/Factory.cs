using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 工厂模式
 */

/**
 * 产品接口
 * 需要给工厂管理的游戏对象必须是Product接口的一个实现
 */
public interface Product {
    GameObject gameobject { get; set; }
    void recycle();
}

/**
 * 工厂的抽象类
 * 可以实现资源的发放与回收
 * 当需要某种产品的工厂时，需要把模板参数T换成需要的函数
 * 并继承该工厂类，重载getInstanse()和generateProduce()方法
 * 
 * 注意：工厂生产出来的产品的gameobject状态为unActive,
 *       在使用前需要使用product.gameobjece.setActive(true)
 *       变成可使用的状态
 * ============================================
 * getInstanse()
 * 用于返回具体工厂的实例
 * ============================================
 * generateProduce()
 * 用于返回新的产品
 */
public class Factory<T> where T : Product
{
    private Dictionary<int, T> productAvailable;
    private Dictionary<int, T> productStore;
    private List<int> idList;
    protected Factory() {
        _instance = this;
        _instance.productAvailable = new Dictionary<int, T>();
        _instance.productStore = new Dictionary<int, T>();
        _instance.idList = new List<int>();
    }
    static protected Factory<T> _instance;
    static public Factory<T> getInstance() {
        if (_instance == null) {
            _instance = new Factory<T>();
        }
        return _instance;
    }
    public T getProduct() {
        T product;
        if (idList.Count == 0) {
            product = generateProduct();
            productStore.Add(product.gameobject.GetInstanceID(), product);
        } else {
            int id = idList[Random.Range(0, idList.Count)];
            product = productAvailable[id];
            productAvailable.Remove(id);
            idList.Remove(id);
        }
        product.gameobject.SetActive(false);
        product.gameobject.transform.rotation = Quaternion.identity;
        product.gameobject.transform.position = Vector3.zero;
        return product;
    }
    protected virtual T generateProduct() {
        throw new System.NotImplementedException();
    }

    public T getProduct(int id) {
        T product = productStore[id];
        if(product != null) {
            return product;
        } else {
            throw new System.MethodAccessException();
        }
    }

    public void recycle(T ufo) {
        int id = ufo.gameobject.GetInstanceID();
        productAvailable.Add(id, ufo);
        idList.Add(id);
        ufo.recycle();
    }
}
