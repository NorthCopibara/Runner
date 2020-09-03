using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game.Save
{
    public struct SaveData
    {
        public int recordPoints;

        public SaveData(int recordPoints) 
        {
            this.recordPoints = recordPoints;
        }
    }
}
