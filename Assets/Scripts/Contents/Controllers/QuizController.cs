using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuizController : MonoBehaviour
{
    private int front;
    private int rear;
    private int answer;
    private int emptySlot;
    private int numberDistance = 5;
    private float startPosY = 6f;
    private float spawnDelay = 3f;
    private float curSpawnDelay = 0;

    Vector2 spawnSize = new Vector2(6.7f, 2.2f);
    private List<int> questions = new List<int>();

    Define.QuizMode quizMode = Define.QuizMode.HavingAQuiz;

    public int quizLength { get; private set; }

    public void MakeQuiz()
    {
        front = Managers.Game.CurrentStageLevel + 1;
        rear = Random.Range(1, 10);
        answer = front * rear;

        string quiz = $"{front}*{rear}={answer}";
        quizLength = quiz.Length;
    }

    public void FillQuizSlot(List<Slot> quizSlots)
    {
        emptySlot = Random.Range(0, 2);

        for (int i = 0; i < quizSlots.Count; i++)
        {
            int index = i;

            if (i == 0)
            {
                MakeNumber(quizSlots, index, front);
            }
            else if (index == 2)
            {
                if (emptySlot == 0)
                {
                    quizSlots[index].SetQustionMarkColor(Color.white);
                    quizSlots[index].SetAnswerNumber(rear);
                    AddQuestionAnswer(rear);
                    continue;
                }

                MakeNumber(quizSlots, index, rear);
            }
            else if (index == 4)
            {
                int answerfirstDigit = int.Parse(answer.ToString()[0].ToString());
                if (answer.ToString().Length >= 2)
                {
                    int answerLastDigit = int.Parse(answer.ToString()[1].ToString());
                    if (emptySlot == 1)
                    {
                        quizSlots[index + 1].SetQustionMarkColor(Color.white);
                        quizSlots[index + 1].SetAnswerNumber(answerLastDigit);
                        AddQuestionAnswer(answerLastDigit);
                    }
                    else
                    {
                        MakeNumber(quizSlots, index + 1, answerLastDigit);
                    }
                }

                if (emptySlot == 1)
                {
                    quizSlots[index].SetQustionMarkColor(Color.white);
                    quizSlots[index].SetAnswerNumber(answerfirstDigit);
                    AddQuestionAnswer(answerfirstDigit);

                    continue;
                }

                MakeNumber(quizSlots, index, answerfirstDigit);
            }
        }
    }

    private void MakeNumber(List<Slot> quizSlots, int index, int value)
    {
        GameObject go = Managers.Resource.Instantiate("Number", quizSlots[index].transform);
        Sprite sprite = Managers.Resource.Load<Sprite>($"Prefabs/Images/Numbers_{value}");
        Number num = go.GetComponent<Number>();
        num.SetNumber(value);
        num.SetSpriteNumber(sprite);
        quizSlots[index].SetQustionMarkColor(Color.clear);
        quizSlots[index].SetIsEmpty();
    }

    public void UpDateSpawnNumber(Define.GameMode gameMode)
    {
        switch (gameMode)
        {
            case Define.GameMode.Nomal:
                switch (quizMode)
                {
                    case Define.QuizMode.HavingAQuiz:
                        SpawnNumber();
                        break;
                    case Define.QuizMode.Waiting:
                        Debug.Log("퀴즈 대기");
                        break;
                }
                break;
            case Define.GameMode.Fever:
                break;
            case Define.GameMode.Clear:
                break;
            case Define.GameMode.Over:
                break;
        }
    }

    private void SpawnNumber()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > spawnDelay)
        {
            float startPosX = Random.Range(-spawnSize.x, spawnSize.x);
            float destPosY = Random.Range(-spawnSize.y, spawnSize.y);
            Vector3 pos = new Vector3(startPosX, startPosY, 0);
            GameObject go = Managers.Resource.Instantiate("Number");
            Number num = go.GetComponent<Number>();
            go.transform.position = pos;
            SettingNumber(num, SelectQuestion());
            if (quizMode != Define.QuizMode.HavingAQuiz)
                return;
            FallingNumber(go, destPosY);
            curSpawnDelay = 0;
        }
    }

    private void FallingNumber(GameObject number, float destPosY)
    {
        number.transform.DOLocalMoveY(destPosY, 5f);
    }

    private void SettingNumber(Number num, int value)
    {
        num.SetNumber(value);
        Sprite spriteNum = Managers.Resource.Load<Sprite>($"Prefabs/Images/Numbers_{value}");
        num.SetSpriteNumber(spriteNum);
    }

    private int prevQuestion = 0;
    public void MakeQuestion()
    {
        if (answer.ToString().Length >= 2)
        {
            for (int i = 0; i < 3; i++)
            {
                int question = Random.Range(1, 10);
                while (question == prevQuestion)
                {
                    question = Random.Range(1, 10);
                }
                prevQuestion = question;
                questions.Add(question);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                int question = Random.Range(1, 10);
                while (question == prevQuestion)
                {
                    question = Random.Range(1, 10);
                }
                prevQuestion = question;
                questions.Add(question);
            }
        }
    }

    private void AddQuestionAnswer(int answer)
    {
        questions.Add(answer);
    }

    private int SelectQuestion()
    {
        int selectQuestion = Random.Range(0, questions.Count);

        if (questions.Count == 0)
        {
            Debug.Log("문제를 모두 출제 했습니다.");
            quizMode = Define.QuizMode.Waiting;
            return 0;
        }

        int selectNumber = questions[selectQuestion];
        questions.RemoveAt(selectQuestion);
        return selectNumber;
    }
}
