﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.PostDtos
{
    public class PostImagesDto
    {
        public int? Id { get; set; }
        public string? ImageUrl { get; set; }
        public int? PostId { get; set; }
    }
}
