using UnityEngine;
using UnityEngine.AI;
//Unused class for NavMesh testing
public class MoveTo : MonoBehaviour
{

    public Transform goal;
    NavMeshAgent agent;

    void Start()
    {
         agent= GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }
    private void Update()
    {
        agent.destination = goal.position;
    }
}