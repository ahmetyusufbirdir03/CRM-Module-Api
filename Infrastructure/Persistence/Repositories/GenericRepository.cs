﻿using Applicaton.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class, new()
{
    protected readonly AppDbContext _context;  // DbContext nesnesi
    protected readonly DbSet<T> _dbSet;        // DbSet ile entity seti

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();         // T tipine karşılık gelen DbSet'i al
    }

    // Yeni bir entity ekle
    public virtual async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);           // Veritabanına eklemek için işaretle
        await _context.SaveChangesAsync();              // Değişiklikleri kaydet
        return entity;                                  // Eklenen entity döndür
    }

    // Var olan bir entity'yi sil
    public virtual async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);                           // Silme işlemi işaretle
        await _context.SaveChangesAsync();               // Değişiklikleri kaydet
    }

    // Var olan entity'yi güncelle
    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);                           // Güncelleme işlemi işaretle
        await _context.SaveChangesAsync();               // Değişiklikleri kaydet
        return entity;                                  // Güncellenen entity döndür
    }

    // Koşula bağlı veya tüm kayıtları getir
    public virtual async Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _dbSet.ToListAsync();          // Koşul yoksa tümünü getir
        return await _dbSet.Where(predicate).ToListAsync(); // Koşula göre filtrele ve getir
    }

    // Id'ye göre entity getir
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);               // PK’ya göre bul ve getir
    }
}

