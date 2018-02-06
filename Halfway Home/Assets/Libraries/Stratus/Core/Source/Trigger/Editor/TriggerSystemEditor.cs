using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Rotorz.ReorderableList;

namespace Stratus
{
  [CustomEditor(typeof(TriggerSystem))]
  public class TriggerSystemEditor : BaseEditor<TriggerSystem>
  {
    //------------------------------------------------------------------------/
    // Fields
    //------------------------------------------------------------------------/
    // Styles
    private GUIStyle selectedButtonStyle;
    private GUIStyle buttonStyle;
    private GUIStyle headerStyle;
    private const float connectedButtonWidth = 20f;
    // List controls
    //private TriggerablesList triggerablesList;
    //private IReorderableListAdaptor triggerablesAdaptor;
    //private TriggersList triggersList;
    //private IReorderableListAdaptor triggersAdaptor;
    private BaseEditor selectedEditor;

    //------------------------------------------------------------------------/
    // Properties
    //------------------------------------------------------------------------/
    private string selectedName { get; set; }
    private Triggerable selectedTriggerable { get; set; }
    private Trigger selectedTrigger { get; set; }
    private BaseTrigger selected { get; set; }
    /// <summary>
    /// Triggerables connected to the current trigger
    /// </summary>
    private Dictionary<Triggerable, bool> connectedTriggerables { get; set; } = new Dictionary<Triggerable, bool>();
    /// <summary>
    /// Triggers connected to the current triggerable
    /// </summary>
    private Dictionary<Trigger, bool> connectedTriggers { get; set; } = new Dictionary<Trigger, bool>();

    //------------------------------------------------------------------------/
    // Messages
    //------------------------------------------------------------------------/
    protected override void PostEnable()
    {
      //triggerablesList = new TriggerablesList();
      //triggerablesAdaptor = new SerializedPropertyAdaptor(propertyMap["triggerables"]);
      //triggersList = new TriggersList();
      //triggersAdaptor = new SerializedPropertyAdaptor(propertyMap["triggers"]);

      selected = null;
      selectedTrigger = null;
      selectedTriggerable = null;

      // For recompilation
      if (selectedTrigger || selectedTriggerable)
        UpdateConnections();
    }

    protected override void OnFirstUpdate()
    {
      headerStyle = StratusEditorStyles.header;
      buttonStyle = StratusEditorStyles.button;
      selectedButtonStyle = StratusEditorStyles.blueButton;
    }

