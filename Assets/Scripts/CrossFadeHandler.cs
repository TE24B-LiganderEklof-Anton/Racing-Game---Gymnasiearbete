using UnityEngine;

public class CrossFadeHandler : MonoBehaviour
{
    [SerializeField]
    float fadeTime = 1f;


    void Start()
    {
        LODGroup.crossFadeAnimationDuration = fadeTime;
    }
    private void OnValidate()
    {
        LODGroup.crossFadeAnimationDuration = fadeTime;
    }

}
