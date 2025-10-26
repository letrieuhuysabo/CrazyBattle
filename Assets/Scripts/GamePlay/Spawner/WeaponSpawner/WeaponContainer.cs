using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    Weapon weapon;
    SpriteRenderer spriteRenderer;
    [SerializeField] ObjectPooling weaponContainerPool;
    public Weapon Weapon { get => weapon; set => weapon = value; }

    void Start()
    {
        spriteRenderer = transform.Find("Weapon").GetComponent<SpriteRenderer>();
    }
    async void OnEnable()
    {
        await WaitTask.WaitForSeconds(0.05f);
        // lấy data của weapon đc spawn
        List<Weapon> listWeapons = WeaponSpawner.instance.WeaponsCanSpawn;
        int rand = Random.Range(0, listWeapons.Count);
        rand = 3;
        // thiết lập thuộc tính
        weapon = listWeapons[rand].Clone();
        weapon.Durability = Random.Range(1, 101);
        weaponContainerPool.ReturnObj(gameObject, 15);
        // đổi sprite
        spriteRenderer.sprite = weapon.WeaponSprite;
        // đổi aura
        GameObject aura = Instantiate(weapon.Aura);
        aura.SetActive(true);
        aura.transform.SetParent(transform, false);
        aura.transform.localPosition = Vector3.zero;


    }
    public void ReturnToPool()
    {
        weaponContainerPool.ReturnObj(gameObject);
    }
}
