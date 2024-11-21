using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ParallaxBackground_Type03 : MonoBehaviour
{

    [SerializeField]
    private Transform cameraTransform;

    private UnityEngine.Vector3 cameraStartPosition;
    private float distance;

    private Material[] materials;
    private float[] layerMoveSpeed;

    [SerializeField]
    [Range(0.01f, 1.0f)]
    private float parllaxSpeed;

    private void Awake()
    {
        int backgroundCount = transform.childCount;
        GameObject[] backgrounds = new GameObject[backgroundCount];

        materials = new Material[backgroundCount];
        layerMoveSpeed = new float[backgroundCount];

        for (int i = 0; i < backgroundCount; ++i)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        CalculateMoveSpeedByLayer(backgrounds, backgroundCount);
    }

    private void CalculateMoveSpeedByLayer(GameObject[] backgrounds, int count)
    {

        float farhestBackDistance = 0;

        for (int i = 0; i < count; ++i)
        {
            if ((backgrounds[i].transform.position.z - cameraTransform.position.z) > farhestBackDistance)
            {
                farhestBackDistance = backgrounds[i].transform.position.z - cameraTransform.position.z;
            }
        }

        for (int i = 0; i < count; ++i)
        {

            layerMoveSpeed[i] = 1 - (backgrounds[i].transform.position.z - cameraTransform.position.z) / farhestBackDistance + 0.05f;

            //Debug.Log($"{layerMoveSpeed[i]}, 실제 이동속도 = {layerMoveSpeed[i] * parllaxSpeed}");
        }
    }

    private void LateUpdate()
    {
        distance = cameraTransform.position.x - cameraStartPosition.x;

        transform.position = new UnityEngine.Vector3(cameraTransform.position.x, transform.position.y, 0);

        for (int i = 0; i < materials.Length; ++i)
        {
            float speed = layerMoveSpeed[i] * parllaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new UnityEngine.Vector2(distance, 0) * speed);
        }
    }

}
