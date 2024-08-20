using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public TextMeshProUGUI interactText; // Referensi ke TextMeshPro

    private IInteractable currentInteractable;

    void Start()
    {
        // Set the interact text to be initially inactive
        interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                currentInteractable = interactObj;
                interactText.gameObject.SetActive(true);
                interactText.text = "Press E to interact";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
            }
            else
            {
                ClearInteractable();
            }
        }
        else
        {
            ClearInteractable();
        }
    }

    private void ClearInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable = null;
            interactText.gameObject.SetActive(false);
        }
    }
}
