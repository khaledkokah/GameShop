using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GameShop.Dtos;
using GameShop.Models;

namespace GameShop.Controllers.Api
{
    public class GamesController : ApiController
    {
        private ApplicationDbContext _context;

        public GamesController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<GameDto> GetGames(string query = null)
        {
            var gamesQuery = _context.Games.Include(m => m.Category);

            if (!String.IsNullOrWhiteSpace(query))
                gamesQuery = gamesQuery.Where(m => m.Name.Contains(query));

            return gamesQuery.ToList()
                .Select(Mapper.Map<Game, GameDto>);
        }

        public IHttpActionResult GetGame(int id)
        {
            var game = _context.Games.SingleOrDefault(c => c.Id == id);

            if (game == null)
                return NotFound();

            return Ok(Mapper.Map<Game, GameDto>(game));
        }

        [HttpPost]
        public IHttpActionResult CreateGame(GameDto GameDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var game = Mapper.Map<GameDto, Game>(GameDto);
            _context.Games.Add(game);
            _context.SaveChanges();

            GameDto.Id = game.Id;
            return Created(new Uri(Request.RequestUri + "/" + game.Id), GameDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateGame(int id, GameDto GameDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var gameInDb = _context.Games.SingleOrDefault(c => c.Id == id);

            if (gameInDb == null)
                return NotFound();

            Mapper.Map(GameDto, gameInDb);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteGame(int id)
        {
            var gameInDb = _context.Games.SingleOrDefault(c => c.Id == id);

            if (gameInDb == null)
                return NotFound();

            _context.Games.Remove(gameInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}