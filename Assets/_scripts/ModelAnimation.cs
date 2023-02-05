using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAnimation : MonoBehaviour
{
  public bool animate;
  public float actTime;
  public float timer;
  public float rotSpeed;
  public float speedVariance;
  public float currentSpeed;
  public float direction = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(animate)
        {
          transform.Rotate(0,currentSpeed * direction * Time.deltaTime,0);

          timer -= Time.deltaTime;
          if(timer <= 0)
          {
            currentSpeed = rotSpeed + Random.Range(-speedVariance, speedVariance);
            timer = actTime;
            direction *= -1;
          }
        }else{transform.rotation = Quaternion.identity;}
    }
}
