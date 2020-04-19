using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    GameObject mainMenuGrop;
    GameObject aboutGroup;
    
    void Start()
    {
        mainMenuGrop = transform.Find("MainMenuGroup").gameObject;
        aboutGroup = transform.Find("AboutGroup").gameObject;
        aboutGroup.SetActive(false);
    }

    public void OnClickBeginButton(){
        Debug.Log("NYI - Begin button pressed");
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
