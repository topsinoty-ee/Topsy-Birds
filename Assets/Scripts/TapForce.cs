using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class TapForce : MonoBehaviour
{
    [SerializeField]
    float tapforce = 10;

    [SerializeField]
    float tilt = 0;

    [SerializeField]
    Vector3 startPos;

    [SerializeField]
    Quaternion foward;

    [SerializeField]
    Quaternion down;

    [SerializeField]
    AudioSource tapSound;

    [SerializeField]
    AudioSource scoreSound;

    [SerializeField]
    AudioSource dieSound;

    [SerializeField]
    private GameManagerScript 


    Rigidbody rigid;
    Quaternion downRotation;
    Quaternion fowardRotation;

    
    TrailRenderer trail;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        downRotation = Quaternion.Euler(0, 0, 0);
        fowardRotation = Quaternion.Euler(0, 0, 0);
        foward = fowardRotation;
        down = downRotation;
        game = GameManagerScript.Instance;
        rigid.simulated = false;
        //trail = GetComponent<TrailRenderer>();
        //trail.sortingOrder=20;
    }

    private void OnEnable() {
        GameManagerScript.OnGameStarted += OnGameStarted;
        GameManagerScript.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    private void OnDisable() {
        GameManagerScript.OnGameStarted -= OnGameStarted;
        GameManagerScript.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameOverConfirmed(){
        transform.localPosition = startPos;
    }

    private void Update()
    {
        if(game.GameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            transform.rotation = fowardRotation;
            rigid.AddForce(Vector3.up*tapforce);
            tapSound.Play();
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tilt * Time.deltaTime);
    }

         private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="ScoreZone"){
            OnPlayerScored();
            scoreSound.Play();
        }
        if (other.gameObject.tag=="DeadZone"){
            rigid.simulated = false;
            OnPlayerDied();
            dieSound.Play();
        }
    }
}
