using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int scope;
    public int time;
    public int money;
    public Skill skill;
    public Employee[] team;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if(scope <= 0 || time <= 0 || money <= 0){
            Debug.Log("Game Over");
        }
    }
}
