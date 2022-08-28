using ParkApi_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkApi_11.Repository.IRepository
{
   public interface ITrailRepository
    {
        ICollection<Trail> GetTrails(); //display
        ICollection<Trail> GetTrailinNationalPark(int nationalParkId);  //Find Code
        Trail GetTrail(int trailId);
        bool TrailExists(int trailId);
        bool TrailExists(string trailname);
        bool CreateTrail(Trail trail);
        bool UpadateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
    }
}
