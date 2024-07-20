using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMuntiplier;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    float textTureUnitSizeX;
    float textTureUnitSizeY;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // Corrected line
        if (spriteRenderer != null)
        {
            Sprite sprite = spriteRenderer.sprite;
            Texture2D texture = sprite.texture;
            textTureUnitSizeX = texture.width / sprite.pixelsPerUnit;
            textTureUnitSizeY = texture.height / sprite.pixelsPerUnit;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found on this GameObject.");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMuntiplier.x, deltaMovement.y * parallaxEffectMuntiplier.y);
        lastCameraPosition = cameraTransform.position;

        if (infiniteHorizontal)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textTureUnitSizeX)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textTureUnitSizeX;
                transform.position = new Vector3(cameraTransform.position.x, transform.position.y);
            }
        }

        if (infiniteVertical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textTureUnitSizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textTureUnitSizeY;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y, transform.position.z); // Corrected line
            }
        }
    }
}
