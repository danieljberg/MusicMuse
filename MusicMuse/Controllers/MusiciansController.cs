﻿using System;
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
    public class MusiciansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusiciansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Musicians
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var musicianLoggedIn = _context.Musician.Where(x => x.ApplicationUserId == userId).FirstOrDefault();
            

            return View(musicianLoggedIn);
        }

        // GET: Musicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musician
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // GET: Musicians/Create
        public IActionResult Create()
        {
            Musician musician = new Musician();
            return View(musician);
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Instrument,LookingForBand,WantToCollaborate,Influence1,Influence2,Influence3")] Musician musician)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                musician.ApplicationUserId = userId;
                _context.Add(musician);
                await _context.SaveChangesAsync();

                List<Band> listOfBands = _context.Band.ToList();
                foreach (var band in listOfBands)
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

                var listOfMusicians = _context.Musician.ToList();
                foreach (var musicianToCheck in listOfMusicians)
                {
                    if (musician.Id != musicianToCheck.Id)
                    {
                        MusicianMusicianInfluenceScore musicianMusicianInfluenceScore = new MusicianMusicianInfluenceScore();
                        var influenceScore = 0;
                        if (musicianToCheck.Influence1 == musician.Influence1)
                        {
                            influenceScore += 15;
                        }
                        if (musicianToCheck.Influence1 == musician.Influence2)
                        {
                            influenceScore += 10;
                        }
                        if (musicianToCheck.Influence1 == musician.Influence3)
                        {
                            influenceScore += 7;
                        }
                        if (musicianToCheck.Influence2 == musician.Influence1)
                        {
                            influenceScore += 10;
                        }
                        if (musicianToCheck.Influence2 == musician.Influence2)
                        {
                            influenceScore += 7;
                        }
                        if (musicianToCheck.Influence2 == musician.Influence3)
                        {
                            influenceScore += 5;
                        }
                        if (musicianToCheck.Influence3 == musician.Influence1)
                        {
                            influenceScore += 7;
                        }
                        if (musicianToCheck.Influence3 == musician.Influence2)
                        {
                            influenceScore += 5;
                        }
                        if (musicianToCheck.Influence3 == musician.Influence3)
                        {
                            influenceScore += 3;
                        }
                        musicianMusicianInfluenceScore.MusicianId = musician.Id;
                        musicianMusicianInfluenceScore.MusicianToCheckId = musicianToCheck.Id;
                        musicianMusicianInfluenceScore.InfluenceScore = influenceScore;
                        _context.MusicianMusicianInfluenceScore.Add(musicianMusicianInfluenceScore);
                        await _context.SaveChangesAsync();
                    }
                    continue;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(musician);
        }

        // GET: Musicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musician.FindAsync(id);
            if (musician == null)
            {
                return NotFound();
            }
            return View(musician);
        }

        // POST: Musicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationUserId,Id,FirstName,LastName,Instrument,LookingForBand,WantToCollaborate,Influence1,Influence2,Influence3")] Musician musician)
        {
            if (id != musician.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    musician.ApplicationUserId = userId;
                    _context.Update(musician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicianExists(musician.Id))
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
            return View(musician);
        }

        // GET: Musicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musician
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // POST: Musicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musician = await _context.Musician.FindAsync(id);
            _context.Musician.Remove(musician);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicianExists(int id)
        {
            return _context.Musician.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Bands()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var musicianLoggedIn = _context.Musician.Where(x => x.ApplicationUserId == userId).Single();

            var bands = _context.MusicianBandInfluenceScore
                .Include(mbis => mbis.Band)
                .Where(mbis => mbis.MusicianId == musicianLoggedIn.Id && mbis.Band.MemberLookingFor == musicianLoggedIn.Instrument)
                .OrderByDescending(mbis => mbis.InfluenceScore)
                .Select(mbis => mbis.Band)
                .ToList();

            return View(bands);
        }
        public async Task<IActionResult> Musicians()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var musicianLoggedIn = _context.Musician.Where(x => x.ApplicationUserId == userId).Single();

            var musicians = _context.MusicianMusicianInfluenceScore
                .Include(mbis => mbis.Musician)
                .Where(mbis => mbis.MusicianId == musicianLoggedIn.Id && mbis.Musician.WantToCollaborate)
                .OrderByDescending(mbis => mbis.InfluenceScore)
                .Select(mbis => mbis.Musician)
                .ToList();

            return View(musicians);
        }

    }
}
