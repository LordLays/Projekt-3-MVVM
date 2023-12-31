﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KregulecApp.Model
{
    public class Tags
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GameCatalog> GameCatalogs { get; set; } = new List<GameCatalog>();
    }
}
