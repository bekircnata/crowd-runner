using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [SerializeField] private Transform runnersParent;
    [SerializeField] private GameObject runnerPrefab;
    [SerializeField] private float radius;
    [SerializeField] private float angle;

    void Start()
    {
        
    }

    void Update()
    {
        PlaceRunners();
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;
            case BonusType.Product:
                int runnersToAdd = (runnersParent.childCount * bonusAmount) - runnersParent.childCount;
                AddRunners(runnersToAdd);
                break;
            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;
            case BonusType.Division:
                int runnersToRemove = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                RemoveRunners(runnersToRemove);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnersParent);
        }
    }

    private void RemoveRunners(int amount)
    {
        int runnersAmount = runnersParent.childCount;

        if(amount > runnersAmount)
        {
            amount = runnersAmount;
        }

        for (int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            Transform runnerToDestroy = runnersParent.GetChild(1);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }
}