    public override void OnBaseEditorGUI()
    {
      // Draw triggers and triggerables side by side
      EditorGUILayout.BeginHorizontal(backgroundStyle);
      {
        GUILayout.FlexibleSpace();
        DrawTriggers();
        GUILayout.FlexibleSpace();
        DrawTriggerables();
        GUILayout.FlexibleSpace();
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
      //triggersList.Draw(triggersAdaptor);

      EditorGUILayout.LabelField("Triggers", headerStyle);
      foreach (var trigger in declaredTarget.triggers)
        DrawTrigger(trigger);


      EditorGUILayout.EndVertical();
    }

    private void DrawTriggerables()
    {
      EditorGUILayout.BeginVertical();
      //triggerablesList.Draw(triggerablesAdaptor);

      EditorGUILayout.LabelField("Triggerables", headerStyle);
      foreach (var triggerable in declaredTarget.triggerables)
      {
        //EditorGUILayout.BeginHorizontal();
        //DrawConnection(triggerable);
        DrawTriggerable(triggerable);
        //EditorGUILayout.EndHorizontal();
      }

      EditorGUILayout.EndVertical();
    }

    void DrawSelected()
    {
      if (selectedEditor == null)
        return;

      EditorGUILayout.Separator();
      EditorGUILayout.BeginVertical(StratusEditorStyles.backgroundLight, GUILayout.ExpandWidth(false));
      {
        EditorGUILayout.LabelField(selectedName, EditorStyles.miniBoldLabel);
        //selectedEditor.serializedObject.Update();
        selectedEditor.OnInspectorGUI();
      }
      EditorGUILayout.EndVertical();
    }

    private void DrawConnection(Triggerable triggerable)
    {
      if (!selectedTrigger)
      {
        //GUILayout.Button(string.Empty, StratusEditorStyles., GUILayout.Width(connectedButtonWidth));
        return;
      }

      bool isConnected = connectedTriggerables[triggerable];
      if (isConnected)
      {
        if (GUILayout.Button(string.Empty, StratusEditorStyles.blueCircleButton, GUILayout.Width(connectedButtonWidth)))
          Disconnect(selectedTrigger, triggerable);
      }
      else
      {
        if (GUILayout.Button(string.Empty, StratusEditorStyles.greyCircleButton, GUILayout.Width(connectedButtonWidth)))
          Connect(selectedTrigger, triggerable);
      }
    }

    private void DrawTrigger(Trigger trigger)
    {
      GUIStyle style = buttonStyle;
      if (selected)
      {
        if (selected == trigger)
          style = selectedButtonStyle;
        else if (selectedTriggerable && connectedTriggers.ContainsKey(trigger) && connectedTriggers[trigger])
          style = StratusEditorStyles.greenButton;
      }

      Draw(trigger, style, SelectTrigger, RemoveTrigger, SetTriggerContextMenu);
    }

    private void DrawTriggerable(Triggerable triggerable)
    {
      GUIStyle style = buttonStyle;
      if (selected)
      {
        if (selected == triggerable)
          style = selectedButtonStyle;
        else if (selectedTrigger && connectedTriggerables.ContainsKey(triggerable) && connectedTriggerables[triggerable])
          style = StratusEditorStyles.greenButton;
      }

      Draw(triggerable, style, SelectTriggerable, RemoveTriggerable, SetTriggerableContextMenu);
    }

    private void Draw<T>(T baseTrigger, GUIStyle style, System.Action<T> selectFunction, System.Action<T> removeFunction, System.Action<T, GenericMenu> contextMenuSetter) where T : BaseTrigger
    {
      string name = baseTrigger.GetType().Name;
      if (GUILayout.Button($"{name}\n{baseTrigger.description}", style, GUILayout.MaxWidth(225f)))
      {
        System.Action onLeftClick = () =>
        {
          selectedName = name;
          selected = baseTrigger;
          selectFunction(baseTrigger);
          GUI.FocusControl(string.Empty);
          UpdateConnections();
        };

        System.Action onRightClick = () =>
        {
          var menu = new GenericMenu();
          contextMenuSetter(baseTrigger, menu);
          menu.AddSeparator("");
          menu.AddItem(new GUIContent("Remove"), false, () => 
          {
            removeFunction(baseTrigger);
            UpdateConnections();
          });
          menu.ShowAsContext();
        };

        StratusEditorUtlity.OnMouseClick(onLeftClick, onRightClick);

      }
    }

    private void SetTriggerContextMenu(Trigger trigger, GenericMenu menu)
    {
      menu.AddItem(new GUIContent("Disconnect all"), false, () =>
      {
        trigger.targets.Clear();
        UpdateConnections();
      });
    }

    private void SetTriggerableContextMenu(Triggerable triggerable, GenericMenu menu)
    {
      if (selectedTrigger)
      {
        if (connectedTriggerables[triggerable])
          menu.AddItem(new GUIContent("Disconnect"), false, () => Disconnect(selectedTrigger, triggerable));
        else
          menu.AddItem(new GUIContent("Connect"), false, () => Connect(selectedTrigger, triggerable));
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

    private void Deselect()
    {
      selected = null;
      selectedEditor = null;
    }

    private void RemoveTrigger(Trigger trigger)
    {
      if (selected == trigger)
      {
        Deselect();
        selectedTrigger = null;
      }
      declaredTarget.triggers.Remove(trigger);
      Undo.DestroyObjectImmediate(trigger);
    }

    private void RemoveTriggerable(Triggerable triggerable)
    {
      if (selected == triggerable)
      {
        Deselect();
        selectedTriggerable = null;
      }
      declaredTarget.triggerables.Remove(triggerable);
      Undo.DestroyObjectImmediate(triggerable);
    }

    //------------------------------------------------------------------------/
    // Methods: Connections
    //------------------------------------------------------------------------/
    private void Connect(Trigger trigger, Triggerable triggerable)
    {
      trigger.targets.Add(triggerable);
      connectedTriggerables[triggerable] = true;
    }

    private void Disconnect(Trigger trigger, Triggerable triggerable)
    {
      trigger.targets.Remove(triggerable);
      connectedTriggerables[triggerable] = false;
    }

    private bool IsConnected(Trigger trigger, Triggerable triggerable)
    {
      if (trigger.targets.Contains(triggerable))
        return true;
      return false;
    }

    private void UpdateConnections()
    {
      connectedTriggerables.Clear();
      if (selectedTrigger)
      {
        foreach (var triggerable in declaredTarget.triggerables)
          connectedTriggerables.Add(triggerable, IsConnected(selectedTrigger, triggerable));
      }

      connectedTriggers.Clear();
      if (selectedTriggerable)
      {
        foreach (var trigger in declaredTarget.triggers)
          connectedTriggers.Add(trigger, IsConnected(trigger, selectedTriggerable));
      }
    }

    private bool NeverDraw() => false;


  }

}