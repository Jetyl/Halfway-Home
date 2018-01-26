using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Stratus
{
  [CustomEditor(typeof(TriggerSystem))]
  public class TriggerSystemEditor : BaseEditor<TriggerSystem>
  {
    //------------------------------------------------------------------------/
    // Properties
    //------------------------------------------------------------------------/
    private GUIStyle headerStyle;
    private GUIStyle backgroundStyle;
    private GUIStyle columnStyle;
    private GUIStyle buttonStyle; 

    private BaseEditor selectedEditor;
    private string selectedName { get; set; }
    private Triggerable selectedTriggerable { get; set; }
    private Trigger selectedTrigger { get; set; }

    //------------------------------------------------------------------------/
    // Messages
    //------------------------------------------------------------------------/
    protected override void OnConfigureGUIStyles()
    {
      headerStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
      backgroundStyle = new GUIStyle(EditorStyles.helpBox);
      //backgroundStyle.normal.background = StratusEditorStyles.MakeTexture(backgroundStyle.normal.background, StratusEditorStyles.backgroundColor);
      columnStyle = new GUIStyle(EditorStyles.helpBox);
      buttonStyle = new GUIStyle(EditorStyles.toolbarButton);
      //Trace.Script("Styles configured");
    }

    public override void OnBaseEditorGUI()
    {
      // Draw triggers and triggerables side by side
      EditorGUILayout.BeginHorizontal(backgroundStyle);
      {
        DrawTriggers();
        DrawTriggerables();
      }
      EditorGUILayout.EndHorizontal();

      // Draw the selected component
      DrawSelected();
    }

    //------------------------------------------------------------------------/
    // Methods: Drawing
    //------------------------------------------------------------------------/
    private void DrawTopControls()
    {
      //if ()
    }

    private void DrawTriggers()
    {
      EditorGUILayout.BeginVertical();
      EditorGUILayout.LabelField("Triggers", headerStyle);

      foreach (var trigger in declaredTarget.triggers)
        DrawTrigger(trigger);
      
      EditorGUILayout.EndVertical();
    }

    private void DrawTriggerables()
    {
      EditorGUILayout.BeginVertical();
      EditorGUILayout.LabelField("Triggerables", headerStyle);

      foreach (var triggerable in declaredTarget.triggerables)              
        DrawTriggerable(triggerable);        
      
      EditorGUILayout.EndVertical();
    }    

    void DrawSelected()
    {
      if (selectedEditor == null)
        return;

      EditorGUILayout.Separator();
      EditorGUILayout.BeginVertical(columnStyle);
      {
        EditorGUILayout.LabelField(selectedName, EditorStyles.miniBoldLabel);

        // Update the editor for the selected object
        selectedEditor.serializedObject.Update();
        selectedEditor.OnInspectorGUI();
      }
      EditorGUILayout.EndVertical();
    }

    private void DrawTrigger(Trigger trigger)
    {
      string name = trigger.GetType().Name;
      if (GUILayout.Button(name, EditorStyles.miniButtonLeft))
      {
        System.Action onLeftClick = () =>
        {
          selectedName = name;
          SelectTrigger(trigger);
        };

        System.Action onRightClick = () =>
        {
          var menu = new GenericMenu();
          menu.AddItem(new GUIContent("Remove"), false, () => RemoveTrigger(trigger));
          menu.ShowAsContext();
        };

        StratusEditorUtlity.OnMouseClick(onLeftClick, onRightClick);

      }
    }

    private void DrawTriggerable(Triggerable triggerable)
    {
      string name = triggerable.GetType().Name;
      if (GUILayout.Button(name, EditorStyles.miniButtonRight))
      {
        System.Action onLeftClick = () =>
        {
          selectedName = name;
          SelectTriggerable(triggerable);
        };

        System.Action onRightClick = () =>
        {
          var menu = new GenericMenu();
          menu.AddItem(new GUIContent("Remove"), false, ()=> RemoveTriggerable(triggerable));
          menu.ShowAsContext();
        };

        StratusEditorUtlity.OnMouseClick(onLeftClick, onRightClick);

      }
    }

    //------------------------------------------------------------------------/
    // Methods: Selection
    //------------------------------------------------------------------------/
    private void SelectTrigger(Trigger trigger)
    {
      selectedTriggerable = null;
      selectedTrigger = trigger;
      
      // Instantiate the editor for it, disable drawwing base trigger properties
      selectedEditor = Editor.CreateEditor(trigger, typeof(TriggerEditor)) as BaseEditor;
      var baseTriggerProperties = selectedEditor.propertiesByType[typeof(Trigger)];
      foreach (var property in baseTriggerProperties)
      {
        selectedEditor.propertyConstraints.Add(property, NeverDraw);
      }
    }

    private void SelectTriggerable(Triggerable triggerable)
    {
      selectedTrigger = null;
      selectedTriggerable = triggerable;
      selectedEditor = Editor.CreateEditor(triggerable, typeof(TriggerableEditor)) as BaseEditor;
    }

    private void RemoveTrigger(Trigger trigger)
    {
      declaredTarget.triggers.Remove(trigger);
      Undo.DestroyObjectImmediate(trigger);
    }

    private void RemoveTriggerable(Triggerable triggerable)
    {
      declaredTarget.triggerables.Remove(triggerable);
      Undo.DestroyObjectImmediate(triggerable);
    }
    
    private bool NeverDraw() => false;


  }

}