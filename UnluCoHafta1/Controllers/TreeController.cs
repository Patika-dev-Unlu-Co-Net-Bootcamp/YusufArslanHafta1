using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCoHafta1.Entity;

namespace UnluCoHafta1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private static List<Tree> trees = new List<Tree>()
        {
            new Tree{ID=1,Age=2,Name="Kızılçam",Note="Akdeniz ikliminin müşir türlerinden olup tipik bir ışık ağacıdır"},
            new Tree{ID=2,Age=5,Name="Karaçam",Note="Çamgiller familyasından bir çam türü."},
            new Tree{ID=3,Age=3,Name="Titrek kavak",Note=" söğütgiller familyasından 25 m'ye kadar boylanabilen, silindirik gövde, sık dallı, geniş konik tepeye sahip bir kavak türü."},
            new Tree{ID=4,Age=8,Name="İspi meşesi",Note="Kayıngiller familyasından Türkiye'ye özgü endemik meşe alt türü."},
            new Tree{ID=5,Age=2,Name="Macar meşesi",Note="40 m'ye kadar boy yapabilen kalın dallı geniş tepeli büyükçe bir ağaçtır. Gövde kabukları yukarıdan aşağıya doğru çatlaklı haldedir."}

        };

        [HttpGet("{id}")]
        public IActionResult GetById(int id) // Route 
        {
            var tree = trees.SingleOrDefault(item => item.ID == id);
            if (tree == null)
                return NotFound("Ağaç bulunamadı.");

            return Ok(tree);
        }

        [HttpGet("GetByIdWithQuery")]
        public IActionResult GetByIdWithQuery([FromQuery] int id) // Query
        {
            var tree = trees.SingleOrDefault(item => item.ID == id);
            if (tree == null)
                return NotFound("Ağaç bulunamadı.");

            return Ok(tree);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            if (trees.Count == 0)
                return NotFound("Ağaçlar bulunamadı.");

            return Ok(trees);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tree newTree)
        {
            var tree = trees.SingleOrDefault(item => item.ID == newTree.ID);
            if (tree is not null)
                return BadRequest("Ağaç mevcuttur.");

            trees.Add(newTree);
            return Created("Ağaç eklendi.",newTree);
        }

        [HttpPatch("/editNote/{id}")]
        public IActionResult EditNote(int id, string note)
        {
            var editTree = trees.Find(item => item.ID == id);
            if (editTree is null)
                return BadRequest("Ağaç bulunamadı.");
            if (note is null)
                return BadRequest("Note değeri boş bırakılamaz.");
            editTree.Note = note!=default ? note : editTree.Note;

            return Ok();
        }

        [HttpDelete("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var deleteTree = trees.Find(item => item.ID == id);
            if (deleteTree is null)
                return BadRequest("Ağaç bulunamadı.");
            trees.Remove(deleteTree);
            return Ok();
        }
        
    }
}
