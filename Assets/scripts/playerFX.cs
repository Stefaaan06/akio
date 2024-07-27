using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFX : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public SpriteRenderer spriteRenderer;
    private float currentDeformationFactor = 1f;
    public float deformationSpeed = 5f; // Speed of the deformation transition

    void Update()
    {
        DeformPlayer();
    }

    void DeformPlayer()
    {
        float speed = playerMovement.rb.velocity.magnitude;
        float targetDeformationFactor = Mathf.Clamp(speed / playerMovement.moveSpeed, 1f, 1.4f); // Stronger deformation

        // Smooth transition
        currentDeformationFactor = Mathf.Lerp(currentDeformationFactor, targetDeformationFactor, Time.deltaTime * deformationSpeed);

        // Check direction
        float direction = playerMovement.rb.velocity.x >= 0 ? 1 : -1;

        // Apply deformation based on direction
        spriteRenderer.transform.localScale = new Vector3(currentDeformationFactor * direction, 1 / currentDeformationFactor, 1);
    }
}