using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    GameObject mainMenuGrop;
    GameObject aboutGroup;
    GameObject ambientSound;
    public PlayableDirector playableDirector;
    
    
    void Start()
    {
        mainMenuGrop = transform.Find("MainMenuGroup").gameObject;
        aboutGroup = transform.Find("AboutGroup").gameObject;
        ambientSound = GameObject.Find("AmbientSound").gameObject;
        aboutGroup.SetActive(false);
    }

    public void OnClickBeginButton(){
        playableDirector.Play();
        ambientSound.GetComponent<AudioSource>().Play();
        this.gameObject.SetActive(false);

    }

    public void OnClickOpenAboutButton(){
        mainMenuGrop.SetActive(false);
        aboutGroup.SetActive(true);
    }

    public void OnClickOpenMainMenuButton(){
        mainMenuGrop.SetActive(true);
        aboutGroup.SetActive(false);
    }

    public void OnClickExitButton(){
        Application.Quit();
    }

    void StartFadeOutMenu(){
        StartCoroutine(FadeOutMenu());
    }

    void FadeBeginning(){
        DeactiveButton(this.gameObject);
    }

    void DeactiveButton(GameObject current){
        Button button = current.GetComponent<Button>();
        if(button!=null) button.enabled = false;

        int childCount = current.transform.childCount;

         if(childCount != 0){
            for( int i=0; i<childCount; i++){
                DeactiveButton(current.transform.GetChild(i).gameObject);
            }
        }
    }

    void FadeEnding(){
        Destroy(this.gameObject);
    }

    IEnumerator FadeOutMenu(float time = .75f){
        FadeBeginning();

        float hardcodedTimeStep = 0.01f;

        float step = -1/(time/hardcodedTimeStep);

        float timeCount = 0;

        List<CanvasRenderer> canvasList = new List<CanvasRenderer>();
        ListCanvasRendererRecursive(this.gameObject, canvasList);

        while(timeCount < time){
            timeCount += hardcodedTimeStep;

            foreach(CanvasRenderer cr in canvasList){
                float alpha = cr.GetAlpha();
                cr.SetAlpha(alpha+step);
            }

            yield return new WaitForSeconds(hardcodedTimeStep);
        }

        FadeEnding();
    }

    void ListCanvasRendererRecursive(GameObject current, List<CanvasRenderer> list){
        CanvasRenderer renderer = current.GetComponent<CanvasRenderer>();
        if(renderer != null){
            list.Add(renderer);
        }

        int childCount = current.transform.childCount;

        if(childCount != 0){
            for( int i=0; i<childCount; i++){
                ListCanvasRendererRecursive(current.transform.GetChild(i).gameObject, list);
            }
        }
    }
}
