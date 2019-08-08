using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicMuse.Data;
using MusicMuse.Models;

namespace MusicMuse.Controllers
{
    public class BandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bands
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Band bandLoggedIn = _context.Band.Where(x => x.ApplicationUserId == userId).Single();

            List<Musician> musicians = _context.MusicianBandInfluenceScore
                .Include(mbis => mbis.Musician)
                .Where(mbis => mbis.BandId == bandLoggedIn.Id && mbis.Musician.LookingForBand && mbis.Musician.Instrument == bandLoggedIn.MemberLookingFor)
                .OrderByDescending(mbis => mbis.InfluenceScore)
                .Select(mbis => mbis.Musician)
                .ToList();
            
            return View(musicians);
        }

        // GET: Bands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Band.FirstOrDefaultAsync(m => m.Id == id);
            if (band == null)
            {
                return NotFound();
            }

            return View(band);
        }

        // GET: Bands/Create
        public IActionResult Create()
        {
            Band band = new Band();
            return View(band);
        }

        // POST: Bands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BandName,MemberLookingFor,LookingToBeHired,Influence1,Influence2,Influence3")] Band band)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                band.ApplicationUserId = userId;
                _context.Band.Add(band);
                await _context.SaveChangesAsync();

                List<Musician> listOfMusicians = _context.Musician.ToList();
                foreach (var musician in listOfMusicians)
                {
                    MusicianBandInfluenceScore musicianBandInfluenceScore = new MusicianBandInfluenceScore();
                    var influenceScore = 0;
                    if (musician.Influence1 == band.Influence1)
                    {
                        influenceScore += 15;
                    }
                    if (musician.Influence1 == band.Influence2)
                    {
                        influenceScore += 10;
                    }
                    if (musician.Influence1 == band.Influence3)
                    {
                        influenceScore += 7;
                    }
                    if (musician.Influence2 == band.Influence1)
                    {
                        influenceScore += 10;
                    }
                    if (musician.Influence2 == band.Influence2)
                    {
                        influenceScore += 7;
                    }
                    if (musician.Influence2 == band.Influence3)
                    {
                        influenceScore += 5;
                    }
                    if (musician.Influence3 == band.Influence1)
                    {
                        influenceScore += 7;
                    }
                    if (musician.Influence3 == band.Influence2)
                    {
                        influenceScore += 5;
                    }
                    if (musician.Influence3 == band.Influence3)
                    {
                        influenceScore += 3;
                    }
                    musicianBandInfluenceScore.BandId = band.Id;
                    musicianBandInfluenceScore.MusicianId = musician.Id;
                    musicianBandInfluenceScore.InfluenceScore = influenceScore;                   
                    _context.MusicianBandInfluenceScore.Add(musicianBandInfluenceScore);
                    await _context.SaveChangesAsync();
                }

                
                return RedirectToAction(nameof(Index));
            }
            return View(band);
        }

        // GET: Bands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Band.FindAsync(id);
            if (band == null)
            {
                return NotFound();
            }
            return View(band);
        }

        // POST: Bands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BandName,MemberLookingFor,LookingToBeHired")] Band band)
        {
            if (id != band.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(band);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BandExists(band.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(band);
        }

        // GET: Bands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Band
                .FirstOrDefaultAsync(m => m.Id == id);
            if (band == null)
            {
                return NotFound();
            }

            return View(band);
        }

        // POST: Bands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var band = await _context.Band.FindAsync(id);
            _context.Band.Remove(band);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BandExists(int id)
        {
            return _context.Band.Any(e => e.Id == id);
        }

        

    }
}
