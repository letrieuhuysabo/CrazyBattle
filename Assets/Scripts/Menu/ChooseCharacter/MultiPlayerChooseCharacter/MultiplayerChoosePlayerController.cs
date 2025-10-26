using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerChoosePlayerController : MonoBehaviour
{
    [SerializeField] List<string> rootContainerPaths;
    [SerializeField] Transform iconCharacterContainer, iconPrefab;
    [SerializeField] RectTransform player1, player2;
    [SerializeField] GameObject bg;
    public static MultiplayerChoosePlayerController instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // tạo các icon
        for (int i = 0; i < rootContainerPaths.Count; i++)
        {

            GameObject iconContainer = Instantiate(iconPrefab.gameObject);
            iconContainer.transform.SetParent(iconCharacterContainer.transform, false);
            iconContainer.SetActive(true);
            iconContainer.GetComponent<ChooseThisCharacter>().Path = rootContainerPaths[i];

            RootContainer rootContainer = CharacterChoisen.GetRootFromPath(rootContainerPaths[i]);
            if (rootContainer == null)
            {
                Debug.LogError("Wrong Path In MultiplayerChoosePlayerController");
            }
            GameObject dauCuaRoot = LayDauCuaRoot(rootContainer);
            // tạo icon
            GameObject icon = Instantiate(dauCuaRoot);

            icon.transform.SetParent(iconContainer.transform, false);
            RectTransform iconRect = icon.AddComponent<RectTransform>();
            iconRect.localScale = new Vector3(75, 75, 0);
            icon.SetActive(true);
            ConvertToUIElement(icon.transform);
            
        }
    }
    GameObject LayDauCuaRoot(RootContainer root)
    {
        return root.Root.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).gameObject;
    }
    public void ConvertToUIElement(Transform icon)
    {
        if (icon.gameObject.name == "P_Head(Clone)" || icon.gameObject.name == "P_Head")
        {
            // đẩy mái tóc xuống child bên dưới để hiện tóc
            icon.GetChild(0).SetSiblingIndex(icon.childCount - 2);
        }
        GameObject iconGobj = icon.gameObject;
        // thêm rectTransform
        if (iconGobj.GetComponent<RectTransform>() == null)
        {
            iconGobj.AddComponent<RectTransform>();
        }
        // đổi spriteRenderer thành image
        SpriteRenderer spriteRenderer = iconGobj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            Image image = iconGobj.AddComponent<Image>();
            image.sprite = spriteRenderer.sprite;
            image.color = spriteRenderer.color;
            Destroy(spriteRenderer);
        }
        // tiếp tục thực hiện với các thành phần con
        for (int i = 0; i < iconGobj.GetComponent<RectTransform>().childCount; i++)
        {
            ConvertToUIElement(iconGobj.GetComponent<RectTransform>().GetChild(i).transform);
        }
    }
    public void StartGame()
    {
        // mở bg để vô hiệu hóa tất cả nút
        bg.SetActive(true);
        // mở anim attack, tạo hiệu ứng vào trận
        player1.Find("UnitRoot").GetComponent<Animator>().SetTrigger("2_Attack");
        player2.Find("UnitRoot").GetComponent<Animator>().SetTrigger("2_Attack");
        StartCoroutine(Move2PlayersNearer());
    }
    IEnumerator Move2PlayersNearer()
    {
        RectTransform p1 = player1.Find("UnitRoot").GetComponent<RectTransform>();
        RectTransform p2 = player2.Find("UnitRoot").GetComponent<RectTransform>();
        Vector2 startPos = p1.anchoredPosition;
        Vector2 secondPosition = new(-35, 78);
        Vector2 finalPosition = new(-225.2f, -6.8f);
        float duration = 0;
        // bay lên
        while (duration < 0.08f)
        {
            p1.anchoredPosition = Vector2.Lerp(startPos, secondPosition, duration / 0.08f);
            p2.anchoredPosition = Vector2.Lerp(startPos, secondPosition, duration / 0.08f);
            duration += Time.deltaTime;
            yield return null;
        }
        p1.anchoredPosition = secondPosition;
        p2.anchoredPosition = secondPosition;
        // hạ xuống
        // Time.timeScale = 0.1f;
        duration = 0;
        while (duration < 0.16f)
        {
            p1.anchoredPosition = Vector2.Lerp(secondPosition, finalPosition, duration / 0.16f);
            p2.anchoredPosition = Vector2.Lerp(secondPosition, finalPosition, duration / 0.16f);
            duration += Time.deltaTime;
            yield return null;
        }
        p1.anchoredPosition = finalPosition;
        p2.anchoredPosition = finalPosition;
        ChangeScene.instance.ChangeToScene(2);
    }
}
