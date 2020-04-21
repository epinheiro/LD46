using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    enum DialogBoxStates {Disable, SendText, Texting, WaitingUser}
    DialogBoxStates currentState = DialogBoxStates.Disable;

    // Metadata and control
    private const string triggerId = "dialogTrigger";
    List<Dialog> dialogFlow;
    int totalOfDialogs;
    int currentDialogIndex = 0;
    private bool endDialogue = false;
    private bool disabled = false;


    // Unity Scene objects
    public DialogCanvasController dialogCanvasController;
    Sequence dialogSequence;
    GameManager gameManager;
    PlayerController playerController;
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

        playerController = GameObject.Find("GameManager").GetComponentInChildren<PlayerController>();
        //dialogCanvasController = GameObject.Find("GameManager").GetCo.GetComponentInChildren<DialogCanvasController>();
        //if (dialogCanvasController == null) throw new System.Exception("DialogCanvas game object not found in Unity scene");
        //dialogCanvasController.SetActive(false);
        //playerObject = GameObject.Find("Player");
        //if (playerObject == null) throw new System.Exception("Player game object not found in Unity scene");
    }

    // Update is called once per frame
    void Update(){
        if (endDialogue) return;
        if (currentState == DialogBoxStates.SendText){
            ShowDialogText();
            // dialogCanvasController.PlayRandomWhisper();
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
        if (disabled) return;
        if (other.tag == "Player" ) {
            FocusOnDialog();
            disabled = true;
        }
        
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
        if (sequenceId == "rebel")
        {
            int count = 0;
            count += gameManager.peasant1 ? 1 : 0;
            count += gameManager.peasant2 ? 1 : 0;
            count += gameManager.peasant3 ? 1 : 0;
            if (count < 2)
            {
                sequenceId = "rebellose";
                dialogSequence = gameManager.dialogModel.GetSequenceById(sequenceId);
                dialogFlow = dialogSequence.GetDialogFlow();
                totalOfDialogs = dialogFlow.Count;
            }
        }




        dialogCanvasController.SetActive(true);
        //playerObject.GetComponent<PlayerBehavior>().AddInteractionDisabler(triggerId);
        playerController.deactivateCharacter();
        currentState = DialogBoxStates.SendText;
        if (playableDirector != null)
        {
            playableDirector.Pause();
        }
    }

    void UnfocusOnDialog() {
        dialogCanvasController.SetActive(false);
        endDialogue = true;


        if (playableDirector != null)
        {
            playableDirector.Play();
        }
        if (sequenceId == "caught")
        {
            EndGameOutro();
        }
        if (sequenceId == "peasant1")
        {
            gameManager.peasant1 = true;
            playerController.activateCharacter();
        }
        if (sequenceId == "peasant2")
        {
            gameManager.peasant2 = true;
            playerController.activateCharacter();
        }
        if (sequenceId == "peasant3")
        {
            gameManager.peasant3 = true;
            playerController.activateCharacter();
        }
        if (sequenceId == "rebel" || sequenceId == "rebellose")
        {
            EndGameOutro(true);
        }
    }

    void EndGameOutro(bool showCredits = false){
        if(!showCredits){
            ReloadScene();
        }else{
            GameObject.Find("MainMenuCanvas").GetComponent<MainMenuController>().ActivateEndGameCredits(ReloadScene);
        }
    }

    static public void ReloadScene(){
        SceneManager.LoadScene("Level1Scene");
    }

}
