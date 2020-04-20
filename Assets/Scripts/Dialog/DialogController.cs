using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogController : MonoBehaviour
{
    enum DialogBoxStates {Disable, SendText, Texting, WaitingUser}
    DialogBoxStates currentState = DialogBoxStates.Disable;

    // Metadata and control
    private const string triggerId = "dialogTrigger";
    List<Dialog> dialogFlow;
    int totalOfDialogs;
    int currentDialogIndex = 0;


    // Unity Scene objects
    public DialogCanvasController dialogCanvasController;
    Sequence dialogSequence;
    GameManager gameManager;
    //GameObject playerObject;
    public PlayableDirector playableDirector;


    // Object setup to dialog scene
    public string sequenceId;
    public bool isAutomatic = false;
    public bool selfDestroyAfterInteraction = false;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null) throw new System.Exception("GameManager game object not found in Unity scene");
        dialogSequence = gameManager.dialogModel.GetSequenceById(sequenceId);
        dialogFlow = dialogSequence.GetDialogFlow();
        totalOfDialogs = dialogFlow.Count;
       
        //dialogCanvasController = GameObject.Find("GameManager").GetCo.GetComponentInChildren<DialogCanvasController>();
        //if (dialogCanvasController == null) throw new System.Exception("DialogCanvas game object not found in Unity scene");
        //dialogCanvasController.SetActive(false);
        //playerObject = GameObject.Find("Player");
        //if (playerObject == null) throw new System.Exception("Player game object not found in Unity scene");
    }

    // Update is called once per frame
    void Update(){
        if (currentState == DialogBoxStates.SendText){
            ShowDialogText();
        } else {
            if (currentState == DialogBoxStates.WaitingUser && Input.GetButtonDown("Use")){ // TODO change key
                currentDialogIndex++;

                currentState = DialogBoxStates.SendText;

                if (currentDialogIndex == totalOfDialogs){
                    if (selfDestroyAfterInteraction){
                        EraseInfluencesAndDestroyObject();
                    }else{
                        currentDialogIndex = 0;
                        UnfocusOnDialog();
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other){
        //if (GameObject.ReferenceEquals(other.gameObject, playerObject) && isAutomatic ) {
            FocusOnDialog();
        //}
    }

    // Posible coroutines
    void ShowDialogText(){
        StartCoroutine(ApperingCharacters(currentDialogIndex));        
    }

    IEnumerator ApperingCharacters(int currentDialogIndex){
        currentState = DialogBoxStates.Texting;
        int textLenght = dialogFlow[currentDialogIndex].text.Length;

        for (int currentCharacter=0; currentCharacter <= textLenght; currentCharacter++){
            dialogCanvasController.setDialog(dialogFlow[currentDialogIndex], currentCharacter);
            yield return null;
        }

        currentState = DialogBoxStates.WaitingUser;
    }

    // Control functions
    void EraseInfluencesAndDestroyObject(){
        UnfocusOnDialog();
        Destroy(this.gameObject);
    }

    public void FocusOnDialog(){
        dialogCanvasController.SetActive(true);
        //playerObject.GetComponent<PlayerBehavior>().AddInteractionDisabler(triggerId);
        currentState = DialogBoxStates.SendText;
        if (playableDirector != null)
        {
            playableDirector.Pause();
        }
    }

    void UnfocusOnDialog() {
        dialogCanvasController.SetActive(false);
        if (playableDirector != null)
        {
            playableDirector.Play();
        }
        //playerObject.GetComponent<PlayerBehavior>().RemoveInteractionDisabler(triggerId);
    }

}
