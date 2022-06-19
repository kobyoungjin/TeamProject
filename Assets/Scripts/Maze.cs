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
        mazeCubes = new GameObject[num * num];  // �Էµ� ������ ������ŭ �Ҵ�
        prefabs = Resources.Load<GameObject>("Prefabs/MazeCube");
        maze = GameObject.Find("Maze");

        for (int i = 0; i < num * num; i++)
        {
            GameObject cube = Instantiate(prefabs);
            cube.name = prefabs.name + (i + 1);  // �̸��� +1 ��ŭ ����
            cube.transform.parent = this.transform;  // Maze������Ʈ�� �ڽ��� ����
            mazeCubes[i] = cube;  // �迭�� �־� ����
        }

        MakeCell(mazeCubes);  // ť�� ���� ����
        MakeMaze(mazeCubes);  // �游���
    }

    void MakeCell(GameObject[] cubes)
    {
        for (int i = 0; i < num * num; i++)
        {   // ���̺� ������� ť��� ����
            cubes[i].transform.position = maze.transform.position + new Vector3(2.3f * (i % num), 0, 2.3f * (i / num));
        }
    }

    // ���� ����� �̱���
    void MakeMaze(GameObject[] cubes)
    {
        int squareNum = num * num;
        List<int> cubeList = new List<int>();
        int temp = Random.Range(0, num);  // ù���� ���� ���ϱ�
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
                //Debug.Log("�ߺ�");
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
            //Debug.Log(cubeList[j] + 1 + "��ȣ:" + j);
            // ���� ���ڸ� ������ ť����� isTrigger�� ������
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
