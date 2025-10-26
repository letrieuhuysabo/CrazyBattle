using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ObjectPooling : MonoBehaviour
{
    protected Queue<GameObject> objs;
    [SerializeField] protected int numberOfObject;
    [SerializeField] protected GameObject prefab;
    void Start()
    {
        objs = new();
        for (int i = 0; i < numberOfObject; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(transform, false);
            objs.Enqueue(obj);
        }
    }
    public GameObject TakeObj()
    {
        GameObject obj;
        if (objs.Count > 0)
        {
            obj = objs.Dequeue();
        }
        else
        {
            obj = Instantiate(prefab);
        }
        obj.transform.SetParent(null, false);
        obj.SetActive(true);
        return obj;
    }
    public GameObject TakeObj(Vector3 pos)
    {
        GameObject obj = TakeObj();
        obj.transform.position = pos;
        return obj;
    }
    public void ReturnObj(GameObject obj, float delay = 0)
    {
        StartCoroutine(ReturnObjCoroutine(obj, delay));
    }
    IEnumerator ReturnObjCoroutine(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null && obj.activeSelf)
        {
            objs.Enqueue(obj);
            obj.transform.SetParent(transform, false);
            obj.SetActive(false);
        }
    }
}
