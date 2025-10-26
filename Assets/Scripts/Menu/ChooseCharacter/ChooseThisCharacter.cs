using UnityEngine;

public class ChooseThisCharacter : MonoBehaviour
{
    string path;

    public string Path { get => path; set => path = value; }
    public RectTransform TowerNeedShow { get => towerNeedShow; set => towerNeedShow = value; }

    RectTransform towerNeedShow;
    RootContainer root;
    void Start()
    {
        SwitchTowerChoising.AddToObserver(this);
    }
    public void Choose()
    {
        if (root == null)
        {
            root = CharacterChoisen.GetRootFromPath(path);
        }
        // Debug.Log(towerNeedShow);
        ShowRootInTower.ShowRoot(root, towerNeedShow);
        towerNeedShow.GetComponent<TowerCharacter>().UpdateDataToConfigs(path);
    }
}
