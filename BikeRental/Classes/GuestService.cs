using BikeRental.POCO;
using System.Collections.Generic;

namespace BikeRental.Classes
{
    public static class GuestService
    {
        public static List<Guest> GetGuests(string _room)
        {
            var _resoult = new List<Guest>();
            using (var _database = new MysqlSupport())
            {
                _database.Query = "select numer, rodzmel, nrg, nrp, imie, nazwis from meldunki_table where nrp=' "+_room+"';";
                foreach (var row in _database.ExecuteQuery())
                {
                    _resoult.Add(new Guest
                    {
                        GuestNumber = row["numer"].ToString(),
                        GuestType = row["rodzmel"].ToString(),
                        GroupNumber = row["nrg"].ToString(),
                        RoomNumber = row["nrp"].ToString(),
                        Name = PlChar.ReplaceChar(row["imie"].ToString()),
                        Surname = PlChar.ReplaceChar(row["nazwis"].ToString())
                    });
                }
            }
            return _resoult;
        }
    }
}
