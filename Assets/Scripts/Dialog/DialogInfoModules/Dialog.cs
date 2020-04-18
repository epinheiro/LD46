[System.Serializable]
public class Dialog {
    public string id;

    public CharacterInfo character;

    public string text;

    //List<Options> options; // May be empty
    public string nextId; // May be empty

    public Dialog(string id, string characterInfo, string charMood, string characterLine){
        this.id = id;
        character = new CharacterInfo(characterInfo, charMood);
        text = characterLine;
    }

    public Dialog(string id, string characterInfo, string charMood, string characterLine, string nextDialogId){
        this.id = id;
        character = new CharacterInfo(characterInfo, charMood);
        text = characterLine;
        this.nextId = nextDialogId;
    }
}