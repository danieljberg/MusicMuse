using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicMuse.Data;
using MusicMuse.Models;

namespace MusicMuse.Controllers
{
    public class MusicianBandInfluenceScoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusicianBandInfluenceScoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MusicianBandInfluenceScores
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MusicianBandInfluenceScore.Include(m => m.Band).Include(m => m.Musician);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MusicianBandInfluenceScores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicianBandInfluenceScore = await _context.MusicianBandInfluenceScore                
                .Include(m => m.Band)
                .Include(m => m.Musician)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicianBandInfluenceScore == null)
            {
                return NotFound();
            }

            return View(musicianBandInfluenceScore);
        }

        // GET: MusicianBandInfluenceScores/Create
        public IActionResult Create()
        {
            ViewData["BandId"] = new SelectList(_context.Band, "Id", "Id");
            ViewData["MusicianId"] = new SelectList(_context.Musician, "Id", "Id");
            return View();
        }

        // POST: MusicianBandInfluenceScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MusicianId,BandId,InfluenceScore")] MusicianBandInfluenceScore musicianBandInfluenceScore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicianBandInfluenceScore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BandId"] = new SelectList(_context.Band, "Id", "Id", musicianBandInfluenceScore.BandId);
            ViewData["MusicianId"] = new SelectList(_context.Musician, "Id", "Id", musicianBandInfluenceScore.MusicianId);
            return View(musicianBandInfluenceScore);
        }

        // GET: MusicianBandInfluenceScores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicianBandInfluenceScore = await _context.MusicianBandInfluenceScore.FindAsync(id);
            if (musicianBandInfluenceScore == null)
            {
                return NotFound();
            }
            ViewData["BandId"] = new SelectList(_context.Band, "Id", "Id", musicianBandInfluenceScore.BandId);
            ViewData["MusicianId"] = new SelectList(_context.Musician, "Id", "Id", musicianBandInfluenceScore.MusicianId);
            return View(musicianBandInfluenceScore);
        }

        // POST: MusicianBandInfluenceScores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MusicianId,BandId,InfluenceScore")] MusicianBandInfluenceScore musicianBandInfluenceScore)
        {
            if (id != musicianBandInfluenceScore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicianBandInfluenceScore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicianBandInfluenceScoreExists(musicianBandInfluenceScore.Id))
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
            ViewData["BandId"] = new SelectList(_context.Band, "Id", "Id", musicianBandInfluenceScore.BandId);
            ViewData["MusicianId"] = new SelectList(_context.Musician, "Id", "Id", musicianBandInfluenceScore.MusicianId);
            return View(musicianBandInfluenceScore);
        }

        // GET: MusicianBandInfluenceScores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicianBandInfluenceScore = await _context.MusicianBandInfluenceScore
                .Include(m => m.Band)
                .Include(m => m.Musician)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicianBandInfluenceScore == null)
            {
                return NotFound();
            }

            return View(musicianBandInfluenceScore);
        }

        // POST: MusicianBandInfluenceScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicianBandInfluenceScore = await _context.MusicianBandInfluenceScore.FindAsync(id);
            _context.MusicianBandInfluenceScore.Remove(musicianBandInfluenceScore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicianBandInfluenceScoreExists(int id)
        {
            return _context.MusicianBandInfluenceScore.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetBandInfluenceScore(int id)
        {

            var influenceScore = 0;
            var band = await _context.Band.FindAsync(id);
            foreach (Musician musician in _context.Musician)
            {
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

            }

            return Ok();
        }
    }
}
