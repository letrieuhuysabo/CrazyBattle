using UnityEngine;

public class TowerCharacter2 : TowerCharacter
{
    public override void UpdateDataToConfigs(string path)
    {
        CharacterChoisen.pathOfCharacter2 = path;
    }
}
