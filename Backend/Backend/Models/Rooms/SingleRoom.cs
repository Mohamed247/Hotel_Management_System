﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Models.Rooms
{
    class SingleRoom : Room
    {
        public SingleRoom(int id, RoomType type, double pricePerNight, Status status) : 
            base(id, type, pricePerNight, status){}
    }
}
