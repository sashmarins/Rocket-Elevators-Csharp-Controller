using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {
       public int ID;
        public int amountOfElevators;
        public List<int>servedFloors; 
        public List<CallButton>callButtonList;
        public List<Elevator>elevatorsList;
       public bool isBasement;  
       public int amountOfFloors;
       public int callButtonID = 1;
       public int elevatorID = 1;
        public Column(int _ID, int _amountOfFloors, int _amountOfElevators, List<int> _servedFloors, bool _isBasement)
        {
            ID = _ID;
            amountOfElevators = _amountOfElevators;
            servedFloors = _servedFloors;
            callButtonList = new List<CallButton>();
            isBasement = _isBasement;
            amountOfFloors = _amountOfFloors;
            elevatorsList = new List<Elevator>();

            createCallButtons(_amountOfFloors, _isBasement);
            createElevators(_amountOfFloors, _amountOfElevators);
        }

        public void createCallButtons(int _amountOfFloors, bool _isBasement) {
            if (_isBasement == true) {
                int buttonFloor = -1;
                for (int i = 0; i < _amountOfFloors; ++i) {
                    CallButton callButton = new CallButton(callButtonID, buttonFloor, "up");
                    callButtonList.Add(callButton);
                    buttonFloor--;
                    callButtonID++;
                }
            }
            else {
                int buttonFloor = 1;
                for (int i = 0; i < _amountOfFloors; ++i) {
                    CallButton callButton = new CallButton(callButtonID, buttonFloor, "down");
                    callButtonList.Add(callButton);
                    buttonFloor++;
                    callButtonID++;
                }
            }
        }

        public void createElevators(int _amountOfFloors, int _amountOfElevators) {
            for (int i = 0; i < _amountOfElevators; ++i){
                Elevator elevator = new Elevator(elevatorID, _amountOfFloors);
                elevatorsList.Add(elevator);
                elevatorID++;
            }
        }

        public Elevator requestElevator(int floor, string direction) {
            Elevator elevator = findElevator(floor, direction);
            elevator.addNewRequest(floor);
            // elevator.move();

            elevator.addNewRequest(1);
            // elevator.move();
            return elevator;
        }

        public Elevator findElevator(int floor, string direction) {
             ElevatorInformation bestElevatorInformations = new ElevatorInformation(null, 6, 1000000);
            if (floor == 1) {
                foreach (Elevator elevator in elevatorsList) {
                if (elevator.currentFloor == 1 && elevator.status == "stopped") {
                    bestElevatorInformations = checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, floor);
                }
                else if (elevator.currentFloor == 1 && elevator.status == "idle") {
                    bestElevatorInformations = checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, floor);
                }
                else if (1 > elevator.currentFloor && elevator.direction == "up") {
                    bestElevatorInformations = checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, floor);
                }
                else if (1 < elevator.currentFloor && elevator.direction == "down") {
                    bestElevatorInformations = checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, floor);
                }
                else if (elevator.status == "idle") {
                    bestElevatorInformations = checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, floor);
                }
                else {
                    bestElevatorInformations = checkIfElevatorIsBetter(5, elevator, bestElevatorInformations, floor);
                }
            }}
            else {
                foreach (Elevator elevator in elevatorsList) {
                if (floor == elevator.currentFloor && elevator.status == "stopped" && direction == elevator.direction) {
                    bestElevatorInformations = checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, floor);
                }
                else if (floor > elevator.currentFloor && elevator.direction == "up" && direction == "up") {
                    bestElevatorInformations = checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, floor);
                }
                else if (floor > elevator.currentFloor && elevator.direction == "down" && direction == "down") {
                    bestElevatorInformations = checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, floor);
                }
                else if (elevator.status == "idle") {
                    bestElevatorInformations = checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, floor);
                }
                else {
                    bestElevatorInformations = checkIfElevatorIsBetter(5, elevator, bestElevatorInformations, floor);
                }
            }}
             return bestElevatorInformations.bestElevator;
        }

        public ElevatorInformation checkIfElevatorIsBetter(int scoreToCheck, Elevator newElevator, ElevatorInformation bestElevatorInformations, int floor) {
            if (scoreToCheck < bestElevatorInformations.bestScore) {
                bestElevatorInformations.bestScore = scoreToCheck;
                bestElevatorInformations.bestElevator = newElevator;
                bestElevatorInformations.referenceGap = Math.Abs(newElevator.currentFloor - floor);
            }
            else if (bestElevatorInformations.bestScore == scoreToCheck) {
                int gap = Math.Abs(newElevator.currentFloor - floor);
                if (bestElevatorInformations.referenceGap > gap) {
                    bestElevatorInformations.bestElevator = newElevator;
                    bestElevatorInformations.referenceGap = gap;
                }
            }
             return bestElevatorInformations;
        }


    }
}