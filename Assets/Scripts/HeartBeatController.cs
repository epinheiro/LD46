using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatController : MonoBehaviour
{

    public struct HeartBeat{
        public AudioClip beat;
        public float distanceThreshold;

        public HeartBeat(AudioClip beat, float distanceThreshold){
            this.beat = beat;
            this.distanceThreshold = distanceThreshold;
        }
    }
    
    GameObject[] enemies;
    HeartBeat[] beats;

    public AudioClip beat0;
    public AudioClip beat1;
    public AudioClip beat2;
    public AudioClip beat3;

    AudioSource source;

    int currentBeat = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        beats = new HeartBeat[]{
            new HeartBeat(beat0, 30),
            new HeartBeat(beat1, 20),
            new HeartBeat(beat2, 15),
            new HeartBeat(beat3, 10)
        };

        source = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeBeat(ProcessDistance(ClosestEnemy()));
        PlayBeat();
    }

    float ClosestEnemy(){
        float distance = Mathf.Infinity;
        
        foreach(GameObject enemy in enemies){
            float checkDistance = Vector3.Distance(this.transform.position, enemy.transform.position);
            if(distance > checkDistance){
                distance = checkDistance;
            }
        }

        return distance;
    }

    int ProcessDistance(float distance){
        int nextBeat = 0;

        for( int i=0; i<beats.Length; i++){
            if(beats[i].distanceThreshold > distance){
                nextBeat = i;
            }
        }

        return nextBeat;
    }

    void ChangeBeat(int nextBeat){
        if(currentBeat != nextBeat){

        }
    }

    void PlayBeat(){
        if(!source.isPlaying){
            HeartBeat beat = beats[currentBeat];
            source.clip = beat.beat;
            source.Play();
        }
    }
}
