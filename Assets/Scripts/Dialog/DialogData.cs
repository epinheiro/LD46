/*
    This class is responsible to read the file describing the dialogs.
    
    The file has a dictionaty of various SEQUENCES
    The sequence may have various DIALOGS
    A dialog consists of:
    - a CHARACTER INFO with NAME and MOOD
    - a TEXT sample
    - a set of text OPTIONS to show after the text (NOT YET IMPLEMENTED)
    - a <OPTIONAL> NEXTID string to point to next dialog chunk

    Therefore:
    SEQUENCES have a DIALOGS LIST that individually has CHARACTER INFO, TEXT, SET OF OPTIONS and NEXT ID

    A example may me examined below:
    {
        "sequences" : [
            {
                "id": "introBida",
                "dialogs": [
                    {
                        "id" : "bida-beginingToTalk" ,
                        "character" : {
                            "name": "bida",
                            "mood": "normal"
                        },
                        "text": "First test",
                        "nextId": "bida-MidTalk"
                    },
                    { 
                        "id" : "bida-MidTalk",
                        "character" : {
                            "name": "bida",
                            "feeling": "sad"
                        },
                        "text": "Blablabla",
                        "nextId": "bida-endSpeech"
                    },
                    { 
                        "id" : "bida-endSpeech",
                        "character" : {
                            "name": "bida",
                            "feeling": "happy"
                        },
                        "text": "That IS IT"
                    } 
                ]
            }
            
        ]
    }
*/

using System.Collections.Generic;
using System;

using UnityEngine;


[System.Serializable]
public class DialogData {
    public List<Sequence> sequences;

    public string[] GetAvailableCharacters(){
        return Enum.GetNames(typeof(CharacterInfo.AvailableCharacters));
    }
    public string[] GetAvailableMoods(){
        return Enum.GetNames(typeof(CharacterInfo.AvailableMoods));
    }

    public string[] GetAvailableSequenceIds(){
        int availableIdsNumber = sequences.Count;
        string[] ids = new string[availableIdsNumber];

        for (int i=0; i< availableIdsNumber; i++){
            ids[i] = sequences[i].id;
        }

        return ids;
    }

    public Sequence GetSequenceById(string id){
        foreach (Sequence sequence in sequences) {
            if (string.Equals(sequence.id, id)){
                return sequence;
            }
        }
        throw new KeyNotFoundException(string.Format("Sequence with Id \"{0}\" does not exist", id));
    }

}