using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Models {
    public class NewTodo {
        [Required]
        public string Description { get; set; }
    }
}