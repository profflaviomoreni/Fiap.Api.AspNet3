using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.AspNet3.Models
{

    /// <summary>
    ///     Classe que representa a entidade de categoria
    /// </summary>
    [Table("Categorias")]
    public class CategoriaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaId { get; set; }

        [Required]
        [MaxLength(30)]
        public string NomeCategoria { get; set; }

        public CategoriaModel()
        {
        }

        public CategoriaModel(int categoriaId, string nomeCategoria)
        {
            CategoriaId = categoriaId;
            NomeCategoria = nomeCategoria;
        }


    }
}
