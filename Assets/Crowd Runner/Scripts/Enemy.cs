using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State {Idle, Runing};

    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State state;
    private Transform targetRunner;

    void Start()
    {
        
    }

    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch(state) 
        {
            case State.Idle:
                SearchForTarget();
                break;
            case State.Runing:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] detectColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectColliders.Length; i++)
        {
            if(detectColliders[i].TryGetComponent(out Runner runner))
            {
                if(runner.IsTarget())
                {
                    continue;
                }

                runner.SetTarget();
                targetRunner = runner.transform;

                StartRuningTowardsTarget();
            }
        }
    }

    private void StartRuningTowardsTarget()
    {
        state = State.Runing;
        GetComponent<Animator>().Play("Run");
    }

    private void RunTowardsTarget()
    {
        if(targetRunner == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);

        if(Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }
}
