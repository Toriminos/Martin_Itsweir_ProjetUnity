using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class CameraEffect : MonoBehaviour
{
    public float cameraAberationDeactivation = 0;
    public float cameraAberationActivation = 1;
    public float rumbleMagnitude;
    public float rumbleTime;
    public VolumeProfile profile;
    private ChromaticAberration chroAber;

    private void Start() {
        if(profile.TryGet(out chroAber))
            chroAber.intensity.value = cameraAberationDeactivation;
    }

    public void ActivateCameraEffect(bool activate) {
        if(profile.TryGet(out chroAber))
            if(activate){
                chroAber.intensity.value = cameraAberationActivation;
                StartCoroutine(Shake(rumbleTime, rumbleMagnitude));
            }
            else{
                chroAber.intensity.value = cameraAberationDeactivation;
            }
    }

    //Sur le coup merci Brackeys
    IEnumerator Shake(float duration, float magnitude){
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration){
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(originalPos.x+x, originalPos.y+y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
