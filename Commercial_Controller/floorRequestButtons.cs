namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class FloorRequestButton
    {
        public int ID;
        public int floor;
        public string direction;

        public FloorRequestButton(int _ID, int _floor, string _direction)
        {
            this.ID = _ID;
            this.floor = _floor;
            this.direction = _direction;
        }
    }
}