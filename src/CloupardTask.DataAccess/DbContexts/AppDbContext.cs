﻿using CloupardTask.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CloupardTask.Api.DbContexts
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{

		}
		public DbSet<Product> Products { get; set; }
	}
}
