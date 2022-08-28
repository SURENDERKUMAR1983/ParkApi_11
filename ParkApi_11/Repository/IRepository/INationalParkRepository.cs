using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkApi_11.Repository.IRepository
{
   public interface INationalParkRepository
    {
        ICollection<NationalPark>GetNationalParks();       // for Display
        NationalPark GetNationalPark(int nationalParkId);  // for Find
        bool NationalParkExists(int nationalParkId);
        bool NationalParkExists(string nationalParkName);
        bool CreateNationalPark(NationalPark nationalPark);
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);
        bool Save();
    }
}
