using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    public float runSpeed; 
    public float gotHayDestroyDelay; 
    private bool hitByHay; 
    private bool dropped; 
    public float dropDestroyDelay; 
    private Collider myCollider; 
    private Rigidbody myRigidbody; 
    private SheepSpawner sheepSpawner;
    public float heartOffset; 
    public GameObject heartPrefab; 


    public float maxSpeed;
    public float speedIncreaseRate;
    // other variables...

    private float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
         
        // Increase the elapsed time since the start of the game
        elapsedTime += Time.deltaTime;

        // Increase the runSpeed by speedIncreaseRate per second,
        // up to a maximum of maxSpeed
        runSpeed = Mathf.Min(maxSpeed, runSpeed + speedIncreaseRate * Time.deltaTime);

        // Move the sheep forward with the updated runSpeed
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
{
    sheepSpawner.RemoveSheepFromList(gameObject);
    hitByHay = true; 
    runSpeed = 0;
    Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
    TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
    tweenScale.targetScale = 0; 
    tweenScale.timeToReachTarget = gotHayDestroyDelay;
    SoundManager.Instance.PlaySheepHitClip();
    GameStateManager.Instance.SavedSheep();
    Destroy(gameObject, gotHayDestroyDelay);
}
private void OnTriggerEnter(Collider other) 
{
    if (other.CompareTag("Hay") && !hitByHay) 
    {
        Destroy(other.gameObject); 
        HitByHay(); 
    }else if (other.CompareTag("DropSheep") && !dropped)
{
    Drop();
}
}
private void Drop()
{
    GameStateManager.Instance.DroppedSheep();
    sheepSpawner.RemoveSheepFromList(gameObject);
    dropped = true;
    myRigidbody.isKinematic = false; 
    myCollider.isTrigger = false; 
    SoundManager.Instance.PlaySheepDroppedClip();
    Destroy(gameObject, dropDestroyDelay); 
}
public void SetSpawner(SheepSpawner spawner)
{
    sheepSpawner = spawner;
}



}

