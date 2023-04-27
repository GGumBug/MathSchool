using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    private int front;
    private int rear;
    private int answer;

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
        int emptySlot = Random.Range(0, 2);

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
}
