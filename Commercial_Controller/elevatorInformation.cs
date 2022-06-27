using System;
using System.Collections.Generic;

namespace Commercial_Controller{
    public class ElevatorInformation
    {
        public int referenceGap;
        public int bestScore;
        public Elevator bestElevator;
        public ElevatorInformation(Elevator _bestElevator, int _bestScore, int _referenceGap) 
        {
            referenceGap = _referenceGap;
            bestScore = _bestScore;
            bestElevator = _bestElevator;
        }
    }
}