﻿using PlacesAPI.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlacesAPI.Models
{
    public partial class Flag : IIdentifiable
    {
        #region Constructor

        public Flag()
        {
            Territories = new HashSet<Territory>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(6)]
        [Required]
        public string Code { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Complete { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<Territory> Territories { get; set; }

        #endregion Foreign Properties
    }
}
