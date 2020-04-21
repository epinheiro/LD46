using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class DialogModel
{
    // Highlevel class based configuration
    public readonly string dialogFilesPath = "Assets/Data/Dialogs/";
    public enum SupportedLanguages {br, en};
    protected const string fileFormat = "json";

    private SupportedLanguages _language;
    public string Language
    {
        get { return _language.ToString(); }
        set {
            if (Enum.IsDefined(typeof(SupportedLanguages), value)){
                Enum.TryParse(value, false, out _language);
            } else {
                throw new System.Exception(string.Format("Language {0} is not supported", value));
            }
        } 
    }

    DialogData dialogData;

    public DialogModel(string fileName){
        dialogData = GetDialogData(fileName);
    }        

    public Sequence GetSequenceById(string id){
        return dialogData.GetSequenceById(id);
    }

    //////////// File functions ////////////
    /// <summary>
    /// Method that gets the Dialog data file in the Assets/Data/ folder
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    protected string GetDialogDataFile(string fileName) {
        try{
            return GetTextFile(string.Format("{0}{1}-{2}.{3}", dialogFilesPath, fileName, _language, fileFormat));
        }catch(Exception e){
            throw e;
        }
    }

    /// <summary>
    /// Get an Text File reference, based on the relative path / of the project
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    protected string GetTextFile(string path) {
        // https://docs.unity3d.com/ScriptReference/AssetDatabase.LoadAssetAtPath.html
        try {
            TextAsset textFile = Resources.Load<TextAsset>("Dialogs/DemoLevel-br");
            //TextAsset textFile = (TextAsset) AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
            return textFile.ToString();
        } catch (Exception e)
        {
            throw e;
        }
    }

    //////////// Read parsed file functions ////////////
    public DialogData GetDialogData(string fileName){
        return JsonUtility.FromJson<DialogData>(GetDialogDataFile(fileName));
    }

}
