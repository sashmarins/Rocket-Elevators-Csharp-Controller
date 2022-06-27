using System.Threading;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Elevator
    {
        public int ID;
        public int amountOfFloors;
        public int currentFloor;
        public string direction;
        //list floorrequestlist
        public Door door;
        public List<int>floorRequestsList;
        public List<int>completedRequestsList;
        public string status;

        public Elevator(int _ID, int _amountOfFloors)
        {
            ID = _ID;
            currentFloor = 1;
            status = "idle";
            amountOfFloors = _amountOfFloors;
            direction = null;
            completedRequestsList = new List<int>();
            floorRequestsList = new List<int>();
            door = new Door(_ID);
        }
        public void move()
        {
            while (floorRequestsList.Count > 0) {
                int destination = floorRequestsList[0]; 
                status = "moving";
                if (currentFloor < destination) {
                    direction = "up";
                    sortFloorList();
                    while (currentFloor < destination) {
                        currentFloor++;
                    }
                }
                else if (currentFloor > destination)
                 {
                    direction = "down";
                    sortFloorList();
                    while (currentFloor > destination) {
                        currentFloor--;
                    }
                }
                // else 
                // if (currentFloor == destination)
                //  {
                    direction = null;
                    status = "stopped";
                    completedRequestsList.Add(floorRequestsList[0]);
                    floorRequestsList.RemoveAt(0);
               
                // }
                    
               
            }
            status = "idle";
        }

        public void sortFloorList() {           
                floorRequestsList.Sort();            
            if (direction == "down") {
                floorRequestsList.Reverse();
            } 
        }
        
        public void addNewRequest(int floor) {
            if (floorRequestsList.Contains(floor) != true) {
                floorRequestsList.Add(floor);    
            }
            if (currentFloor < floor) {
                this.direction = "up";
            }
            else if(currentFloor > floor) {
                direction = "down";
            }
            move();
        }
    }
}