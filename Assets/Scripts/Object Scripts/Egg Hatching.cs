using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EggHatching : MonoBehaviour
{
    [SerializeField] private List<Sprite> eggStages;
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private GameObject middleOfTheScreen;
    
    private PlayerMovement playerMovement;

    private GameObject currentEgg;
    
    private int currentEggStage;
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByName("Main Scene").buildIndex)
        {
            playerMovement.shouldMove = false;
            StartHatching();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextEgg();
        }
    }

    private void StartHatching()
    {
        currentEggStage = 0;
        currentEgg = Instantiate(eggPrefab, new Vector3(middleOfTheScreen.transform.position.x, middleOfTheScreen.transform.position.x, 0), Quaternion.identity);
    }

    private void NextEgg()
    {
        if (currentEggStage == eggStages.Count)
        {
            EndHatching();
            return;
        }
        
        AudioManager.instance.PlayOneShot(FMODEvents.instance.eggHatching, this.transform.position);
        currentEgg.GetComponent<SpriteRenderer>().sprite = eggStages[currentEggStage];
        currentEggStage += 1;
    }
    
    private void EndHatching()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.eggHatching, this.transform.position);
        playerMovement.shouldMove = true;
        Destroy(currentEgg);
    }
}
