using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.SysDto.Comment;

public class CommentDto
{
    public int Rating { get; set; }
    public string? CommentText { get; set; }
}
