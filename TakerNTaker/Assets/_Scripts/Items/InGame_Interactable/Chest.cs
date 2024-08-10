using Goldmetal.UndeadSurvivor;
using UnityEngine;
namespace InGameInteractable
{
    public class Chest : MonoBehaviour, IInGameInteractable
    {
        public void Use(Player player)
        {
            GameManager.instance.ShowSkillSelector();
            Destroy(gameObject);
        }
    }
}