using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KregulecApp.Model
{
    public class GameCatalog
    {
        [Key]
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Players { get; set; }
        public TimeSpan PlayTime { get; set; }
        public int Age { get; set; }
        public string Language { get; set; }


        public virtual IList<Tags> Tags { get; set; } = new List<Tags>();
        public virtual IList<Inventory> Inventory { get; set; }
    }
}
