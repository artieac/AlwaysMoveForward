using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlwaysMoveForward.OAuth2.DataLayer.Models
{
    public partial class AMFOAuthDbContext : DbContext
    {
        public virtual DbSet<AccessTokens> AccessTokens { get; set; }
        public virtual DbSet<Amfusers> Amfusers { get; set; }
        public virtual DbSet<ApiClaims> ApiClaims { get; set; }
        public virtual DbSet<ApiResources> ApiResources { get; set; }
        public virtual DbSet<ApiScopeClaims> ApiScopeClaims { get; set; }
        public virtual DbSet<ApiScopes> ApiScopes { get; set; }
        public virtual DbSet<ApiSecrets> ApiSecrets { get; set; }
        public virtual DbSet<ClientClaims> ClientClaims { get; set; }
        public virtual DbSet<ClientCorsOrigins> ClientCorsOrigins { get; set; }
        public virtual DbSet<ClientGrantTypes> ClientGrantTypes { get; set; }
        public virtual DbSet<ClientIdPrestrictions> ClientIdPrestrictions { get; set; }
        public virtual DbSet<ClientPostLogoutRedirectUris> ClientPostLogoutRedirectUris { get; set; }
        public virtual DbSet<ClientProperties> ClientProperties { get; set; }
        public virtual DbSet<ClientRedirectUris> ClientRedirectUris { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ClientScopes> ClientScopes { get; set; }
        public virtual DbSet<ClientSecrets> ClientSecrets { get; set; }
        public virtual DbSet<ConsumerNonce> ConsumerNonce { get; set; }
        public virtual DbSet<Consumers> Consumers { get; set; }
        public virtual DbSet<IdentityClaims> IdentityClaims { get; set; }
        public virtual DbSet<IdentityResources> IdentityResources { get; set; }
        public virtual DbSet<LoginAttempts> LoginAttempts { get; set; }
        public virtual DbSet<PersistedGrants> PersistedGrants { get; set; }
        public virtual DbSet<RequestTokens> RequestTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "db_datareader");

            modelBuilder.Entity<AccessTokens>(entity =>
            {
                entity.ToTable("AccessTokens", "dbo");

                entity.HasIndex(e => e.Id);

                entity.HasIndex(e => e.Token)
                    .IsUnique();

                entity.Property(e => e.ConsumerKey)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateGranted).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Realm)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.TokenSecret)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Amfusers>(entity =>
            {
                entity.ToTable("AMFUsers", "dbo");

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Id);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHint).HasMaxLength(255);

                entity.Property(e => e.PasswordSalt).HasMaxLength(50);

                entity.Property(e => e.ResetToken).HasMaxLength(36);
            });

            modelBuilder.Entity<ApiClaims>(entity =>
            {
                entity.ToTable("ApiClaims", "dbo");

                entity.HasIndex(e => e.ApiResourceId);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.ApiResource)
                    .WithMany(p => p.ApiClaims)
                    .HasForeignKey(d => d.ApiResourceId);
            });

            modelBuilder.Entity<ApiResources>(entity =>
            {
                entity.ToTable("ApiResources", "dbo");

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ApiScopeClaims>(entity =>
            {
                entity.ToTable("ApiScopeClaims", "dbo");

                entity.HasIndex(e => e.ApiScopeId);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.ApiScope)
                    .WithMany(p => p.ApiScopeClaims)
                    .HasForeignKey(d => d.ApiScopeId);
            });

            modelBuilder.Entity<ApiScopes>(entity =>
            {
                entity.ToTable("ApiScopes", "dbo");

                entity.HasIndex(e => e.ApiResourceId);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.ApiResource)
                    .WithMany(p => p.ApiScopes)
                    .HasForeignKey(d => d.ApiResourceId);
            });

            modelBuilder.Entity<ApiSecrets>(entity =>
            {
                entity.ToTable("ApiSecrets", "dbo");

                entity.HasIndex(e => e.ApiResourceId);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Type).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(2000);

                entity.HasOne(d => d.ApiResource)
                    .WithMany(p => p.ApiSecrets)
                    .HasForeignKey(d => d.ApiResourceId);
            });

            modelBuilder.Entity<ClientClaims>(entity =>
            {
                entity.ToTable("ClientClaims", "dbo");

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientClaims)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientCorsOrigins>(entity =>
            {
                entity.ToTable("ClientCorsOrigins", "dbo");

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientCorsOrigins)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientGrantTypes>(entity =>
            {
                entity.ToTable("ClientGrantTypes", "dbo");

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.GrantType)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientGrantTypes)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientIdPrestrictions>(entity =>
            {
                entity.ToTable("ClientIdPRestrictions", "dbo");

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.Provider)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientIdPrestrictions)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientPostLogoutRedirectUris>(entity =>
            {
                entity.ToTable("ClientPostLogoutRedirectUris", "dbo");

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.PostLogoutRedirectUri)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientPostLogoutRedirectUris)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientProperties>(entity =>
            {
                entity.ToTable("ClientProperties", "dbo");

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientProperties)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientRedirectUris>(entity =>
            {
                entity.ToTable("ClientRedirectUris", "dbo");

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.RedirectUri)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientRedirectUris)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.ToTable("Clients", "dbo");

                entity.HasIndex(e => e.ClientId)
                    .IsUnique();

                entity.Property(e => e.BackChannelLogoutUri).HasMaxLength(2000);

                entity.Property(e => e.ClientClaimsPrefix).HasMaxLength(200);

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ClientName).HasMaxLength(200);

                entity.Property(e => e.ClientUri).HasMaxLength(2000);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FrontChannelLogoutUri).HasMaxLength(2000);

                entity.Property(e => e.LogoUri).HasMaxLength(2000);

                entity.Property(e => e.PairWiseSubjectSalt).HasMaxLength(200);

                entity.Property(e => e.ProtocolType)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ClientScopes>(entity =>
            {
                entity.ToTable("ClientScopes", "dbo");

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.Scope)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientScopes)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ClientSecrets>(entity =>
            {
                entity.ToTable("ClientSecrets", "dbo");

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Type).HasMaxLength(250);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientSecrets)
                    .HasForeignKey(d => d.ClientId);
            });

            modelBuilder.Entity<ConsumerNonce>(entity =>
            {
                entity.HasKey(e => e.Nonce);

                entity.ToTable("ConsumerNonce", "dbo");

                entity.HasIndex(e => e.Nonce);

                entity.Property(e => e.Nonce)
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.ConsumerKey)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<Consumers>(entity =>
            {
                entity.HasKey(e => e.ConsumerKey);

                entity.ToTable("Consumers", "dbo");

                entity.HasIndex(e => e.ConsumerKey);

                entity.Property(e => e.ConsumerKey)
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.ConsumerSecret)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PublicKey)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<IdentityClaims>(entity =>
            {
                entity.ToTable("IdentityClaims", "dbo");

                entity.HasIndex(e => e.IdentityResourceId);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdentityResource)
                    .WithMany(p => p.IdentityClaims)
                    .HasForeignKey(d => d.IdentityResourceId);
            });

            modelBuilder.Entity<IdentityResources>(entity =>
            {
                entity.ToTable("IdentityResources", "dbo");

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<LoginAttempts>(entity =>
            {
                entity.ToTable("LoginAttempts", "dbo");

                entity.HasIndex(e => e.Id);

                entity.HasIndex(e => e.UserName);

                entity.Property(e => e.AttemptDate).HasColumnType("datetime");

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PersistedGrants>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("PersistedGrants", "dbo");

                entity.HasIndex(e => new { e.SubjectId, e.ClientId, e.Type });

                entity.Property(e => e.Key)
                    .HasMaxLength(200)
                    .ValueGeneratedNever();

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Data).IsRequired();

                entity.Property(e => e.SubjectId).HasMaxLength(200);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RequestTokens>(entity =>
            {
                entity.ToTable("RequestTokens", "dbo");

                entity.HasIndex(e => e.Id);

                entity.HasIndex(e => e.Token)
                    .IsUnique();

                entity.Property(e => e.CallbackUrl)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.ConsumerKey)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.DateAuthorized).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Realm).HasMaxLength(512);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.TokenSecret)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.Property(e => e.VerifierCode).HasMaxLength(36);

                entity.HasOne(d => d.AccessToken)
                    .WithMany(p => p.RequestTokens)
                    .HasForeignKey(d => d.AccessTokenId)
                    .HasConstraintName("FK_RequestTokens_AccessTokens");
            });
        }
    }
}
