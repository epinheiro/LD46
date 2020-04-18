using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    public enum State {Patrol, Pursuit, CloseSearch};

    public float moveSpeed;
    public float angularSpeed;
    public Vector3 visibilityConeScale;

    void SetUpStruct(float moveSpeed, float angularSpeed, Vector3 visibilityConeScale){
        this.moveSpeed = moveSpeed;
        this.angularSpeed = angularSpeed;
        this.visibilityConeScale = visibilityConeScale;
    }

    public EnemyState (float moveSpeed, float angularSpeed, Vector3 visibilityConeScale){
        SetUpStruct(moveSpeed, angularSpeed, visibilityConeScale);
    }

    public EnemyState (State state){
        switch(state){
            case State.Patrol:
                SetUpStruct(20, 360, Vector3.one * 300);
                break;

            case State.Pursuit:
                SetUpStruct(25, 1440, new Vector3(200,200,600));
                break;

            case State.CloseSearch:
                SetUpStruct(10, 720, new Vector3(1000,500,450f));
                break;

            default:
                throw new System.Exception(string.Format("State {0} does not have EnemyState preset", state));
        }
    }

    static public Dictionary<State, EnemyState> GetStatesDictionary(){
        Dictionary<State, EnemyState> output = new Dictionary<State, EnemyState>();

        foreach (State state in (State[]) System.Enum.GetValues(typeof(State))){
            output.Add(state, new EnemyState(state));
        }

        return output;
    }

}
