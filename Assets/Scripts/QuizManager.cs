using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public TableController tc_questions;
    public Button bt_add;
    public Button bt_preview;
    public Button bt_save;
    public Button bt_return;

    public QuizJson quizJson;

    void Start()
    {
        bt_add.onClick.AddListener(() =>
        {
            OpenEditQuestionDialog(null, x =>
            {
                quizJson.questions.Add(x);
                AddQuestionJsonToTable(x);
            }, "Add Question");
        });
        bt_preview.onClick.AddListener(() =>
        {
            PreviewQuestionDialogController dialog = Instantiate(Constants.pf_PreviewQuestionDialog.gameObject, Constants.CanvasRoot).GetComponent<PreviewQuestionDialogController>();
            dialog.quizJson = quizJson;
        });
        bt_save.onClick.AddListener(() =>
        {
            Destroy(gameObject);
            MainManager.Instance.gameObject.SetActive(true);
        });
        bt_return.onClick.AddListener(() =>
        {
            Destroy(gameObject);
            MainManager.Instance.gameObject.SetActive(true);
        });

        Constants.tx_Title.text = $"Quiz Details - {quizJson.name}";
        foreach (QuestionJson qj in quizJson.questions)
        {
            AddQuestionJsonToTable(qj);
        }
    }

    void Update()
    {
        bt_preview.interactable = quizJson.questions.Count > 0;
    }

    public void Initialize(QuizJson qj, UnityAction<QuizJson> onSave)
    {
        quizJson = qj.Clone() as QuizJson;
        bt_save.onClick.AddListener(() => onSave(quizJson));
    }

    void AddQuestionJsonToTable(QuestionJson qj)
    {
        qj.orderInList = quizJson.questions.IndexOf(qj);
        qj.cells.Add(tc_questions.AddTableCell((qj.orderInList + 1).ToString()));
        qj.cells.Add(tc_questions.AddTableCell(qj.question));
        qj.cells.Add(tc_questions.AddTableCell_Button(Constants.spr_Edit, "Edit", () => OpenEditQuestionDialog(qj, x =>
        {
            qj.question = x.question;
            qj.options = x.options;
            qj.correctOption = x.correctOption;
            qj.cells[1].Text = qj.question;
        })));
    }

    void OpenEditQuestionDialog(QuestionJson qj, UnityEngine.Events.UnityAction<QuestionJson> onSave, string title = "Edit Question")
    {
        if (qj == null) qj = new();
        EditQuestionDialogController dialog = Instantiate(Constants.pf_EditQuestionDialog.gameObject, Constants.CanvasRoot).GetComponent<EditQuestionDialogController>();
        dialog.Initialize(title, qj, onSave);
    }
}
