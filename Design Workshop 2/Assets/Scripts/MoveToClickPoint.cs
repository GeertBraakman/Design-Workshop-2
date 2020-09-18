using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
    
public class MoveToClickPoint : MonoBehaviour {
    private NavMeshAgent agent;
    private bool pickedUpLucifer;
    public float speedIncrease = 1;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
        
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
                
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                agent.destination = hit.point;
            }
        }
    }


void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Lucifer")) 
        {
            other.gameObject.SetActive(false);
            pickedUpLucifer = true;
        } 
        else if (other.gameObject.CompareTag("Soul"))
        {
            other.gameObject.SetActive(false);
            agent.speed = agent.speed + speedIncrease; 
        } 
        else if (other.gameObject.CompareTag("Portal") && pickedUpLucifer) 
        {
            Vector3 dest = new Vector3(0, 2, 0);

            transform.position = dest;
            agent.destination = dest;
        }
    }
}
