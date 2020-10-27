using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ActReport.Core.Contracts
{
  public interface IGenericRepository<TEntity> where TEntity : class, IEntityObject, new()
    {
        /// <summary>
        ///     Liefert eine Menge von Entities zurück. Diese kann optional
        ///     gefiltert und/oder sortiert sein.
        ///     Weiters werden bei Bedarf abhängige Entitäten mitgeladen (eager loading).
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<TEntity[]> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        ///     Eindeutige Entität oder null zurückliefern
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// Fügt neue Identität zum Datenbestand hinzu
        /// </summary>
        /// <param name="entity"></param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        ///     Entität per primary key löschen
        /// </summary>
        /// <param name="id"></param>
        bool Delete(object id);

        /// <summary>
        ///     Übergebene Entität löschen.
        /// </summary>
        /// <param name="entityToDelete"></param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        ///     Entität aktualisieren
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        ///     Generisches Count mit Filtermöglichkeit. Sind vom Filter
        ///     Navigationproperties betroffen, können diese per eager-loading
        ///     geladen werden.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");

        /// <summary>
        ///     Liste von Entities einfügen
        /// </summary>
        /// <param name="entities"></param>
        Task InsertManyAsync(IEnumerable<TEntity> entities);
    }
}