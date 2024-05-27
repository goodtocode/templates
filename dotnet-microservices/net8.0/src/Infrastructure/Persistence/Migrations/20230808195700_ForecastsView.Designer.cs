﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SemanticKernelMicroservice.Infrastructure.Persistence;

#nullable disable

namespace SemanticKernelMicroservice.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(SemanticKernelMicroserviceContext))]
    [Migration("20230808195700_ForecastsView")]
    partial class ForecastsView
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SemanticKernelMicroservice.Core.Domain.Forecasts.Entities.Forecast", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ForecastDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("TemperatureF")
                        .HasColumnType("int");

                    b.Property<string>("ZipCodesSearch")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.ToTable("Forecasts", (string)null);
                });

            modelBuilder.Entity("SemanticKernelMicroservice.Core.Domain.Forecasts.Entities.WeatherForecastZipcode", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WeatherForecastKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Key");

                    b.HasIndex("WeatherForecastKey");

                    b.ToTable("ForecastZipCodes", (string)null);
                });

            modelBuilder.Entity("SemanticKernelMicroservice.Core.Domain.Forecasts.Models.ForecastsView", b =>
                {
                    b.Property<Guid>("Key")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ForecastDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("TemperatureF")
                        .HasColumnType("int");

                    b.Property<string>("ZipCodesSearch")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.ToTable((string)null);

                    b.ToView("ForecastsView", (string)null);
                });

            modelBuilder.Entity("SemanticKernelMicroservice.Core.Domain.Forecasts.Entities.WeatherForecastZipcode", b =>
                {
                    b.HasOne("SemanticKernelMicroservice.Core.Domain.Forecasts.Entities.Forecast", "WeatherForecast")
                        .WithMany("ZipCodes")
                        .HasForeignKey("WeatherForecastKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeatherForecast");
                });

            modelBuilder.Entity("SemanticKernelMicroservice.Core.Domain.Forecasts.Entities.Forecast", b =>
                {
                    b.Navigation("ZipCodes");
                });
#pragma warning restore 612, 618
        }
    }
}