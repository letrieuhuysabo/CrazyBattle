using System.IO;
using UnityEngine;

public class TowerCharacter1 : TowerCharacter
{
    public override void UpdateDataToConfigs(string path)
    {
        CharacterChoisen.pathOfCharacter1 = path;
    }
}
