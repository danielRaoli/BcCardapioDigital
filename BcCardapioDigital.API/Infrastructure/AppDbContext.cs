using BcCardapioDigital.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BcCardapioDigital.API.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
    {

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto>  Produtos{ get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<HorarioFuncionamento> Horarios { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nome)
                      .HasMaxLength(150)
                      .IsRequired();
                entity.Property(c => c.ImageUrl)
                      .IsRequired();

                // Relacionamento 1:N com Produto
                entity.HasMany(c => c.Produtos)
                      .WithOne()
                      .HasForeignKey(p => p.CategoriaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome)
                      .HasMaxLength(150)
                      .IsRequired();
                entity.Property(p => p.Descricao)
                      .HasMaxLength(200);
                entity.Property(p => p.Preco)
                      .HasColumnType("money")
                      .IsRequired();
                entity.Property(p => p.Imagem)
                      .IsRequired();

            });

            // Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Status)
                      .IsRequired();
                entity.Property(p => p.FormaDePagamento)
                      .IsRequired();

                // Relacionamento 1:N com ItemPedido
                entity.HasMany(p => p.Items)
                      .WithOne()
                      .HasForeignKey(ip => ip.PedidoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ItemPedido
            modelBuilder.Entity<ItemPedido>(entity =>
            {
                entity.HasKey(ip => new { ip.PedidoId, ip.ProdutoId });
                entity.Property(ip => ip.Quantidade)
                      .IsRequired();
                entity.Property(ip => ip.PrecoUnitario)
                      .HasColumnType("money")
                      .IsRequired();

                // Relacionamento com Produto
                entity.HasOne(ip => ip.Produto)
                      .WithMany()
                      .HasForeignKey(ip => ip.ProdutoId);
            });

            // HorarioFuncionamento
            modelBuilder.Entity<HorarioFuncionamento>(entity =>
            {
                entity.HasKey(hf => hf.DiaSemana);
                entity.Property(hf => hf.HoraAbertura)
                      .IsRequired();
                entity.Property(hf => hf.HoraFechamento)
                      .IsRequired();
                entity.Property(hf => hf.Funcionando)
                      .IsRequired();
            });

            // Restaurante
            modelBuilder.Entity<Restaurante>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Nome)
                      .HasMaxLength(150)
                      .IsRequired();
                entity.Property(r => r.Endereco)
                      .IsRequired();
                entity.Property(r => r.Telefone)
                      .IsRequired();
                entity.Property(r => r.Email)
                      .IsRequired();

                // Relacionamento 1:N com HorarioFuncionamento
                entity.HasMany(r => r.Horarios)
                      .WithOne()
                      .HasForeignKey(hf => hf.RestauranteId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
