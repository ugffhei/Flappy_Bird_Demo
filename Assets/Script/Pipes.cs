using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speedMag = 2;
    public float endTime = 5;

    private void Start()
    {
        StartCoroutine(Death());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speedMag * Vector3.left * Time.deltaTime;
    }


    //Waits for the timer to end and then Destorys the bullet 
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(endTime);
        Destroy(gameObject);
    }
}
