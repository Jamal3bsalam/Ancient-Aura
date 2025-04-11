using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.PostDtos
{
    public class PostsFilesDto
    {
        public IFormFile? File { get; set; }
    }
}
