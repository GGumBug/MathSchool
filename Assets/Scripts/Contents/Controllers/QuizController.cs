using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface QuizClear
{
    void QuizFadeOut(float delay);
    IEnumerator WaitFadeOut(float delay);
}

public class QuizController : MonoBehaviour, QuizClear
{
    private int         front;
    private int         rear;
    private int         answer;
    private int         emptySlot;
    private float       startPosY = 6f;
    private float       spawnDelay = 3f;
    private float       curSpawnDelay = 0;
    private float       fadeDelay = 2f;
    private bool        isChoose;
    private bool        isQuizEnd;

    Vector2                 spawnSize = new Vector2(6.7f, 2.2f);
    private List<int>       questions = new List<int>();
    private List<Number>    numbers = new List<Number>();
    private Number          curNumber;

    Define.QuizMode quizMode = Define.QuizMode.HavingAQuiz;

    public int quizLength { get; private set; }
    public int AcquiredGear { get; private set; }

    private void Update()
    {
        if (Managers.Game.IsUnitCollocating)
            return;
        
        RaycastHit[] hits;
        LayerMask mask = LayerMask.GetMask("Number");
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), mask);

        foreach (RaycastHit hit in hits)
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Number") && !isChoose)
            {
                curNumber = hit.collider.gameObject.GetComponent<Number>();
                if (!curNumber.FildNumber)
                    return;
                StartCoroutine(SelectNumber(hit, curNumber));
            }
        }
    }

    public void UpDateSpawnNumber(Define.GameMode gameMode)
    {
        switch (gameMode)
        {
            case Define.GameMode.Nomal:
                switch (quizMode)
                {
                    case Define.QuizMode.HavingAQuiz:
                        SpawnFildNumber();
                        break;
                    case Define.QuizMode.Matched:
                        quizMode = Define.QuizMode.Waiting;
                        PlayerStat player = Managers.Game.GetPlayer().playerStat;
                        player.PlusGear(1);
                        AcquiredGear++;
                        PlusMathEnergy();
                        QuizFadeOut(fadeDelay);
                        StartCoroutine(WaitFadeOut(fadeDelay));
                        break;
                }
                break;
            case Define.GameMode.Fever:
                switch (quizMode)
                {
                    case Define.QuizMode.HavingAQuiz:
                        SpawnFildNumber();
                        break;
                    case Define.QuizMode.Matched:
                        quizMode = Define.QuizMode.Waiting;
                        PlusMathEnergy();
                        QuizFadeOut(fadeDelay);
                        StartCoroutine(WaitFadeOut(fadeDelay));
                        break;
                }
                break;
            case Define.GameMode.Clear:
                if (isQuizEnd)
                    return;
                isQuizEnd = true;
                quizMode = Define.QuizMode.QuizEnd;
                QuizFadeOut(fadeDelay);
                break;
            case Define.GameMode.Over:
                if (isQuizEnd)
                    return;
                isQuizEnd = true;
                quizMode = Define.QuizMode.QuizEnd;
                QuizFadeOut(fadeDelay);
                break;
        }
    }

    public void MakeQuiz()
    {
        front = Managers.Game.CurrentStageLevel + 1;
        rear = Random.Range(1, 10);
        answer = front * rear;

        string quiz = $"{front}*{rear}={answer}";
        quizLength = quiz.Length;
    }

    public void FillQuizSlot(List<GameObject> quizSlots)
    {
        emptySlot = Random.Range(0, 2);

        for (int i = 0; i < quizSlots.Count; i++)
        {
            int index = i;

            if (i == 0)
            {
                SpawnQuestionNumber(quizSlots, index, front);
            }
            else if (index == 2)
            {
                if (emptySlot == 0)
                {
                    quizSlots[index].GetComponent<Slot>().SetQustionMarkColor(Color.white);
                    quizSlots[index].GetComponent<Slot>().SetAnswerNumber(rear);
                    AddQuestionAnswer(rear);
                    continue;
                }

                SpawnQuestionNumber(quizSlots, index, rear);
            }
            else if (index == 4)
            {
                int answerfirstDigit = int.Parse(answer.ToString()[0].ToString());
                if (answer.ToString().Length >= 2)
                {
                    int answerLastDigit = int.Parse(answer.ToString()[1].ToString());
                    if (emptySlot == 1)
                    {
                        quizSlots[index + 1].GetComponent<Slot>().SetQustionMarkColor(Color.white);
                        quizSlots[index + 1].GetComponent<Slot>().SetAnswerNumber(answerLastDigit);
                        AddQuestionAnswer(answerLastDigit);
                    }
                    else
                    {
                        SpawnQuestionNumber(quizSlots, index + 1, answerLastDigit);
                    }
                }

                if (emptySlot == 1)
                {
                    quizSlots[index].GetComponent<Slot>().SetQustionMarkColor(Color.white);
                    quizSlots[index].GetComponent<Slot>().SetAnswerNumber(answerfirstDigit);
                    AddQuestionAnswer(answerfirstDigit);

                    continue;
                }

                SpawnQuestionNumber(quizSlots, index, answerfirstDigit);
            }
        }
    }

    private void SpawnQuestionNumber(List<GameObject> quizSlots, int index, int value)
    {
        GameObject go = Managers.Resource.Instantiate("Number", quizSlots[index].transform);
        go.transform.localPosition = Vector3.zero;
        Sprite sprite = Managers.Resource.Load<Sprite>($"Prefabs/Images/Numbers_{value}");
        Number num = go.GetComponent<Number>();
        numbers.Add(num);
        num.SetNumber(value);
        num.SetSpriteNumber(sprite);
        quizSlots[index].GetComponent<Slot>().SetQustionMarkColor(Color.clear);
        quizSlots[index].GetComponent<Slot>().SetIsEmpty();
    }

    private void SpawnFildNumber()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > spawnDelay)
        {
            float startPosX = Random.Range(-spawnSize.x, spawnSize.x);
            float destPosY = Random.Range(-spawnSize.y, spawnSize.y);
            Vector3 pos = new Vector3(startPosX, startPosY, 0);
            GameObject go = Managers.Resource.Instantiate("Number");
            Number num = go.GetComponent<Number>();
            numbers.Add(num);
            go.transform.position = pos;
            SettingNumber(num, SelectQuestion());
            num.SwitchFildNumber();
            if (quizMode != Define.QuizMode.HavingAQuiz)
                return;
            FallingNumber(num, destPosY);
            curSpawnDelay = 0;
        }
    }

    private void FallingNumber(Number number, float destPosY)
    {
        var numberTween = number.transform.DOLocalMoveY(destPosY, 5f);
        number.SetTweener(numberTween);
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

    private bool CheckSlots()
    {
        Game gameScene = Managers.Scene.CurrentScene as Game;
        List<GameObject> _quizSlots = gameScene.slotSpawner.QuizSlots;
        if (_quizSlots.Count == 0)
            return false;

        foreach (GameObject go in _quizSlots)
        {
            Slot slot = go.GetComponent<Slot>();
            if (slot == null)
                continue;

            if (!slot.IsEmpty)
                return false;
        }

        return true;
    }

    public void SwitchFeverTime()
    {
        spawnDelay *= 0.5f; 
    }

    private void PlusMathEnergy()
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().playerStat;
        // 콤보 시스템 추가 되면 콤보에따라 value 증가 되도록
        playerStat.PlusMathEnerge(5);
        UI_Game ui_Game = Managers.UI.uI_Scene as UI_Game;
        ui_Game.SetTextMathEnergy(Managers.Game.GetPlayer());
    }

    public void QuizFadeOut(float delay)
    {
        List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        Game gameScene = Managers.Scene.CurrentScene as Game;
        SlotSpawner slotSpawner = gameScene.slotSpawner;
        slotSpawner.QuizFadeOut(delay);
        

        foreach (Number item in numbers)
        {
            sprites.Add(item.gameObject.GetComponent<SpriteRenderer>());
            
        }

        foreach (SpriteRenderer item in sprites)
        {
            item.DOColor(Color.clear, delay);
        }
    }

    private void CreateNewQuiz()
    {
        Game gameScene = Managers.Scene.CurrentScene as Game;
        Debug.Log("새 문제 출제");
        MakeQuiz();
        MakeQuestion();
        gameScene.slotSpawner.SetSlotCount(quizLength);
        gameScene.slotSpawner.SpwanSlot();
        FillQuizSlot(gameScene.slotSpawner.QuizSlots);
        quizMode = Define.QuizMode.HavingAQuiz;
    }

    private IEnumerator SelectNumber(RaycastHit numhit, Number num)
    {
        isChoose = true;
        num.StopTweener();
        while (true)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            numhit.collider.transform.position = mousePos;

            RaycastHit[] hits;
            LayerMask mask = LayerMask.GetMask("Number");
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), mask);

            foreach (RaycastHit hit in hits)
            {
                if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Slot"))
                {
                    Slot slot = hit.collider.gameObject.GetComponent<Slot>();
                    if (curNumber != null && curNumber.number == slot.AnswerNumber)
                    {
                        slot.SetIsEmpty();
                        slot.SetQustionMarkColor(Color.clear);
                        curNumber.transform.position = slot.transform.position;
                        curNumber.SwitchFildNumber();                        
                        curNumber = null;
                        isChoose = false;
                        if (CheckSlots())
                        {
                            quizMode = Define.QuizMode.Matched;
                        }
                        yield break;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                isChoose = false;
                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator WaitFadeOut(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Number item in numbers)
        {
            item.ResetNumber();
            Managers.Resource.Destroy(item.gameObject);
        }
        numbers.Clear();
        questions.Clear();
        CreateNewQuiz();
    }
}
