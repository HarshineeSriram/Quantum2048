using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour
{
    [SerializeField] private Image image; // Ensure this is set in the Inspector or dynamically
    public float currentValue;

    // Declare the Icon Line, Icon Dialog, and RadialProgress variables at the class level
    private GameObject iconLine;
    private GameObject iconDialog;
    private GameObject radialProgress;

    private bool isProgressComplete = false; // Add this flag

    void Start()
    {
        // Find the Canvas first
        GameObject canvas = GameObject.Find("Canvas");

        // Check if Canvas is found
        if (canvas != null)
        {
            // Navigate through the hierarchy
            Transform competencyCounter = canvas.transform.Find("Competency Counter");
            if (competencyCounter != null)
            {
                Transform tunnellingProgress = competencyCounter.Find("TunnellingProgress");
                if (tunnellingProgress != null)
                {
                    Transform icon = tunnellingProgress.Find("Icon");
                    if (icon != null)
                    {
                        Transform iconLineTransform = icon.Find("Icon Line");
                        if (iconLineTransform != null)
                        {
                            iconLine = iconLineTransform.gameObject;
                            // Ensure the Icon Line starts inactive
                            iconLine.SetActive(false);
                        }
                        else
                        {
                            Debug.LogError("Icon Line GameObject not found under Icon.");
                        }

                        Transform iconDialogTransform = icon.Find("Icon Dialog");
                        if (iconDialogTransform != null)
                        {
                            iconDialog = iconDialogTransform.gameObject;
                            // Ensure the Icon Dialog starts inactive
                            iconDialog.SetActive(false);
                        }
                        else
                        {
                            Debug.LogError("Icon Dialog GameObject not found under Icon.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Icon GameObject not found under TunnellingProgress.");
                    }

                    // Find RadialProgress under TunnellingProgress
                    Transform radialProgressTransform = tunnellingProgress.Find("RadialProgress");
                    if (radialProgressTransform != null)
                    {
                        radialProgress = radialProgressTransform.gameObject;
                        image = radialProgress.GetComponent<Image>();

                        if (image == null)
                        {
                            Debug.LogError("Image component not found on RadialProgress GameObject.");
                        }
                    }
                    else
                    {
                        Debug.LogError("RadialProgress GameObject not found under TunnellingProgress.");
                    }
                }
                else
                {
                    Debug.LogError("TunnellingProgress GameObject not found under Competency Counter.");
                }
            }
            else
            {
                Debug.LogError("Competency Counter GameObject not found under Canvas.");
            }
        }
        else
        {
            Debug.LogError("Canvas GameObject not found.");
        }
    }

    void Update()
    {
        if (image == null)
        {
            Debug.LogError("Image component is null. Please assign it in the Inspector or ensure it's set correctly.");
            return;
        }

        image.fillAmount = currentValue / 100;

        if (image.fillAmount >= 1 && !isProgressComplete)
        {
            isProgressComplete = true;

            // Pause the game
            Time.timeScale = 0;

            // Convert R=208, G=71, B=195 to a Color object
            Color customColor = new Color(208 / 255f, 71 / 255f, 195 / 255f);
            image.color = customColor;

            // Start the coroutine to handle the delayed actions
            StartCoroutine(ProgressCompleteRoutine());
        }
    }

    private IEnumerator ProgressCompleteRoutine()
    {
        // Wait for 1 second in real time
        yield return new WaitForSecondsRealtime(1);

        // Open the Icon Line if it's not null
        if (iconLine != null)
        {
            iconLine.SetActive(true);
        }
        else
        {
            Debug.LogError("iconLine GameObject is null. Cannot activate it.");
        }

        // Open the Icon Dialog if it's not null
        if (iconDialog != null)
        {
            iconDialog.SetActive(true);
        }
        else
        {
            Debug.LogError("iconDialog GameObject is null. Cannot activate it.");
        }

        // Close radialProgress if it's not null
        if (radialProgress != null)
        {
            radialProgress.SetActive(false);
        }
        else
        {
            Debug.LogError("radialProgress GameObject is null. Cannot deactivate it.");
        }

        // Unpause the game
        Time.timeScale = 1;
    }
}