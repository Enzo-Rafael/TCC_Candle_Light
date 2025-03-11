using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
	[Header("Transmitindo em")]  
    [SerializeField] private ItemEventChannelSO _itemToggleEvent = default;
    [SerializeField] private ItemEventChannelSO _itemTriggerEvent = default;
    [SerializeField] private ItemEventChannelSO _itemConsumeEvent = default;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
