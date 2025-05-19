using UnityEngine;

public class FaithManager : MonoBehaviour
{
    [SerializeField] private float faith = 0f;
    [SerializeField] private float faithGainPerSecond = 1f;
    [SerializeField] private float maxFaith = 100f;

    void Update()
    {
        faith = Mathf.Min(maxFaith, faith + faithGainPerSecond * Time.deltaTime);
    }

    public bool ConsumeFaith(float cost)
    {
        if (faith >= cost)
        {
            Debug.Log($"Faith consumed: {cost}. Remaining faith: {Mathf.FloorToInt(faith - cost)}");
            faith -= cost;
            return true;
        }
        return false;
    }
}