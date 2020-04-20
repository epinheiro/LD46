using System;

[System.Serializable]
public class CharacterInfo {
    public enum AvailableCharacters {Prisoner, Knight, Peasant, Rebel}

    public enum AvailableMoods {serious, happy, sad}

    public string name = "";
    public string mood = "";

    public CharacterInfo(string name, string mood) {
        if (CharacterInfo.IsCharacterNameValid(name)) {
            this.name = name;
        }else{
            throw new Exception(string.Format("Character name {0} is not defined", name));
        }

        if (CharacterInfo.IsCharacterMoodValid(mood)) {
            this.mood = mood;
        }else{
            throw new Exception(string.Format("Mood {0} is not defined", mood));
        }
    }

    static public bool IsCharacterNameValid(string characterName){
        return Enum.IsDefined(typeof(AvailableCharacters), characterName);
    }

    static public bool IsCharacterMoodValid(string characterMood){
        return Enum.IsDefined(typeof(AvailableMoods), characterMood);
    }
}