﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

public partial class BD6B8_424SEntities : DbContext
{
    public BD6B8_424SEntities()
        : base("name=BD6B8_424SEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public virtual DbSet<PPArticlesEnPanier> PPArticlesEnPanier { get; set; }
    public virtual DbSet<PPCategories> PPCategories { get; set; }
    public virtual DbSet<PPClients> PPClients { get; set; }
    public virtual DbSet<PPCommandes> PPCommandes { get; set; }
    public virtual DbSet<PPDetailsCommandes> PPDetailsCommandes { get; set; }
    public virtual DbSet<PPHistoriquePaiements> PPHistoriquePaiements { get; set; }
    public virtual DbSet<PPPoidsLivraisons> PPPoidsLivraisons { get; set; }
    public virtual DbSet<PPProduits> PPProduits { get; set; }
    public virtual DbSet<PPTaxeFederale> PPTaxeFederale { get; set; }
    public virtual DbSet<PPTaxeProvinciale> PPTaxeProvinciale { get; set; }
    public virtual DbSet<PPTypesLivraison> PPTypesLivraison { get; set; }
    public virtual DbSet<PPTypesPoids> PPTypesPoids { get; set; }
    public virtual DbSet<PPVendeurs> PPVendeurs { get; set; }
    public virtual DbSet<PPVendeursClients> PPVendeursClients { get; set; }
    public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    public virtual DbSet<PPGestionnaires> PPGestionnaires { get; set; }
}
