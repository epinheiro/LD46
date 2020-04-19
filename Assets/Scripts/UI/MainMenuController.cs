using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainMenuController : MonoBehaviour
{
    GameObject mainMenuGrop;
    GameObject aboutGroup;
    public PlayableDirector playableDirector;

    
    void Start()
    {
        mainMenuGrop = transform.Find("MainMenuGroup").gameObject;
        aboutGroup = transform.Find("AboutGroup").gameObject;
        aboutGroup.SetActive(false);
    }

    public void OnClickBeginButton(){
        Debug.LogError("NYI - Begin button pressed");
        playableDirector.Play();
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

}
