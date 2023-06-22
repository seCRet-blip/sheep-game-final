﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayNachine : MonoBehaviour
{
    public float horizontalBoundary = 22;
    public float movementSpeed;

    public GameObject hayBalePrefab; 
    public Transform haySpawnpoint; 
    public float shootInterval; 
    private float shootTimer; 

    public Transform modelParent; 

    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        LoadModel();
    }
private void LoadModel()
{
    Destroy(modelParent.GetChild(0).gameObject); 

    switch (GameSettings.hayMachineColor) 
    {
        case HayMachineColor.Blue:
            Instantiate(blueModelPrefab, modelParent);
        break;

        case HayMachineColor.Yellow:
            Instantiate(yellowModelPrefab, modelParent);
        break;

        case HayMachineColor.Red:
            Instantiate(redModelPrefab, modelParent);
        break;
    }
}
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateShooting();
    }



    public void UpdateMovement()
    {
           float horizontalInput = Input.GetAxisRaw("Horizontal"); 

    if (horizontalInput < 0 && transform.position.x > -horizontalBoundary) 
    {
        transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
    }
    else if (horizontalInput > 0 && transform.position.x < horizontalBoundary) 
    {
        transform.Translate(transform.right * movementSpeed * Time.deltaTime);
    }
    
    }
   private void UpdateShooting()
{
    shootTimer -= Time.deltaTime; 

    if (shootTimer <= 0 && Input.GetKey(KeyCode.Space)) 
    {
        shootTimer = shootInterval; 
        ShootHay(); 
    }
}
    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();
    }
}
