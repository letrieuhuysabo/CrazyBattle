
using UnityEngine;

public class LostAllHearthEffect : MonoBehaviour
{
    public static LostAllHearthEffect instance;
    GameObject effect;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        effect = transform.GetChild(0).gameObject;
        
    }
    public void ShowEffect()
    {
        effect.SetActive(true);
        GameOverEffectController.instance.GetComponent<Animator>().SetTrigger("LostAllHearth");
    }
}
