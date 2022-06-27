using System;
using System.Collections.Generic;

namespace Commercial_Controller
{

    public class Battery
    {
       public int ID;
        public int amountOfColumns;
        public int amountOfFloors;
        public int amountOfBasements;
        public int amountOfElevatorPerColumn;
        public List<Column>columnsList;
        public List<FloorRequestButton>floorRequestButtonsList;
        public int columnID = 1;
        public int floorRequestButtonID = 1;
        public string status;
 
        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            ID = _ID;
            amountOfColumns = _amountOfColumns;
            amountOfFloors = _amountOfFloors;
            amountOfBasements = _amountOfBasements;
            amountOfElevatorPerColumn = _amountOfElevatorPerColumn;
            status = "online";
            columnsList = new List<Column>();
            floorRequestButtonsList = new List<FloorRequestButton>();

            createFloorRequestButtons(_amountOfFloors);
            createColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);

             if (_amountOfBasements > 0) {
                createBasementFloorRequestButtons(_amountOfFloors);
                createBasementColumn(_amountOfBasements, _amountOfFloors, _amountOfElevatorPerColumn);
                _amountOfColumns -= 1;
            };

        }

        public void createBasementColumn(int _amountOfBasements, int _amountOfFloors, int _amountOfElevatorPerColumn) {
            List<int> servedFloors = new List<int>();
            int floor = -1;
            for (int i = 0; i < _amountOfBasements; i++) {
                servedFloors.Add(floor);
                floor--;
            }
            Column column = new Column(columnID, _amountOfBasements, _amountOfElevatorPerColumn, servedFloors, true);
           columnsList.Add(column);
            columnID++;
        }

        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfElevatorPerColumn) {
            int amountOfFloorsPerColumn = (int)Math.Ceiling((double)(_amountOfFloors / _amountOfColumns));
            int floor = 1;
            for (int i = 0; i < _amountOfColumns; ++i) {
                List<int> servedFloors = new List<int>(); 
                for (int h = 0; h < amountOfFloorsPerColumn; ++h) {
                    if (floor <= _amountOfFloors) {
                        servedFloors.Add(floor);
                        floor++;
                    }
                }
            Column column = new Column(columnID, _amountOfFloors, _amountOfElevatorPerColumn, servedFloors, false);
            columnsList.Add(column);
            columnID++;
            }
        }

        public void createFloorRequestButtons(int _amountOfFloors) {
            int buttonFloor = 1;
            for (int i = 0; i < _amountOfFloors; ++i) {
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, buttonFloor, "up");
                floorRequestButtonsList.Add(floorRequestButton);
                buttonFloor++;
                floorRequestButtonID++;
            }
        }

        public void createBasementFloorRequestButtons(int _amountOfBasements) {
            int buttonFloor = -1;
            for (int i = 0; i < _amountOfBasements; ++i) {
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, buttonFloor, "down");
                floorRequestButtonsList.Add(floorRequestButton);
                buttonFloor--;
                floorRequestButtonID++;
            }
        }


        public Column findBestColumn(int floor)
        {
          foreach (Column column in columnsList) 
          {
            if (column.servedFloors.Contains(floor)) {
                return column;
            }
          }
            return null;
        }
        
        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int floor, string direction)
        {
            Column column = findBestColumn(floor);
            Elevator elevator = column.findElevator(1, direction);
            elevator.addNewRequest(1);
            // elevator.move();

           elevator.addNewRequest(floor);
            // elevator.move();

            return (column, elevator);
        }
    }
}

