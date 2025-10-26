using UnityEngine;

public class ShowSkill : MonoBehaviour
{
    GameObject panel;
    void Start()
    {
        panel = transform.GetChild(0).gameObject;
    }
    public void OpenPanel()
    {
        panel.SetActive(true);
    }
    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
