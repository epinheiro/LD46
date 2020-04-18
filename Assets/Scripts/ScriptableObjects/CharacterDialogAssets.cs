using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterDialogAssets", order = 1)]
public class CharacterDialogAssets : ScriptableObject
{
    public AudioClip defaultTextSound;

    public Sprite afraidSprite;
    public Sprite angrySprite;
    public Sprite happySprite;
    public Sprite sadSprite;
    public Sprite seriousSprite;
    public Sprite surprisedSprite;

    public Sprite GetSpriteByMood(string moodName)
    {
        switch (moodName)
        {
            case "happy":
                return happySprite;
            case "serious":
                return seriousSprite;
            case "sad":
                return sadSprite;
            case "angry":
                return angrySprite;
            case "afraid":
                return afraidSprite;
            case "surprised":
                return surprisedSprite;
        }
        return null;
    }
}