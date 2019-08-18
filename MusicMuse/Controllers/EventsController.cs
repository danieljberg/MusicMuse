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
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> BandIndex(int id)
        {
            var bands = _context.EventBandInfluenceScore
                .Include(mbis => mbis.Band)
                .Where(mbis => mbis.EventId == id)
                .OrderByDescending(mbis => mbis.InfluenceScore)
                .Select(mbis => mbis.Band)
                .ToList();
            return View(bands);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            Event @event = new Event();
            return View(@event);
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventName,Venue,EventInfo,BusinessId,Influence1,Influence2,Influence3")] Event @event)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var businessLoggedIn = _context.Business.Where(x => x.ApplicationUserId == userId).Single();
                @event.BusinessId = businessLoggedIn.Id;
                @event.Posted = DateTime.Now;
                _context.Add(@event);
                await _context.SaveChangesAsync();

                List<Band> listOfBands = _context.Band.ToList();
                foreach (var band in listOfBands)
                {
                    EventBandInfluenceScore eventBandInfluenceScore = new EventBandInfluenceScore();
                    var influenceScore = 0;
                    if (@event.Influence1 == band.Influence1)
                    {
                        influenceScore += 15;
                    }
                    if (@event.Influence1 == band.Influence2)
                    {
                        influenceScore += 10;
                    }
                    if (@event.Influence1 == band.Influence3)
                    {
                        influenceScore += 7;
                    }
                    if (@event.Influence2 == band.Influence1)
                    {
                        influenceScore += 10;
                    }
                    if (@event.Influence2 == band.Influence2)
                    {
                        influenceScore += 7;
                    }
                    if (@event.Influence2 == band.Influence3)
                    {
                        influenceScore += 5;
                    }
                    if (@event.Influence3 == band.Influence1)
                    {
                        influenceScore += 7;
                    }
                    if (@event.Influence3 == band.Influence2)
                    {
                        influenceScore += 5;
                    }
                    if (@event.Influence3 == band.Influence3)
                    {
                        influenceScore += 3;
                    }
                    eventBandInfluenceScore.BandId = band.Id;
                    eventBandInfluenceScore.EventId = @event.Id;
                    eventBandInfluenceScore.InfluenceScore = influenceScore;
                    _context.EventBandInfluenceScore.Add(eventBandInfluenceScore);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Businesses");
            }
            //ViewData["BusinessId"] = new SelectList(_context.Business, "Id", "Id", @event.BusinessId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["BusinessId"] = new SelectList(_context.Business, "Id", "Id", @event.BusinessId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventName,Venue,EventInfo,BusinessId,Posted")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var businessLoggedIn = _context.Business.Where(x => x.ApplicationUserId == userId).Single();
                    @event.BusinessId = businessLoggedIn.Id;
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Businesses");
            }
            ViewData["BusinessId"] = new SelectList(_context.Business, "Id", "Id", @event.BusinessId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Businesses");
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
