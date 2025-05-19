using System.Collections;
using UnityEngine;

public class AbstractGrowable : MonoBehaviour
{
    [field: SerializeField] public float TimeToGrow { get; private set; } = 0.5f;
    private Vector3 _targetScale;

    public void Spawn(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        transform.up = direction;
        transform.Rotate(0, Random.Range(0, 360), 0);
        _targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        float time = 0;
        while (time < TimeToGrow)
        {
            transform.localScale += _targetScale * Time.deltaTime * TimeToGrow;
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = _targetScale;
    }
}
