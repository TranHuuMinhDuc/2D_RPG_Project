using UnityEngine;


[CreateAssetMenu(fileName = "ActorSO", menuName = "ScriptableObjects/Dialog/Actor")]
public class ActorSO : ScriptableObject
{
    public string actorName;
    public Sprite portrait;
}
