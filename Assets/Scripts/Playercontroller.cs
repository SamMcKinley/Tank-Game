using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public TankData data;
    private TankMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Player = this.gameObject.transform;
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
