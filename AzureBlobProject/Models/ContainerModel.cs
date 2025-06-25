using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AzureBlobProject.Models
{
    public class ContainerModel
    {
        [Required]
        public string Name { get; set; }
    }
}
