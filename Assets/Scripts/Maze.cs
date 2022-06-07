using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    GameObject prefabs;
    GameObject[] mazeCubes;
    GameObject maze;
    public int num;
    int tempNum;

    void Start()
    {
        mazeCubes = new GameObject[num * num];  // 입력된 숫자의 제곱만큼 할당
        prefabs = Resources.Load<GameObject>("Prefabs/MazeCube");
        maze = GameObject.Find("Maze");

        for (int i = 0; i < num * num; i++)
        {
            GameObject cube = Instantiate(prefabs);
            cube.name = prefabs.name + (i + 1);  // 이름은 +1 만큼 증가
            cube.transform.parent = this.transform;  // Maze오브젝트의 자식을 설정
            mazeCubes[i] = cube;  // 배열에 넣어 관리
        }

        MakeCell(mazeCubes);  // 큐브 동적 생성
        MakeMaze(mazeCubes);  // 길만들기
    }

    void MakeCell(GameObject[] cubes)
    {
        for (int i = 0; i < num * num; i++)
        {   // 테이블 모양으로 큐브들 생성
            cubes[i].transform.position = maze.transform.position + new Vector3(4.0f * (i % num), 0, 4.0f * (i / num));
        }
    }

    // 아직 제대로 미구현
    void MakeMaze(GameObject[] cubes)
    {
        int squareNum = num * num;
        List<int> cubeList = new List<int>();
        int temp = Random.Range(0, num);  // 첫번쨰 숫자 정하기
        int random = 0;
        int i = 0;

        cubeList.Add(temp);
        //cubes[temp].gameObject.GetComponent<BoxCollider>().isTrigger = true;
        //cubes[temp].gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        //Debug.Log(temp + 1);
        while (random < squareNum - num)
        {
            random = RandomNum(temp);

            if (cubeList.Contains(random))
            {
                //Debug.Log("중복");
            }
            else
            {
                cubeList.Add(random);
                temp = random;
            }
           
            i++;
        }

        for (int j = 0; j < cubeList.Count; j++)
        {
            //Debug.Log(cubeList[j] + 1 + "번호:" + j);
            // 뽑은 숫자를 제외한 큐브들은 isTrigger가 꺼지게
            cubes[cubeList[j]].gameObject.GetComponent<BoxCollider>().isTrigger = false;
            //cubes[cubeList[j]].gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    int RandomNum(int ran)
    {
        int randomNum = 0;
        int[] index = new int[5];
        int temp = Random.Range(0, 5);

        index[0] = ran - 1;
        index[1] = ran + 1;
        index[2] = ran + num - 1;
        index[3] = ran + num;
        index[4] = ran + num + 1;

        if (ran % num == 0)
        {
            if (temp == 0 || temp == 2)
            {
                temp = ReRamdom(0, 2);
            }
        }
        if (ran % num == 4)
        {
            if (temp == 1 || temp == 4)
            {
                temp = ReRamdom(1, 4);
            }
        }

        randomNum = index[temp];

        return randomNum;
    }

    int ReRamdom(int garbage1, int garbage2)
    {
        int temp = 0;
        bool isFinish = false;

        while (!isFinish)
        {
            temp = Random.Range(0, 5);

            if (temp == garbage1 || temp == garbage2)
            {
                continue;
            }
            isFinish = true;
        }

        return temp;
    }
}
