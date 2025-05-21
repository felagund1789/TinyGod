using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class FaithManager : MonoBehaviour
    {
        [SerializeField] private float faith = 0f;
        [SerializeField] private float faithGainPerSecond = 1f;
        [SerializeField] private float maxFaith = 100f;
        [SerializeField] private Image faithBarImage;

        void Update()
        {
            faith = Mathf.Min(maxFaith, faith + faithGainPerSecond * Time.deltaTime);
            UpdateFaithBar();
        }

        public bool ConsumeFaith(float cost)
        {
            if (faith >= cost)
            {
                Debug.Log($"Faith consumed: {cost}. Remaining faith: {Mathf.FloorToInt(faith - cost)}");
                faith -= cost;
                UpdateFaithBar();
                return true;
            }
            return false;
        }
    
        private void UpdateFaithBar()
        {
            if (faithBarImage)
            {
                faithBarImage.transform.localScale = new Vector3(faith / maxFaith, 1f, 1f);
            }
        }
    }
}