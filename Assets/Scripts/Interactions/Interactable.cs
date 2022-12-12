using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteracted;
    [SerializeField] private UnityEvent Selected;
    [SerializeField] private UnityEvent Deselected;


    private void Start()
    {
        List<Interaction> interactions = GetComponentsInChildren<Interaction>(true).ToList();

        if (interactions.Count > 0)
        {
            interactions[0].gameObject.SetActive(true);
        }
    }

    public void Interact()
    {
        Interaction interaction = FindActiveInteraction();

        if (interaction != null)
        {
            interaction.Execute();
        }
        onInteracted.Invoke();
        Debug.Log("Interacted");
    }

    public void Select()
    {
        Selected.Invoke();
        Debug.Log("Select");
    }
    
    public void Deselect()
    {
        Deselected.Invoke();
        Debug.Log("Deselect");
    }

    private Interaction FindActiveInteraction()
    {
        return GetComponentInChildren<Interaction>(false);
    }
}
