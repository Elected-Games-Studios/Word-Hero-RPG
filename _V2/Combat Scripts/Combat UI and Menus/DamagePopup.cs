
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class DamagePopup : MonoBehaviour {

    // Create a Damage Popup
    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit) {
        var randomSpawnRadius = .2f;
        var itemOffset = position += Random.insideUnitSphere * randomSpawnRadius;
        itemOffset.y += 3f;
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, itemOffset, Quaternion.Euler(0.0f, 0.0f, Random.Range(-5f, 5f)));

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);
        if (isCriticalHit)
        {
            AuraBubbleInitializer.Create(damagePopupTransform.position);
        }

        return damagePopup;
    }

    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = .5f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    private bool offset;
    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit) {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit) {
            // Normal hit
            textMesh.fontSize = 6;
            textColor = UtilsClass.GetColorFromString("FFFFFF");
        } else {
            // Critical hit
            textMesh.fontSize = 7.3f;
            textColor = UtilsClass.GetColorFromString("FF3800");

        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(.3f, 1.4f);
    }

    private void Update() {

        //moveVector -= moveVector * 8f * Time.deltaTime;
        
        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f) {
            // First half of the popup lifetime
            transform.position += moveVector * Time.deltaTime;
            float increaseScaleAmount = .5f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else if (disappearTimer > DISAPPEAR_TIMER_MAX * .45)
        {
            //float increaseScaleAmount = .4f;
            //transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else {

            // Second half of the popup lifetime
            //float decreaseScaleAmount = .5f;
            //transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            // Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0) {
                Destroy(gameObject);
            }
        }
    }

}
