using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{

    [System.Serializable]
    public struct PickupEventEntry
    {
        public FloatGameEvent gameEvent;
        public float value;
    }
    public PickupEventEntry[] onPickupEventEntries;
    public void Pickup()
    {
        foreach (var pickupEntry in onPickupEventEntries)
        {
            pickupEntry.gameEvent.Raise(pickupEntry.value);
        }
    }
}
