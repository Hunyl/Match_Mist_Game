using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button hidebutton;
    public Button returnbutton;

    public GameObject enemyPrefab;
    public GameObject enemyNavPrefab;
    public GameObject stagePrefab;
    public GameObject hidingSpotPrefab;
    public GameObject swordPrefab;
    public GameObject smallMatchPrefab;
    public GameObject bigMatchPrefab;
    public GameObject player;
    
    public Transform[] enemySpawnPoints;
    public Transform playerGoalPoint;
    public Transform playerSpawnPoint;
    public Transform[] hidingSpotPoints;
    public Transform[] smallMatchPoints;
    public Transform bigMatchPoint;
    public Transform swordPoint;


    private void Awake() {
        foreach (Transform enemySpawnPoint in enemySpawnPoints) {
            GameObject enemyNav = Instantiate(enemyNavPrefab,enemySpawnPoint.position,Quaternion.identity);
            enemyPrefab.GetComponent<ZombieBehaviour>().enemyNav = enemyNav;
            enemyPrefab.GetComponent<ZombieBehaviour>().player = player.transform;
            enemyPrefab.GetComponent<ZombieBehaviour>().spawnPoint = enemySpawnPoint;
            GameObject enemy = Instantiate(enemyPrefab,enemySpawnPoint.position,Quaternion.identity);                                              
        }

        foreach (Transform hidingSpotPoint in hidingSpotPoints) {
            hidingSpotPrefab.GetComponent<HidingSpot>().buttonHide = hidebutton;
            hidingSpotPrefab.GetComponent<HidingSpot>().buttonReturn = returnbutton;
            hidingSpotPrefab.GetComponent<HidingSpot>().camera = player.GetComponentInChildren<Camera>();
            hidingSpotPrefab.GetComponent<HidingSpot>().player = player;
            Instantiate(hidingSpotPrefab,hidingSpotPoint.position,Quaternion.identity);
        }

        foreach (Transform hidingSpotPoint in hidingSpotPoints) {
            
        }
    }
    private void Start() {
        
    }
}

