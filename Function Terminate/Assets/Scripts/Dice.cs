//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Dice : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Dice : Random
    {
        private static Dice instance = null;
        private Dice() : base() { }
        public static Dice Get_Instance()
        {

            if (instance == null)
            {
                instance = new Dice();
            }
            return instance;
        }
    }


