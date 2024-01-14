using UnityEngine;
using DG.Tweening;
using System;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform cameraa;
    [SerializeField] private Vector3 positionStrength;
    [SerializeField] private Vector3 rotationStrength;

    public static event Action Shake;

    public static void Invoke()
    {
        Shake?.Invoke();
    }

    private void OnEnable() => Shake += CameraShakes;
    private void OnDisable() => Shake -= CameraShakes;
    

    private void CameraShakes()
    {
        cameraa.DOComplete();
        cameraa.DOShakePosition(0.3f, positionStrength);
        cameraa.DOShakeRotation(0.3f, rotationStrength);
    }













    //public IEnumerator CameraShakes(float duration, float magnitude)
    //{
    //    Vector3 originalPos = transform.localPosition;

    //    float elapsed = 0.0f;

    //    while (elapsed<duration)
    //    {
    //        float x = Random.Range(-1f, 1f) * magnitude;
    //        float y = Random.Range(-1f, 1f) * magnitude;

    //        transform.localPosition = new Vector3(x, y, originalPos.z);

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    transform.localPosition = originalPos;
        
    //}

    //public void CameraShakeCall()
    //{
    //    StartCoroutine(CameraShakes(0.22f,0.4f));
    //}
    #region
    //public Transform cameraTransform;
    //public float shakeDuration = 0.5f;
    //public float shakeIntensity = 0.3f;

    //Vector3 originalPos;

    //void Start()
    //{
    //    if (cameraTransform == null)
    //    {
    //        cameraTransform = Camera.main.transform;
    //    }

    //    originalPos = cameraTransform.localPosition;
    //}

    //public void Shake()
    //{
    //    if (cameraTransform != null)
    //    {
    //        StartCoroutine(ShakeCoroutine());
    //    }
    //}

    //IEnumerator ShakeCoroutine()
    //{
    //    float elapsedTime = 0f;

    //    while (elapsedTime < shakeDuration)
    //    {
    //        Vector3 shakePos = originalPos + Random.insideUnitSphere * shakeIntensity;
    //        cameraTransform.localPosition = shakePos;

    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    cameraTransform.localPosition = originalPos;
    //}
    #endregion
}
