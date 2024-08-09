using Goldmetal.UndeadSurvivor;
using UnityEngine;
namespace InGameInteractable
{
    public class Box : MonoBehaviour, IInGameInteractable
    {
        public void Use(Player player)
        {
            //Level Up처럼 Box가 고를 수 있는 Popup UI 나오고
            //Weapon or Skill을 선택하고, 첫번째 skill을 Player에게 해금해준다.
        }
    }
}