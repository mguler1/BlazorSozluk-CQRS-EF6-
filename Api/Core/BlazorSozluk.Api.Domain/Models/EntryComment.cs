﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Domain.Models
{
    public class EntryComment : BaseEntity
    {
        public string Content { get; set; } 

        public Guid CreatedById { get; set; }
        public Guid EntryById { get; set; }
        public virtual Entry Entry { get; set; }

        public virtual Users CreatedBy { get; set; }

        public virtual ICollection<EntryCommentVote> EntryCommentVotes{get;set;}

        public virtual ICollection<EntryCommentFavorite> EntryCommentFavorite {get; set;}
    }
}