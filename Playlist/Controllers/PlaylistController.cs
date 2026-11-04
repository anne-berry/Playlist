using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Mvc;
using System;


namespace Playlist.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly string[] songs =
        {
            "Toast - Koffee",
            "Essence - Wizkid",
            "Perfect - Ed Sheeran",
            "Calm Down - Rema",
            "Flowers - Miley Cyrus",
            "One Love - Bob Marley"
        };

        // Display the playlist
        public IActionResult Index()
        {
            return View(songs);
        }

        // Display the liner notes
        [HttpGet("/liner-notes")]
        public IActionResult Notes()
        {
            return View();
        }

        // Display an individual song
        [HttpGet("/track/{number:int:range(1,6)=1}")]
        public IActionResult Track(int number = 1)
        {
            ViewBag.Number = number;
            ViewBag.Song = songs[number - 1];

            ViewBag.Previous = number > 1 ? number - 1 : 0;
            ViewBag.Next = number < songs.Length ? number + 1 : 0;

            return View();
        }

        // Search for a song or artist
        [HttpGet("/find/{word:alpha}")]
        public IActionResult Find(string word)
        {
            int index = Array.FindIndex(
                songs,
                song => song.Contains(
                    word,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (index >= 0)
            {
                ViewBag.Song = songs[index];
                ViewBag.Number = index + 1;
            }
            else
            {
                ViewBag.Message = "No track matches " + word;
            }

            return View();
        }

        // Handle searches containing invalid characters
        [HttpGet("/find/{**rest}")]
        public IActionResult InvalidSearch(string? rest)
        {
            ViewBag.SearchTerm = rest ?? "";
            return View("InvalidSearch");
        }
    }
}
