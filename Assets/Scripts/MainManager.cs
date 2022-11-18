using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance = null;

    public TableController tc_quizzes;

    public Button bt_create;

    List<QuizJson> quizJsons = new();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bt_create.onClick.AddListener(() => StartCoroutine(CreateQuizCrt()));
    }

    void OnEnable()
    {
        Constants.tx_Title.text = "Main Page";
        PopulateTable();
    }

    void Update()
    {

    }

    List<QuizJson> LoadQuizzes()
    {
        List<QuizJson> quizJsons = new();
        foreach (string filePath in Directory.GetFiles(Constants.QuizzesDataPath))
        {
            try
            {
                quizJsons.Add(JsonUtility.FromJson<QuizJson>(File.ReadAllText(filePath)));
                quizJsons.Last().filePath = filePath;
            }
            catch
            {
                continue;
            }
        }
        return quizJsons;
    }

    void PopulateTable()
    {
        foreach (QuizJson qj in quizJsons)
        {
            foreach (TableCell tc in qj.cells) Destroy(tc.gameObject);
            qj.cells.Clear();
        }
        quizJsons = LoadQuizzes();

        foreach (QuizJson qj in quizJsons)
        {
            qj.cells.Add(tc_quizzes.AddTableCell(qj.index.ToString()));
            qj.cells.Add(tc_quizzes.AddTableCell(qj.name));
            qj.cells.Add(tc_quizzes.AddTableCell_Button(Constants.spr_Edit, "Open", () =>
            {
                QuizManager qm = Instantiate(Constants.pf_QuizManager.gameObject, Constants.CanvasRoot).GetComponent<QuizManager>();
                qm.Initialize(qj, x =>
                {
                    qj.questions = x.questions;
                    File.WriteAllText(qj.filePath, JsonUtility.ToJson(qj));
                });
                gameObject.SetActive(false);
            }));
        }
    }

    IEnumerator CreateQuizCrt()
    {
        GameObject dialog = Instantiate(Constants.pf_CreateQuizDialog, Constants.CanvasRoot);
        int state = 0;
        dialog.GetAddQuizConfirmButton().onClick.AddListener(() => state = 1);
        dialog.GetAddQuizDiscardButton().onClick.AddListener(() => state = -1);
        yield return new WaitUntil(() => state != 0);
        Destroy(dialog);
        if (state == -1) yield break;

        string name = dialog.GetQuizNameInputField().text;
        List<QuizJson> quizJsons = LoadQuizzes();
        int index = quizJsons.Count == 0 ? 1 : quizJsons.Select((x) => x.index).Max() + 1;

        QuizJson quizJson = new QuizJson { index = index, name = name, filePath = Path.Combine(Constants.QuizzesDataPath, $"{index}_{name}.json") };
        File.WriteAllText(quizJson.filePath, JsonUtility.ToJson(quizJson));

        QuizManager qm = Instantiate(Constants.pf_QuizManager.gameObject, Constants.CanvasRoot).GetComponent<QuizManager>();
        qm.Initialize(quizJson, x =>
        {
            quizJson.questions = x.questions;
            File.WriteAllText(quizJson.filePath, JsonUtility.ToJson(quizJson));
        });
        gameObject.SetActive(false);
    }
}