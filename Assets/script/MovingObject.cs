using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//케이디 쯔꾸르 강좌 11강 진행중에 NPC이동 구현 자체는 10강에 있다는 사실을 알고 10강으로 튀어버리고 이코드는 의미없이 남음

public class MovingObject : MonoBehaviour
{
    //[]은 넣은값을 수정할 수 없지만 <>는 .Add(), .Remove(), .Clear() 등의 메소드로 안의 값을 조절할 수 있음 .Claer()는 <>를 삭제하는 메소드
    //<>는 크기를 셀 때 .Count로 셈
    private List<play> CharacterMove;

    public void MoveCharacter()
    {
        CharacterMove = ToList();
    }

    public List<play> ToList()
    {
        //retuen값이 있는 함수이기 때문에 배열이 삭제되서 배열을 써도 상관이 없음
        //FindObjectsOfType는 FindObjectsOfType와 다르게 <>안에 값이 달린 모든 객체(오브젝트)를 반환시켜줌
        List<play> TempList = new List<play>();
        play[] temp = FindObjectsOfType<play>();

        for (int i = 0; i < temp.Length; i++)
        {
            //TempList에 play가 있는 모든 오브젝트를 넣어줌 
            TempList.Add(temp[i]);
        }

        //함수가 List형식이므로 반환값을 List값으로 해주어야 함
        return TempList;
    }

    //public void Move(string _name, string _dir)
    //{
    //    for(int i = 0; i < CharacterMove.Count; i++)
    //    {
    //        if(_name == CharacterMove[i].CharacterName)
    //        {
    //            //CharacterMove[i].Move(_dir);
    //            Debug.Log("stxfa");
    //        }
    //    }
    //}
}


