using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DialogModel dialogModel;
    public bool peasant1 { get; set; }
    public bool peasant2 { get; set; }
    public bool peasant3 { get; set; }
    

    void Awake()
    {
        dialogModel = new DialogModel("DemoLevel");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
