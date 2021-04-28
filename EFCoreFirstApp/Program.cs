using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EFCoreFirstApp
{
    [Table("Author")]
    class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [MaxLength(15)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(15), Required]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return FirstName + "" + LastName; }
        }

        public List<Book> Books { get; set; }
    }

    [Table("Book")]
    class Book
    {
        [Key, Column(Order = 0)]
        public int BookId { get; set; }

        [Column(Order = 1)]
        [Required]
        public string BookName { get; set; }

        [Column(Order = 2)]
        [ConcurrencyCheck]
        [Required]
        public double PricePerUnit { get; set; }

        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        [Column(Order = 4)]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }
    }

    [NotMapped]
    class Test
    {
        [Key]
        public int TestId { get; set; }
    }

    class ShopDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreFirstApp;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {

            ShopDbContext shopDbContext = new ShopDbContext();
            Author author = new Author() { FirstName = "Manzoor", LastName = "Ahmed" };
            
            shopDbContext.Set<Author>();
            //shopDbContext.Set<Book>();

            shopDbContext.Add<Author>(author);
            shopDbContext.SaveChanges();

            shopDbContext.SaveChanges();

            using (ShopDbContext shopDbCodntext = new ShopDbContext())
            {
                Author autdhor = shopDbCodntext.Authors.Find(1);

                if(autdhor != null)
                  Console.WriteLine(autdhor.FullName);
                else
                Console.WriteLine("Author Not Found");



            } 


        }
    }
}
