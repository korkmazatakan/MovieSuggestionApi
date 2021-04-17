using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Comment:IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public int MovieId { get; set; }
        public int SubCommentOf { get; set; }
        public bool IsPublished { get; set; }

    }
}
