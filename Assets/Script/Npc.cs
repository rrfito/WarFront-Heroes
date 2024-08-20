using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC behaviour")]
public class Npc : ScriptableObject
{
    public enum medicine
    {
        Perban, Pil, Isotonik, Analgesics, AntiBiotik, CairanElektrolit, Celox, Defibrilator, HidrogenPeroksida, SalineSolution, SterlieGauze, Tourniquet
    }
    public int id;
    public medicine item;
    [TextArea(15,20)]
    public string description;
}
