using System.Threading.Tasks;
using UnityEngine;

public class ShowDefaultChooseCharacterMultiplayer : MonoBehaviour
{
    [SerializeField] RectTransform tower1, tower2;
    async void Start()
    {
        RootContainer root = CharacterChoisen.GetRootFromPath(CharacterChoisen.pathOfCharacter1);
        ShowRootInTower.ShowRoot(root, tower1);
        root = CharacterChoisen.GetRootFromPath(CharacterChoisen.pathOfCharacter2);
        ShowRootInTower.ShowRoot(root, tower2);
        // ban đầu, chọn tháp 1
        await WaitTask.WaitForSeconds(0.1f);
        SwitchTowerChoising.instance.SwitchTower(tower1);
    }
}
