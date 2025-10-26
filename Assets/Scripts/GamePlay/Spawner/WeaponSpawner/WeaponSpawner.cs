using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] float minX, maxX, minY, maxY;
    GameObject weaponContainerPrefab;
    [SerializeField] List<Weapon> weaponsCanSpawn;
    [SerializeField] float timePerSpawn;
    public List<Weapon> WeaponsCanSpawn { get => weaponsCanSpawn; set => weaponsCanSpawn = value; }
    public static WeaponSpawner instance;
    ObjectPooling weaponContainerPool;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        weaponContainerPrefab = transform.Find("WeaponContainerPooling").Find("WeaponContainerPrefab").gameObject;
        weaponContainerPool = transform.Find("WeaponContainerPooling").GetComponent<ObjectPooling>();
        StartCoroutine(SpawnWeapons());
    }
    IEnumerator SpawnWeapons()
    {
        while (true)
        {
            yield return new WaitForSeconds(timePerSpawn);
            Vector3 posSpawn = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            GameObject weaponContainer = weaponContainerPool.TakeObj(posSpawn);
            weaponContainer.SetActive(true);
            // break;
        }
    }
}
