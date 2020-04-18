using System.Collections.Generic;

[System.Serializable]
public class Sequence {
    public string id;

    public List<Dialog> dialogs;

    public string[] GetAvailableDialogIds(){
        int availableIdsNumber = dialogs.Count;
        string[] ids = new string[availableIdsNumber];

        for (int i=0; i< availableIdsNumber; i++){
            ids[i] = dialogs[i].id;
        }

        return ids;
    }

    public Dialog GetFirstDialog(){
        return dialogs[0];
    }

    public Dialog GetDialogById(string id){
        foreach (Dialog dialog in dialogs) {
            if (string.Equals(dialog.id, id)){
                return dialog;
            }
        }
        throw new KeyNotFoundException(string.Format("Id {0} not found in sequence", id));
    }

    public List<Dialog> GetDialogFlow(){
        Dialog firstDialog = GetFirstDialog();
        return GetDialogFlow(firstDialog);
    }

    public List<Dialog> GetDialogFlow(string id){
        Dialog initialDialog = GetDialogById(id);
        return GetDialogFlow(initialDialog);
    }

    protected List<Dialog> GetDialogFlow(Dialog initialDialog){
        List<Dialog> outputDialogs = new List<Dialog>();
        Dialog currentDialog = initialDialog;

        while (!string.IsNullOrEmpty(currentDialog.nextId)){
            outputDialogs.Add(currentDialog);
            currentDialog = GetDialogById(currentDialog.nextId);
        } 

        outputDialogs.Add(currentDialog);

        return outputDialogs;
    }
}