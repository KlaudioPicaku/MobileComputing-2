using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  [CreateAssetMenu(fileName = "Generic Sign", menuName = "Interactable System/Items/Generic Sign")]
public class GenericSign :  InteractableObject
{
        private void Awake()
        {
            type = InteractableType.Tent;
        }
    }
