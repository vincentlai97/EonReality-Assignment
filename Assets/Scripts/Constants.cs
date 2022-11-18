using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using InputField = TMPro.TMP_InputField;
using System;
using System.Linq;

public class Constants : MonoBehaviour
{
    public static Constants Instance = null;

    public RectTransform canvasRoot;
    public static RectTransform CanvasRoot => Instance.canvasRoot;

    public Text tx_title;
    public static Text tx_Title => Instance.tx_title;

    string quizzesDataPath;
    public static string QuizzesDataPath => Instance.quizzesDataPath;

    public GameObject pf_createQuizDialog;
    static public GameObject pf_CreateQuizDialog => Instance.pf_createQuizDialog;

    public QuizManager pf_quizManager;
    static public QuizManager pf_QuizManager => Instance.pf_quizManager;

    public EditQuestionDialogController pf_editQuestionDialog;
    static public EditQuestionDialogController pf_EditQuestionDialog => Instance.pf_editQuestionDialog;

    public PreviewQuestionDialogController pf_previewQuestionDialog;
    static public PreviewQuestionDialogController pf_PreviewQuestionDialog => Instance.pf_previewQuestionDialog;

    [Header("Sprites")]
    public Sprite spr_edit;
    public static Sprite spr_Edit => Instance.spr_edit;

    private void Awake()
    {
        Instance = this;

        quizzesDataPath = Path.Combine(Application.persistentDataPath, "Quizzes");
        if (!Directory.Exists(quizzesDataPath)) Directory.CreateDirectory(quizzesDataPath);
    }
}

public static class ConstantsExtensions
{
    static public InputField GetQuizNameInputField(this GameObject go) => go.transform.Find("Dialog Box/Prompt").GetComponent<InputField>();
    static public Button GetAddQuizConfirmButton(this GameObject go) => go.transform.Find("Dialog Box/GameObject/Confirm").GetComponent<Button>();
    static public Button GetAddQuizDiscardButton(this GameObject go) => go.transform.Find("Dialog Box/GameObject/Discard").GetComponent<Button>();
}

[Serializable]
public class QuizJson : ICloneable
{
    public int index;
    public string name;
    public List<QuestionJson> questions;

    [NonSerialized]
    public List<TableCell> cells;
    [NonSerialized]
    public string filePath;

    public QuizJson()
    {
        questions = new();

        cells = new();
    }

    public object Clone()
    {
        return new QuizJson
        {
            index = this.index,
            name = this.name,
            questions = new List<QuestionJson>(this.questions.Select(x => x.Clone() as QuestionJson))
        };
    }
}

[Serializable]
public class QuestionJson : ICloneable
{
    public string question;
    public string[] options;
    public int correctOption;

    [NonSerialized]
    public List<TableCell> cells;
    [NonSerialized]
    public int orderInList;

    public QuestionJson()
    {
        options = new string[3];

        cells = new();
    }

    public object Clone()
    {
        return new QuestionJson
        {
            question = this.question,
            options = this.options.Clone() as string[],
            correctOption = this.correctOption
        };
    }
}