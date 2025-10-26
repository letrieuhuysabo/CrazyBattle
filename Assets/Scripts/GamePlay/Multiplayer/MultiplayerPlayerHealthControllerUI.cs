using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MultiplayerPlayerHealthControllerUI : MonoBehaviour
{
    float numberOfHealth;
    [SerializeField] TextMeshProUGUI player1Health, player2Health;
    [SerializeField] Transform player1HeadUI, player2HeadUI;
    public static MultiplayerPlayerHealthControllerUI instance;
    private void Awake()
    {
        instance = this;
    }
    public int Player1CurrentHealth()
    {
        return int.Parse(player1Health.text);
    }
    public int Player2CurrentHealth()
    {
        return int.Parse(player2Health.text);
    }
    void Start()
    {
        numberOfHealth = DataForStartMultiPlayerGame.numberOfHeart;
        // hiện số mạng
        player1Health.text = numberOfHealth + "";
        player2Health.text = numberOfHealth + "";

        // hiện avatar
        
        Transform player1Head = CharacterChoisen.GetRootFromPath(CharacterChoisen.pathOfCharacter1).Root.
                                transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0);
        Transform player2Head = CharacterChoisen.GetRootFromPath(CharacterChoisen.pathOfCharacter2).Root.
                                transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0);
        ChangeSkin(player1HeadUI, player1Head);
        ChangeSkin(player2HeadUI, player2Head);
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
    public void SetHearth1(int n)
    {
        player1Health.text = n + "";
    }
    public void SetHearth2(int n)
    {
        player2Health.text = n + "";
    }
}
