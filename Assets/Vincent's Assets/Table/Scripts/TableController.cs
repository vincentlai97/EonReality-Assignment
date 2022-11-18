using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    //public HeaderCell pf_headerCell;
    //public Transform headerContent;
    //List<HeaderCell> headerCells = new List<HeaderCell>();

    public TableCell pf_tableCell;
    public TableCell_Button pf_tableCell_Button;
    public Transform content_table;
    List<TableCell> tableCells = new List<TableCell>();

    //public void AddHeaders(List<string> headers)
    //{
    //    for (int i = 0; i < headers.Count; i++)
    //    {
    //        if (headerCells.Count > i)
    //        {
    //            headerCells[i].Text = headers[i];
    //            headerCells[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            HeaderCell newCell = Instantiate(pf_headerCell.gameObject, headerContent).GetComponent<HeaderCell>();
    //            newCell.Text = headers[i];
    //            headerCells.Add(newCell);
    //        }
    //    }
    //    for (int i = headers.Count; i < headerCells.Count; i++)
    //    {
    //        headerCells[i].gameObject.SetActive(false);
    //    }
    //}

    public TableCell AddTableCell(string data)
    {
        TableCell tc = Instantiate(pf_tableCell.gameObject, content_table).GetComponent<TableCell>();
        tc.Text = data;
        tableCells.Add(tc);
        return tc;
    }

    public TableCell_Button AddTableCell_Button(Sprite sprite, string tooltipText, UnityEngine.Events.UnityAction call)
    {
        TableCell_Button tcb = Instantiate(pf_tableCell_Button.gameObject, content_table).GetComponent<TableCell_Button>();
        tcb.Sprite = sprite;
        tcb.TooltipText.text = tooltipText;
        tcb.AddListener(call);
        tableCells.Add(tcb);
        return tcb;
    }

    public void Truncate()
    {
        foreach (TableCell tc in tableCells)
        {
            Destroy(tc.gameObject);
        }
        tableCells.Clear();
    }
}
