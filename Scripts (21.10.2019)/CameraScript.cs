using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera cam;
    public Transform cameraShake;
    public Transform limiteY;
    public float speed = 0.15f;
    private Transform target;
    public Transform bandeira;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target) {
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, -10);
        }
        if (transform.position.y < limiteY.position.y) {
            transform.position = new Vector3(transform.position.x, limiteY.position.y, transform.position.z);
        }
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = cameraShake.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            cameraShake.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraShake.localPosition = originalPos;
    }

    public void endgame()
    {
        limiteY.position = new Vector2(bandeira.position.x, bandeira.position.y + 4f);
    }
}
