using System.Collections.Generic;
using UnityEngine;

public class SwitchTowerChoising : MonoBehaviour
{
    static List<ChooseThisCharacter> chooseThisCharacters;
    [SerializeField] Transform choosingTowerEffect;
    public static SwitchTowerChoising instance;
    private void Awake()
    {
        instance = this;
    }
    Transform currentParticle;
    public static void AddToObserver(ChooseThisCharacter chooseThisCharacter)
    {
        // observer design pattern
        if (chooseThisCharacters == null)
        {
            chooseThisCharacters = new();
        }
        chooseThisCharacters.Add(chooseThisCharacter);
    }
    public void SwitchTower(RectTransform tower)
    {
        // cập nhật tower đang được chọn vào các icon chọn nhân vật
        if (chooseThisCharacters != null)
        {
            foreach (var x in chooseThisCharacters)
            {
                x.TowerNeedShow = tower;
            }
        }
        
        if (currentParticle != null)
        {
            PlayAndStopParticle.StopParticle(currentParticle);
        }
        currentParticle = tower.Find("ChoosingTowerEffect");
        PlayAndStopParticle.PlayParticle(currentParticle);
    }
}
