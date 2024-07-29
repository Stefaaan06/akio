using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public Transform[] backgrounds; // Array of all the backgrounds to be parallaxed
    public float[] parallaxScales; // Proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f; // How smooth the parallax effect should be

    public Transform cam; // Reference to the main camera's transform
    private Vector3 previousCamPos; // The position of the camera in the previous frame
    private Vector3[] initialPositions; // Initial positions of the backgrounds

    void Start()
    {
        previousCamPos = cam.position;

        // Store initial positions of the backgrounds
        initialPositions = new Vector3[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            initialPositions[i] = backgrounds[i].position;
        }

        // Assign corresponding parallax scales if not set
        if (parallaxScales.Length != backgrounds.Length)
        {
            parallaxScales = new float[backgrounds.Length];
            for (int i = 0; i < backgrounds.Length; i++)
            {
                parallaxScales[i] = backgrounds[i].position.z * -1;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = initialPositions[i].x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }
}