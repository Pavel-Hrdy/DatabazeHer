using DataHer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataHer.Model
{
    public class Game : IEntity
    {

        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Název hry je vyžadován")]
        public virtual string Name { get; set; }
        [Required(ErrorMessage = "Vývojář je vyžadován")]
        public virtual string Developer { get; set; }
        [Required(ErrorMessage = "Rok vydání je vyžadován")]
        [Range(1950, 2019, ErrorMessage = "Rok může být od 1950-2019")]
        public virtual int PublishedYear { get; set; }

        [AllowHtml]
        public virtual string Video { get; set; }
        [AllowHtml]
        public virtual string Description { get; set; }
        [AllowHtml]
        public virtual string DescriptionDeveloper { get; set; }

        public virtual string ImageName { get; set; }

        public virtual GameCategory Category { get; set; }
        
        }
    }
