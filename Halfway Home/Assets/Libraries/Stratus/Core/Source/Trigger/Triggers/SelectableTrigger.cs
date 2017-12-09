using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stratus
{
  public class SelectableTrigger : Trigger
  {    
    public Selectable selectable;
    public SelectableProxy.SelectionType type;
    public bool state;

    private SelectableProxy proxy;

    protected override void OnAwake()
    {
      proxy = SelectableProxy.Construct(selectable, type, OnSelection, persistent);
    }

    void OnSelection(bool state)
    {
      if (this.state != state)
        return;

      //Trace.Script(type.ToString() + " = " + state);
      Activate();
    }



  } 
}
