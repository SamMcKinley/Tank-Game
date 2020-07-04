using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearing : MonoBehaviour
{
    public TankData data;
    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, GameManager.Instance.Player.position)
        < GameManager.Instance.Player.gameObject.GetComponent<TankData>().NoiseDistance + data.HearingDistance)
        {
            data.CanHear = true;
        }
        else
        {
            data.CanHear = false;
        }
    }
}
