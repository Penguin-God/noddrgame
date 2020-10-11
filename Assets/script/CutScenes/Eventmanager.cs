using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventmanager : MonoBehaviour
{
    private List<Npcmanager> npcmanagers;
    //마을마다 수가 다른 Npc특성상 한번넣으면 값을 바꿀 수 없는 []보다는 값을 유동적으로 바꿀 수 있는 <>가 적절하다.

    public void NpcLode()
    {
        npcmanagers = ToList(); //npcmanager에 ToList의 반환값인 templist(모든npc)를 넣음
    }

    public List<Npcmanager> ToList()
    {
        List<Npcmanager> templist = new List<Npcmanager>();
        Npcmanager[] temp = FindObjectsOfType<Npcmanager>();
        //여기서는 함수안에서 아예 새로운 배열이 만들어지는 것이므로 []을 사용해도 됨.
        //FindObjectsOfType은 <>안에 스크립트가 있는 모든 오브젝트를 반환함 따라서 모든 npc를 가져옴
        //변수 저장소를 상속받는 playermanager, npcmanager등을 보유중인 오브젝트들도 가져옴
        
        for(int i =0; i < temp.Length; i++)
        {
            templist.Add(temp[i]);
            //templist안에 모든NPC를 넣음
        }
        return templist;
    }

    public void EventMove(string name, string dir)
    {
        for(int i = 0; i < npcmanagers.Count; i++)
        {
            if (name == npcmanagers[i].CharactreName)
            {
                npcmanagers[i].Move(dir);
            }
        }
    }

    public void SetAc(string name) //원하는 게임 오브젝트를 숨기는 함수
    {
        for (int i = 0; i < npcmanagers.Count; i++)
        {
            if(name == npcmanagers[i].CharactreName)
            {
                npcmanagers[i].gameObject.SetActive(false);
            }
        }
    }
}
