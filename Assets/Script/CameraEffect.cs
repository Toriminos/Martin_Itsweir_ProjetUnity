using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class CameraEffect : MonoBehaviour
{
    public float cameraAberationDeactivation = 0;
    public float cameraAberationActivation = 1;
    public int fovBase;
    public int fovMax;
    public float rumbleMagnitude;
    public float rumbleTime;
    public VolumeProfile profile;
    private ChromaticAberration chroAber;
    private Camera cam;

    private void Start() {
        if(profile.TryGet(out chroAber))
            chroAber.intensity.value = cameraAberationDeactivation;
        cam = GetComponent<Camera>();
        cam.fieldOfView = fovBase;
    }

    public void ActivateCameraEffect(bool activate) {
        if(profile.TryGet(out chroAber))
            if(activate){
                chroAber.intensity.value = cameraAberationActivation;
                StartCoroutine(ShakePlusFov(rumbleTime, rumbleMagnitude));
            }
            else{
                chroAber.intensity.value = cameraAberationDeactivation;
                GetComponent<Camera>().fieldOfView = fovBase;
            }
    }

    //Sur le coup merci Brackeys
    IEnumerator ShakePlusFov(float duration, float magnitude){
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration){
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(originalPos.x+x, originalPos.y+y, originalPos.z);
            //cam.fieldOfView = Mathf.Lerp(fovBase, fovMax, elapsed/duration);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public void SetClampFov(float mult){
        cam.fieldOfView = Mathf.Lerp(fovBase, fovMax, mult);
    }
}
