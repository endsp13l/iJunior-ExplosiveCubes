using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DivisionSolver : MonoBehaviour
{
   private int _divisionChanceInPercents = 50;
   private Random _random = new Random();
   
   private void OnMouseDown()
   {
      TryDivision();
   }

   private void TryDivision()
   {
      int divisionChance = _random.Next(0, 100);
      
      if (divisionChance > _divisionChanceInPercents)
        Destroy(gameObject);
      
      Debug.Log("Division" + divisionChance);
   }
}
