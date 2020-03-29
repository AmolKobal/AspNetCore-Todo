using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string UserID { get; set; }

        public bool IsDone { get; set; }

        [Required(ErrorMessage="Todo item is require")]
        public string Title { get; set; }

        public DateTimeOffset? DueAt { get; set; }
    }
}