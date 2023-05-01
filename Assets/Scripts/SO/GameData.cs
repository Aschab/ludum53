using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data", menuName = "GameData")]
public class GameData : ScriptableObject
{
    public VehicleController selectedVehicle;
    public bool muted = false;
    public bool dialog = true;
}
