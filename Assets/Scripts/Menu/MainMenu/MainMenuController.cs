using System.Threading.Tasks;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject multiplayerSetting;
    public void OpenMultiplayerSetting()
    {
        multiplayerSetting.SetActive(true);
    }
    public async void CloseMultiPlayerSetting()
    {
        multiplayerSetting.GetComponent<Animator>().SetTrigger("Close");
        await WaitTask.WaitForSeconds(0.5f);
        multiplayerSetting.SetActive(false);
    }
}
