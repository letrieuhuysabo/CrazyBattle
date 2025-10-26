using UnityEngine;

public class ChangeSkinController_Player1 : MultiplayerChangeSkinController
{
    protected override void Init()
    {
        pathOfSkin = CharacterChoisen.pathOfCharacter1;
    }
}
