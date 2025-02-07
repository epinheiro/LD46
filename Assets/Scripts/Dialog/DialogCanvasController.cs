﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DialogCanvasController : MonoBehaviour
{

    const string charactersAssetsPath = "Dialogs/CharactersAssets/";
    const string fileExtension = ".asset";

    Text text_name;
    Text text_line;
    Image image_char;
    Image mask_char;
    AudioSource source;
    public List<AudioClip> whispers;
    int currentWhisper = 0;

    // Start is called before the first frame update
    void Start()
    {
        Transform panel = this.transform.GetChild(0);
        mask_char = panel.transform.GetChild(1).GetComponent<Image>();
        image_char = panel.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        text_name = panel.transform.GetChild(2).GetComponent<Text>();
        text_line = panel.transform.GetChild(3).GetComponent<Text>();
        
        source = this.GetComponent<AudioSource>();

        this.SetActive(false);
    }

    protected UnityEngine.Object loadAssets<T>(string characterName){
        if (!CharacterInfo.IsCharacterNameValid(characterName)) throw new System.Exception(string.Format("Character {0} is not valid", characterName));
        
        Object go = Resources.Load<CharacterDialogAssets>(string.Format("{0}{1}", charactersAssetsPath, characterName));
        Debug.Log("Path " + string.Format("{0}{1}", charactersAssetsPath, characterName));

        if (go == null) throw new System.Exception(string.Format("{0} character without {1} file", characterName, typeof(T).Name));

        return go;
    }

    public void SetActive(bool isActive){
        this.gameObject.SetActive(isActive);
    }

    public void SetDebugDialogTest(){ // DEBUG function
        CharacterDialogAssets bidaAsset = (CharacterDialogAssets) loadAssets<CharacterDialogAssets>("Bida");
                
        text_name.text = "Condescendenting Bida";
        text_line.text = "I guess it worked, champ";
        image_char.sprite = bidaAsset.happySprite;
        //mask_char.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background"); // THIS will be removed eventually by something as bidaAsset.maskXXX
    }

    public void setDialog(Dialog dialog){
        CharacterInfo charInfo = dialog.character;

        CharacterDialogAssets charAsset = (CharacterDialogAssets) loadAssets<CharacterDialogAssets>(charInfo.name);

        text_name.text = charInfo.name;
        text_line.text = dialog.text;
        image_char.sprite = charAsset.GetSpriteByMood(charInfo.mood);
        //mask_char.sprite = TODO
    }

    public void setDialog(Dialog dialog, int lastCharacter){
        CharacterInfo charInfo = dialog.character;

        CharacterDialogAssets charAsset = (CharacterDialogAssets) loadAssets<CharacterDialogAssets>(charInfo.name);

        text_name.text = charInfo.name;
        text_line.text = dialog.text.Substring(0, lastCharacter);
        image_char.sprite = charAsset.GetSpriteByMood(charInfo.mood);
        //mask_char.sprite = TODO

        PlayRandomWhisper();
        // source.PlayOneShot(charAsset.defaultTextSound, 0.1F); // TODO verify echo effect in multiple character sounds in sequence
    }

    public void PlayRandomWhisper() {
        if(!source.isPlaying) {
            var random = new System.Random();
            int nextWhisper = random.Next(whispers.Count);
            if(currentWhisper != nextWhisper){
                currentWhisper = nextWhisper;
                source.clip = whispers[currentWhisper];
                source.Play();
            }
        }
    }
}
