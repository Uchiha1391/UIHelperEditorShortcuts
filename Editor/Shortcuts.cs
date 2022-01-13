using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector.Editor;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public static class Shortcuts
{
    [MenuItem("My Ui Commands/SelectParent _`")]
    public static void SelectParent()
    {
        var local_activeGameObject = Selection.activeGameObject;
        if (local_activeGameObject == null)
        {
            Debug.LogError("No parent available means at the highiest hierircy");

            return;
        }

        Selection.activeObject = local_activeGameObject.transform.parent; // very dope 
    }

    [MenuItem("My Ui Commands/Create button _1")]
    public static void ButtonInstansiate()
    {
        var Parent = Selection.activeGameObject;
        if (Parent == null)
        {
            Debug.LogError("button should be inside a canvas");

            return;
        }

        Transform CheckForCanvasTransform = Parent.transform;
        bool DoesHaveCanvas = false;
        bool BreakLoop = false;
        while (!BreakLoop)
        {
            if (CheckForCanvasTransform == Parent.transform.root)
            {
                BreakLoop = true;
            }

            Canvas Canvas = CheckForCanvasTransform.gameObject.GetComponent<Canvas>();
            if (Canvas != null)
            {
                DoesHaveCanvas = true;
                break;
            }

            CheckForCanvasTransform = CheckForCanvasTransform.parent;
        }

        if (!DoesHaveCanvas)
        {
            Debug.LogError("button should be inside a canvas");
            return;
        }

        GameObject buttonObject = new GameObject();
        Undo.RegisterCreatedObjectUndo(buttonObject, " remove object");

        buttonObject.AddComponent<Button>();
        buttonObject.AddComponent<Image>();

        buttonObject.gameObject.name = "New button";

        GameObjectUtility.SetParentAndAlign(buttonObject, Parent);
        Selection.activeObject = buttonObject; // very dope 
    }

    [MenuItem("My Ui Commands/Create_Image _2")]
    public static void ImageCreate()
    {
        var Parent = Selection.activeGameObject;
        if (Parent == null)
        {
            Debug.LogError("button should be inside a canvas");

            return;
        }

        Transform CheckForCanvasTransform = Parent.transform;
        bool DoesHaveCanvas = false;
        bool BreakLoop = false;
        while (!BreakLoop)
        {
            if (CheckForCanvasTransform == Parent.transform.root)
            {
                BreakLoop = true;
            }

            Canvas Canvas = CheckForCanvasTransform.gameObject.GetComponent<Canvas>();
            if (Canvas != null)
            {
                DoesHaveCanvas = true;
                break;
            }

            CheckForCanvasTransform = CheckForCanvasTransform.parent;
        }

        if (!DoesHaveCanvas)
        {
            Debug.LogError("button should be inside a canvas");
            return;
        }

        GameObject imageObject = new GameObject();
        Undo.RegisterCreatedObjectUndo(imageObject, " remove object");

        imageObject.AddComponent<Image>();

        imageObject.gameObject.name = "New Image";

        GameObjectUtility.SetParentAndAlign(imageObject, Parent);
        Selection.activeObject = imageObject;
    }

    [MenuItem("My Ui Commands/Create text _3")]
    public static void TextInstansiate()
    {
        var Parent = Selection.activeGameObject;
        if (Parent == null)
        {
            Debug.LogError("text should be inside a canvas");

            return;
        }

        Transform CheckForCanvasTransform = Parent.transform;
        bool DoesHaveCanvas = false;
        bool BreakLoop = false;
        while (!BreakLoop)
        {
            if (CheckForCanvasTransform == Parent.transform.root)
            {
                BreakLoop = true;
            }

            Canvas Canvas = CheckForCanvasTransform.gameObject.GetComponent<Canvas>();
            if (Canvas != null)
            {
                DoesHaveCanvas = true;
                break;
            }

            CheckForCanvasTransform = CheckForCanvasTransform.parent;
        }

        if (!DoesHaveCanvas)
        {
            Debug.LogError("text should be inside a canvas");
            return;
        }

        GameObject TextObject = new GameObject();
        Undo.RegisterCreatedObjectUndo(TextObject, " remove object");

        var compo = TextObject.AddComponent<TextMeshProUGUI>();
        compo.text = "new text";
        TextObject.gameObject.name = "New Text";

        GameObjectUtility.SetParentAndAlign(TextObject, Parent);
        Selection.activeObject = TextObject; // very dope 
    }

    [MenuItem("My Ui Commands/Create Empty _4")]
    public static void EmptyInstansiate()
    {
        var parent = Selection.activeGameObject;

        GameObject EmptyObject = new GameObject();
        EmptyObject.AddComponent<RectTransform>();
        Undo.RegisterCreatedObjectUndo(EmptyObject, " remove object");

        if (parent != null)
        {
            GameObjectUtility.SetParentAndAlign(EmptyObject, parent); //dope
        }

        Selection.activeObject = EmptyObject; // very dope 
    }


    /// <summary>
    /// This function is like blender apply trasnform. it will make child 0 while change parent transform
    /// </summary>
    [MenuItem("My Ui Commands/PushChild_RectTransformToParent #&t")]
    public static void PushChildRectTransformToParentGui()
    {
        var SelectedChildObj = Selection.activeGameObject;
        Transform TransformParent = SelectedChildObj.GetComponent<RectTransform>().parent;
        if (SelectedChildObj == null || TransformParent == null)
        {
            Debug.LogError(" select a child object");
        }

        if (TransformParent != null)
        {
            Undo.RecordObject(TransformParent.gameObject, "UndoParentTransform");
            Vector3 SelectedObjLocalPosition =
                SelectedChildObj.GetComponent<RectTransform>().localPosition;
            var Invert_SelectedObjLocalPosition = new Vector3(-SelectedObjLocalPosition.x,
                -SelectedObjLocalPosition.y, -SelectedObjLocalPosition.z);
            TransformParent.localPosition -=
                Invert_SelectedObjLocalPosition; // minus is very important

            for (int Index = 0; Index < TransformParent.childCount; Index++)


            {
                Transform Child = TransformParent.GetChild(Index);
                if (Child.gameObject.Equals(SelectedChildObj))
                    continue; // don't do anything with same object .it will cause bug
                Undo.RecordObject(Child, "UndoChildTransform");

                Vector3 ChildLocalPosition = Child.GetComponent<RectTransform>().localPosition;
                var DifferenceTransform = ChildLocalPosition - SelectedObjLocalPosition;
                Child.GetComponent<RectTransform>().localPosition = DifferenceTransform;
            }

            SelectedChildObj.GetComponent<RectTransform>().localPosition +=
                Invert_SelectedObjLocalPosition; // plus is very important

            Debug.Log(SelectedChildObj.GetComponent<RectTransform>().localPosition);
        }
    }

    /// <summary>
    /// This function is like blender apply trasnform. it will make child 0 while change parent transform
    /// </summary>
    [MenuItem("My Ui Commands/PushChildTransformToParent #t")]
    public static void PushChildTransformToParent3d()
    {
        var SelectedChildObj = Selection.activeGameObject;
        Transform TransformParent = SelectedChildObj.transform.parent;
        if (SelectedChildObj == null || TransformParent == null)
        {
            Debug.LogError(" select a child object");
        }

        if (TransformParent != null)
        {
            Undo.RecordObject(TransformParent.gameObject, "UndoParentTransform");
            Vector3 SelectedObjLocalPosition = SelectedChildObj.transform.localPosition;
            var Invert_SelectedObjLocalPosition = new Vector3(-SelectedObjLocalPosition.x,
                -SelectedObjLocalPosition.y, -SelectedObjLocalPosition.z);
            TransformParent.localPosition -=
                Invert_SelectedObjLocalPosition; // minus is very important

            for (int Index = 0; Index < TransformParent.childCount; Index++)


            {
                Transform Child = TransformParent.GetChild(Index);
                if (Child.gameObject.Equals(SelectedChildObj))
                    continue; // don't do anything with same object .it will cause bug
                Undo.RecordObject(Child, "UndoChildTransform");

                Vector3 ChildLocalPosition = Child.transform.localPosition;
                var DifferenceTransform = ChildLocalPosition - SelectedObjLocalPosition;
                Child.transform.localPosition = DifferenceTransform;
            }

            SelectedChildObj.transform.localPosition +=
                Invert_SelectedObjLocalPosition; // plus is very important

            Debug.Log(SelectedChildObj.transform.localPosition);


        }
    }


  
}