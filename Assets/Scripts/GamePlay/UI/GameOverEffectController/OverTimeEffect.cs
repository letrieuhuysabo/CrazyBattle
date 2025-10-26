using UnityEngine;

public class OverTimeEffect : MonoBehaviour
{
    public static OverTimeEffect instance;
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
        GameOverEffectController.instance.GetComponent<Animator>().SetTrigger("TimeUp");
    }
    public void CloseEffect()
    {
        effect.SetActive(false);
    }
}
