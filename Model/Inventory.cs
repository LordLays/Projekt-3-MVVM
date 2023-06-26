using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KregulecApp.Model
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Title")]
        public string GameName { get; set; }
        public bool Mark { get; set; }
        public string Rarity { get; set; }
        public bool Completeness { get; set; }
        public string KnownMissingParts { get; set; }

        public virtual ICollection<Convention> Convention { get; set; } = new List<Convention>();
        public virtual GameCatalog GameCatalog { get; set; }


    }
}
