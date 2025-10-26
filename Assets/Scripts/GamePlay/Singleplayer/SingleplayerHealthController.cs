using TMPro;
using UnityEngine;

public class SingleplayerHealthController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textHealth;
    [SerializeField] Transform player1HeadUI;
    public static SingleplayerHealthController instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Transform player1Head = CharacterChoisen.GetRootFromPath(CharacterChoisen.pathOfCharacter1).Root.
                                transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0);
        ChangeSkin(player1HeadUI, player1Head);
    }
    public void ShowHearth(int hearth)
    {
        textHealth.text = hearth + "";
    }
    void ChangeSkin(Transform oldRoot, Transform newRoot)
    {
        
        SpriteRenderer oldRootRenderer = oldRoot.GetComponent<SpriteRenderer>();
        SpriteRenderer newRootRenderer = newRoot.GetComponent<SpriteRenderer>();
        if (oldRootRenderer != null)
        {
            // Debug.Log("hello");
            oldRootRenderer.sprite = newRootRenderer.sprite;

            oldRootRenderer.color = newRootRenderer.color;

        }
        if (oldRoot.childCount == newRoot.childCount)
        {
            for (int i = 0; i < oldRoot.childCount; i++)
            {
                ChangeSkin(oldRoot.GetChild(i), newRoot.GetChild(i));
            }
        }
    }
}
