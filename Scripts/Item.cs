using System;
using System.Collections;
using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;

public class Item : MonoBehaviour
{
  public ItemObject item;

  public String GetItemType()
  {
    return item.type.ToString();
  }
}
