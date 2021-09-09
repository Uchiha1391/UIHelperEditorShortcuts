using System.Collections.Generic;
using System.IO;
using MyExtensionMethods;
using PersonalUtilityScripts;
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

        Transform CheckForCanvasTransform=Parent.transform;
        bool DoesHaveCanvas=false;
        bool BreakLoop=false;
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

    [MenuItem("My Ui Commands/createUiScript #&u")]
    public static void CreateUItemplateScript()
    {
        var activeUIselection = Selection.activeGameObject;
        var Canvas = activeUIselection.GetComponent<Canvas>();
        if (activeUIselection == null || Canvas == null)
        {
            Debug.LogError("not select canvas");

            return;
        }

        List<string> HighetsHierircyUIcomponents = new List<string>();
        for (int i = 0; i < activeUIselection.transform.childCount; i++)
        {
            var TempChild = activeUIselection.transform.GetChild(i);

            if (TempChild.gameObject.GetComponents(typeof(Component)).Length != 1)
            {
                HighetsHierircyUIcomponents.Add(TempChild.name);
            }
        }

        #region Getting parent organizations objects name ui

        List<string> ParentOrganizationObjectsStrings = new List<string>();
        List<GameObject> ParentOrganizationObjects = new List<GameObject>();
        for (int i = 0; i < activeUIselection.transform.childCount; i++)
        {
            var TempChild = activeUIselection.transform.GetChild(i);

            if (TempChild.gameObject.GetComponents(typeof(Component)).Length == 1)
            {
                ParentOrganizationObjectsStrings.Add(TempChild.name);
                ParentOrganizationObjects.Add(TempChild.gameObject);
            }
        }

        #endregion

        #region button names list

        List<string> ButtonNamesList = new List<string>();
        for (int i = 0; i < activeUIselection.transform.childCount; i++)
        {
            var TempChild = activeUIselection.transform.GetChild(i);

            if (TempChild.gameObject.GetComponent<Button>() != null)
            {
                ButtonNamesList.Add(TempChild.name);
            }
        }

        #endregion

        #region creating list of childs of organizationUIparentObjects

        List<List<UI_Script_template_generator.UIorganisationObjectsChildTypesAndNameForTemplateClass>>
            OrganizationObjectsChildNamesAndTypeList;
        OrganizationObjectsChildNamesAndTypeList =
            new List<List<UI_Script_template_generator.UIorganisationObjectsChildTypesAndNameForTemplateClass>>();
        for (var I = 0; I < ParentOrganizationObjectsStrings.Count; I++)
        {
            var TempList =
                new List<UI_Script_template_generator.UIorganisationObjectsChildTypesAndNameForTemplateClass>();
            for (var j = 0; j < ParentOrganizationObjects[I].transform.childCount; j++)
            {
                var TempChild2 = ParentOrganizationObjects[I].transform.GetChild(j);

                string Childtype = null;

                if (TempChild2.GetComponent<Button>() != null)
                {
                    Childtype = typeof(Button).ToString();
                }
                else if (TempChild2.GetComponent<Image>() != null)
                {
                    Childtype = typeof(Image).ToString();
                }
                else if (TempChild2.GetComponent<TextMeshProUGUI>() != null)
                {
                    Childtype = typeof(TextMeshProUGUI).ToString();
                }
                else if (TempChild2.GetComponents(typeof(Component)).Length == 1)
                {
                    Childtype = TempChild2.gameObject.name;
                }

                TempList.Add(new UI_Script_template_generator.UIorganisationObjectsChildTypesAndNameForTemplateClass
                {
                    Childname = TempChild2.name, Childtype = Childtype
                });
            }

            OrganizationObjectsChildNamesAndTypeList.Add(TempList);
        }

        #endregion

        var generator = new UI_Script_template_generator();

        //Pop-up a file explorer and ask the client to click where to save the project.
        string outputPath = EditorUtility.SaveFilePanelInProject(title: "Save Location", defaultName: "Layers",
            extension: "cs", message: "Where do you want to save this script?");

        string className = Path.GetFileNameWithoutExtension(outputPath);

        generator.FillData(OrganizationObjectsChildNamesAndTypeList, HighetsHierircyUIcomponents,
            ParentOrganizationObjectsStrings, ButtonNamesList, className);

        string code = generator.TransformText();

        //Write the class to disk
        File.WriteAllText(outputPath, code);

        //Tell Unity to refresh. 
        AssetDatabase.Refresh();
    }

    [MenuItem("My Ui Commands/DeleteNotVisibleObjectsFromScene #&d")]
    public static void DeleteNotVisibleObjectsFromScene()
    {
        var MeshesList = GameObject.FindObjectsOfType<MeshRenderer>();
        var selectedCamera = Selection.activeGameObject;
        Camera SelectedCameraComponent = selectedCamera.GetComponent<Camera>();
        if (SelectedCameraComponent == null)
        {
            Debug.LogError(" select a camera first..");
            return;
        }

        foreach (var MeshRenderer in MeshesList)
        {
            if (!MeshRenderer.IsVisibleFrom(SelectedCameraComponent))
            {
                Undo.DestroyObjectImmediate(MeshRenderer);
                Object.DestroyImmediate(MeshRenderer);
            }
        }
    }

    /// <summary>
    /// This function is like blender apply trasnform. it will make child 0 while change parent transform
    /// </summary>
    [MenuItem("My Ui Commands/PushChild_RectTransformToParent #&t")]
    public static void PushChildRectTrasnformToParent()
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
            Vector3 SelectedObjLocalPosition = SelectedChildObj.GetComponent<RectTransform>().localPosition;
            var Invert_SelectedObjLocalPosition = new Vector3(
                -SelectedObjLocalPosition.x,
                -SelectedObjLocalPosition.y,
                -SelectedObjLocalPosition.z


            );
            TransformParent.localPosition -= Invert_SelectedObjLocalPosition; // minus is very important

            for (int Index = 0; Index < TransformParent.childCount; Index++)


            {
                Transform Child = TransformParent.GetChild(Index);
                if (Child.gameObject.Equals(SelectedChildObj)) continue; // don't do anything with same object .it will cause bug
                Undo.RecordObject(Child, "UndoChildTransform");

                Vector3 ChildLocalPosition = Child.GetComponent<RectTransform>().localPosition;
                var DifferenceTransform = ChildLocalPosition - SelectedObjLocalPosition;
                Child.GetComponent<RectTransform>().localPosition = DifferenceTransform;
            }

            SelectedChildObj.GetComponent<RectTransform>().localPosition += Invert_SelectedObjLocalPosition;// plus is very important

            Debug.Log(SelectedChildObj.GetComponent<RectTransform>().localPosition);
        }

  





    }

    /// <summary>
    /// This function is like blender apply trasnform. it will make child 0 while change parent transform
    /// </summary>
    [MenuItem("My Ui Commands/PushChildTransformToParent #t")]
    public static void PushChildTrasnformToParent()
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
            var Invert_SelectedObjLocalPosition = new Vector3(
                -SelectedObjLocalPosition.x,
                -SelectedObjLocalPosition.y,
                -SelectedObjLocalPosition.z


            );
            TransformParent.localPosition -= Invert_SelectedObjLocalPosition; // minus is very important

            for (int Index = 0; Index < TransformParent.childCount; Index++)


            {
                Transform Child = TransformParent.GetChild(Index);
                if (Child.gameObject.Equals(SelectedChildObj)) continue; // don't do anything with same object .it will cause bug
                Undo.RecordObject(Child, "UndoChildTransform");

                Vector3 ChildLocalPosition = Child.transform.localPosition;
                var DifferenceTransform = ChildLocalPosition - SelectedObjLocalPosition;
                Child.transform.localPosition = DifferenceTransform;
            }

            SelectedChildObj.transform.localPosition += Invert_SelectedObjLocalPosition;// plus is very important

            Debug.Log(SelectedChildObj.transform.localPosition);
        }


    }

}