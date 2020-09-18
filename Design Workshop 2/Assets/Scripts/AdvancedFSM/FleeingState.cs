using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FleeingState : FSMState
{
    public FleeingState(Transform[] wp)
    {
        waypoints = wp;
        stateID = FSMStateID.Fleeing;

        curRotSpeed = 1.0f;
        curSpeed = 100.0f;
    }
    public override void Act(Transform player, Transform npc)
    {
        //If NPC is too close to Player
        //Or NPC has less than 100 health
        // Change to Fleeing state
        if (Vector3.Distance(npc.position, player.position) <= 300.0f)
        {
            Debug.Log("Switch to Fleeing State");
            npc.GetComponent<EnemieStateControl>().SetTransition(Transition.CloseToPlayer);
            npc.GetComponent<EnemieStateControl>().statePlaceholder.text = "Fleeing!";
        }
        if (npc.GetComponent<EnemieStateControl>().Health < 100)
        {
            npc.GetComponent<EnemieStateControl>().SetTransition(Transition.LowHealth);
            npc.GetComponent<EnemieStateControl>().statePlaceholder.text = "Fleeing!";
        }
    }

    public override void Reason(Transform player, Transform npc)
    {
        //Rotate to the target point
        destPos = player.position;

        Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        //Go away
        npc.Translate(Vector3.forward * Time.deltaTime * (curSpeed * -1));
    }
}
