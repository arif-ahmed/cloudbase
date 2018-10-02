﻿// <auto-generated />
using System;
using CloudBase.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CloudBase.Data.Migrations.ECommerceDb
{
    [DbContext(typeof(ECommerceDbContext))]
    [Migration("20181002091923_ProductCategory")]
    partial class ProductCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cloudbase.Entities.ECommerce.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<Guid>("LastUpdatedBy");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Cloudbase.Entities.ECommerce.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryGuid");

                    b.Property<Guid?>("CategoryId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<Guid>("LastUpdatedBy");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Cloudbase.Entities.ECommerce.Product", b =>
                {
                    b.HasOne("Cloudbase.Entities.ECommerce.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
