using Microsoft.EntityFrameworkCore;
using ParkApi_11.data;
using ParkApi_11.Models;
using ParkApi_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkApi_11.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _context;
        public TrailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateTrail(Trail trail)
        {
            _context.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _context.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int trailId)
        {
            return _context.Trails.Include(np=>np.NationalPark).FirstOrDefault(t=>t.Id==trailId);
        }

        public ICollection<Trail> GetTrailinNationalPark(int nationalParkId)
        {
            return _context.Trails.Include(np => np.NationalPark).
                Where(t => t.NationalParkId == nationalParkId).ToList();
        }      
        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool TrailExists(int trailId)
        {
            return _context.Trails.Any(np => np.Id == trailId);
        }

        public bool TrailExists(string trailname)
        {
            return _context.Trails.Any(np => np.Name == trailname);
        }

        public bool UpadateTrail(Trail trail)
        {
            _context.Trails.Update(trail);
            return Save();                
        }

        ICollection<Trail> ITrailRepository.GetTrails()
        {
            return _context.Trails.Include(np => np.NationalPark).ToList();
        }
    }
}
